// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgencyPro.Core.DisperseFunds.Events;
using AgencyPro.Core.DisperseFunds.ViewModels;
using AgencyPro.Core.Extensions;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stripe;

namespace AgencyPro.Services.DisperseFunds
{
    public partial class DisperseFundsService
    {
        private async Task<DisperseFundsResult> DisperseFundsToOrganization(IProviderAgencyOwner ao, string invoiceId)
        {
            _logger.LogInformation(GetLogMessage("Dispersing Funds for invoice: {0}"), invoiceId);
            var retVal = new DisperseFundsResult {InvoiceId = invoiceId};

            var organizationPayouts = await _organizationPayoutIntents
                .Queryable()
                .Include(x => x.InvoiceTransfer)
                .Include(x => x.InvoiceItem)
                .ThenInclude(x => x.Contract)
                .Include(x => x.InvoiceItem)
                .ThenInclude(x => x.TimeEntries)
                .Include(x => x.Organization)
                .ThenInclude(x => x.OrganizationFinancialAccount)
                .ThenInclude(x => x.FinancialAccount)
                .Where(x => x.InvoiceItem.InvoiceId == invoiceId &&
                            x.Organization.OrganizationFinancialAccount.FinancialAccount != null &&
                            x.Organization.OrganizationFinancialAccount.FinancialAccount.PayoutsEnabled &&
                            x.InvoiceTransfer == null)
                .ToListAsync();

            _logger.LogDebug(GetLogMessage("Payouts ready for payment: {0}"), organizationPayouts.Count);


            var totalTransfersMade = 0;
            var totalAmountTransferred = 0m;


            if (organizationPayouts.Count > 0)
            {
                var transfersToMake = new List<TransferCreateOptions>();

                var organizations = organizationPayouts.Select(x => x.Organization).Distinct().ToList();

                _logger.LogDebug(GetLogMessage("Unique Organizations: {0}"), organizations.Count);

                foreach (var organization in organizations)
                {
                    _logger.LogDebug(GetLogMessage("Processing Payouts for Organization: {0}"), organization.Id);

                    var orgPayouts = organizationPayouts
                        .Where(x => x.OrganizationId == organization.Id)
                        .ToList();

                    _logger.LogDebug(GetLogMessage("Number of Payouts: {0}"), orgPayouts.Count);

                    if (orgPayouts.Count > 0)
                    {

                        var orgPayoutIds = orgPayouts
                            .Select(x => x.Id)
                            .ToArray();

                        var payoutIdString = string.Join(",", orgPayoutIds);

                        var sb = new StringBuilder();
                        foreach (var payout in orgPayouts)
                        {
                            sb.AppendLine($@"{payout.Type.GetDescription()}: {payout.Amount:C}");
                        }

                        transfersToMake.Add(new TransferCreateOptions()
                        {
                            Amount = Convert.ToInt64(orgPayouts.Sum(x => x.Amount) * 100),
                            Currency = "usd",
                            Description = sb.ToString(),
                            Destination = organization.OrganizationFinancialAccount.FinancialAccountId,
                            TransferGroup = invoiceId,
                            Metadata = new Dictionary<string, string>()
                            {
                                {"invoice-id", invoiceId},
                                {"organization-payout-ids", payoutIdString},
                                { "organization-id", organization.Id.ToString() }

                            }
                        });
                    }
                    else
                    {
                        _logger.LogDebug(GetLogMessage("Skipping payouts"));
                    }

                }

                _logger.LogDebug(GetLogMessage("{0} Transfers to make"), transfersToMake.Count);

                foreach (var tran in transfersToMake)
                {
                    _logger.LogDebug(GetLogMessage("Transfer Amount: {0}"), tran.Amount);

                    var transfer = _transferService.Create(tran);

                    var transferResult = await TransferCreated(transfer);
                    _logger.LogDebug(GetLogMessage("Transfer Result: {@0}"), transferResult);
                    if (transferResult.Succeeded)
                    {
                        _logger.LogDebug(GetLogMessage("Transfer Creation Succeeded: {@result}"), transferResult);
                        totalAmountTransferred += transferResult.Amount;
                        totalTransfersMade += 1;
                    }

                    else
                    {
                        _logger.LogDebug(GetLogMessage("Transfer Creation Failed"));
                    }
                }

                if (totalTransfersMade > 0)
                {
                    retVal.Succeeded = true;
                    retVal.Amount = totalAmountTransferred;
                    retVal.TotalTransfersMade = totalTransfersMade;

                    await Task.Run(() =>
                    {
                        RaiseEvent(new FundsDispersedEvent()
                        {
                            InvoiceId = invoiceId,
                            TotalTransfersMade = totalTransfersMade
                        });
                    });
                }
            }


            return await Task.FromResult(retVal);
        }
    }
}