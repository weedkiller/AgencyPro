// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.Data.UnitOfWork;
using AgencyPro.Core.DisperseFunds.Events;
using AgencyPro.Core.DisperseFunds.Services;
using AgencyPro.Core.DisperseFunds.ViewModels;
using AgencyPro.Core.OrganizationPeople.Services;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.PayoutIntents.Models;
using AgencyPro.Core.PayoutIntents.ViewModels;
using AgencyPro.Core.Transfers.Models;
using AgencyPro.Services.DisperseFunds.EventHandlers;
using AgencyPro.Services.Invoices.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;
using Stripe;

namespace AgencyPro.Services.DisperseFunds
{
    public partial class DisperseFundsService : Service<OrganizationPayoutIntent>, IDisperseFundsService
    {
        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(DisperseFundsService)}.{callerName}] - {message}";
        }

        private readonly IRepositoryAsync<StripeTransfer> _transfers;
        
        private readonly ILogger<PayoutService> _logger;
        private readonly TransferService _transferService;
        private readonly IRepositoryAsync<IndividualPayoutIntent> _individualPayoutIntents;
        private readonly IRepositoryAsync<OrganizationPayoutIntent> _organizationPayoutIntents;
        
        public DisperseFundsService(
            IUnitOfWorkAsync unitOfWork,
            IServiceProvider serviceProvider,
            ILogger<PayoutService> logger,
            FundsDispersalEventHandlers fundsEventHandlers,
            InvoicesEventHandlers handlers,
            TransferService transferService) : base(serviceProvider)
        {
            _logger = logger;
            _transferService = transferService;
            _individualPayoutIntents = unitOfWork.RepositoryAsync<IndividualPayoutIntent>();
            _organizationPayoutIntents = unitOfWork.RepositoryAsync<OrganizationPayoutIntent>();
            _transfers = unitOfWork.RepositoryAsync<StripeTransfer>();

            AddEventHandler(handlers, fundsEventHandlers);
        }

        public async Task<List<OrganizationPayoutIntentOutput>> GetPending(IAgencyOwner principal, PayoutFilters filters)
        {
            var payouts = await _organizationPayoutIntents.Queryable()
                .Where(x => x.OrganizationId == principal.OrganizationId && string.IsNullOrWhiteSpace(x.InvoiceTransferId))
                .ProjectTo<OrganizationPayoutIntentOutput>(ProjectionMapping).ToListAsync();

            return payouts;
        }


        public async Task<List<IndividualPayoutIntentOutput>> GetPending(IOrganizationPerson principal, PayoutFilters filters)
        {
            var payouts = await _individualPayoutIntents.Queryable()
                .Where(x => x.OrganizationId == principal.OrganizationId && x.PersonId == principal.PersonId)
                .ProjectTo<IndividualPayoutIntentOutput>(ProjectionMapping)
                .ToListAsync();

            return payouts;
        }

        public async Task<DisperseFundsResult> DisperseInvoice(IProviderAgencyOwner ao, string invoiceId)
        {
            _logger.LogInformation(GetLogMessage("Disperse for Organization : {0} & Invoice Id : {1}"), ao.OrganizationId, invoiceId);

            var individualResult = await DisperseFundsToIndividual(ao, invoiceId);

            var organizationResult = await DisperseFundsToOrganization(ao, invoiceId);

            return new DisperseFundsResult()
            {
                Succeeded = individualResult.Succeeded || organizationResult.Succeeded,
                Amount = individualResult.Amount + organizationResult.Amount,
                InvoiceId = invoiceId,
                TotalTransfersMade = individualResult.TotalTransfersMade + organizationResult.TotalTransfersMade,
                TransferId = $"{individualResult.TransferId}+{organizationResult.TransferId}"
            };
        }


        public async Task<DisperseFundsResult> TransferCreated(Transfer transfer)
        {
            _logger.LogInformation(GetLogMessage("Transfer ID: {0}"), transfer.Id);
            var retVal = new DisperseFundsResult()
            {
                TransferId = transfer.Id,
                Amount = 0,

            };

            if (transfer.Metadata.ContainsKey("invoice-id"))
            {
                string invoiceId = transfer.Metadata["invoice-id"];
                _logger.LogDebug(GetLogMessage("Invoice Id: {0}"), invoiceId);

                retVal.InvoiceId = invoiceId;

                var stripeTransfer = new StripeTransfer
                {
                    ObjectState = ObjectState.Added,
                    Id = transfer.Id,
                    Amount = transfer.Amount / 100m,
                    Created = transfer.Created,
                    DestinationId = transfer.DestinationId,
                    AmountReversed = transfer.AmountReversed / 100m,
                    Description = transfer.Description,
                    InvoiceTransfer = new InvoiceTransfer()
                    {
                        ObjectState = ObjectState.Added,
                        InvoiceId = invoiceId,
                        TransferId = transfer.Id,
                    }
                };
                stripeTransfer.InjectFrom(transfer);

                var firstUpdate = _transfers.InsertOrUpdateGraph(stripeTransfer, true);
                _logger.LogDebug(GetLogMessage("{0} Transfer Records Created"), firstUpdate);

                if (firstUpdate > 0)
                {
                    stripeTransfer.ObjectState = ObjectState.Modified;
                    if (transfer.Metadata.ContainsKey("individual-payout-ids"))
                    {
                        if (Guid.TryParse(transfer.Metadata["person-id"], out var personId))
                        {
                            _logger.LogInformation(GetLogMessage("This is an individual payout transaction request"));

                            var payoutIds = transfer.Metadata["individual-payout-ids"].Split(",")
                                .Select(Guid.Parse).ToArray();

                            _logger.LogDebug(GetLogMessage("Person Payout Ids: {0}"), payoutIds);
                            
                            var payouts = await _individualPayoutIntents.Queryable()
                                .Where(x => payoutIds.Contains(x.Id) && x.InvoiceTransferId == null)
                                .ToListAsync();

                            _logger.LogDebug(GetLogMessage("{0} Individual Payouts to process"), payouts.Count);

                            foreach (var x in payouts)
                            {
                                _logger.LogInformation(GetLogMessage("Intent Id: {0}; Transfer Id: {1}"), x.Id, transfer.Id);

                                x.InvoiceTransferId = transfer.Id;
                                x.ObjectState = ObjectState.Modified;

                                _individualPayoutIntents.InsertOrUpdateGraph(x);
                            }

                            var updatedPayouts = _individualPayoutIntents.Commit();
                            _logger.LogDebug(GetLogMessage("{0} Individual Payouts records updated"), updatedPayouts);

                            if (updatedPayouts > 0)
                            {
                                retVal.Succeeded = true;
                                retVal.Amount = stripeTransfer.Amount;

                                await Task.Run(() =>
                                {
                                    RaiseEvent(new FundsDispersedToPersonEvent()
                                    {
                                        InvoiceId = invoiceId,
                                        PersonId = personId,
                                        Amount = stripeTransfer.Amount
                                    });
                                });
                            }
                        }

                    }

                    var organizationPayouts = 0;
                    if (transfer.Metadata.ContainsKey("organization-payout-ids"))
                    {
                        if (Guid.TryParse(transfer.Metadata["organization-id"], out var organizationId))
                        {
                            _logger.LogInformation(GetLogMessage("Processing Organization Payouts"));

                            var payoutIds = transfer.Metadata["organization-payout-ids"].Split(",").Select(Guid.Parse).ToArray();
                            var payouts = await _organizationPayoutIntents.Queryable()
                                .Where(x => payoutIds.Contains(x.Id) && x.OrganizationId == organizationId && x.InvoiceTransferId == null)
                                .ToListAsync();

                            _logger.LogDebug(GetLogMessage("PayoutIds:{0}"), payoutIds);
                            
                            foreach (var x in payouts)
                            {
                                _logger.LogInformation(GetLogMessage("Intent Id: {0}; Transfer Id: {1}"), x.Id, x.InvoiceTransferId);

                                x.InvoiceTransferId = transfer.Id;
                                x.ObjectState = ObjectState.Modified;

                                _organizationPayoutIntents.InsertOrUpdateGraph(x);

                            }


                            var records = _organizationPayoutIntents.Commit();

                            _logger.LogDebug(GetLogMessage("{0} Organization Payout records updated"), records);

                            if (records > 0)
                            {
                                organizationPayouts++;
                            }

                            _logger.LogDebug(GetLogMessage("{0} Organization payouts updated"), organizationPayouts);

                            if (organizationPayouts > 0)
                            {
                                retVal.Succeeded = true;
                                retVal.Amount = stripeTransfer.Amount;

                                await Task.Run(() =>
                                {
                                    RaiseEvent(new FundsDispersedToOrganizationEvent()
                                    {
                                        InvoiceId = invoiceId,
                                        OrganizationId = organizationId,
                                        Amount = stripeTransfer.Amount
                                    });
                                });
                            }
                        };
                    }
                }
                else
                {
                    _logger.LogDebug(GetLogMessage("First update failed"));
                }


            }
            else
            {
                _logger.LogDebug(GetLogMessage("Not an invoice transfer"));
            }


            return retVal;
        }



    }
}
