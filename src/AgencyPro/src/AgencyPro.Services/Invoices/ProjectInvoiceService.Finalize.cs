// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Invoices.Events;
using AgencyPro.Core.Invoices.Models;
using AgencyPro.Core.Invoices.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.TimeEntries.Enums;
using AgencyPro.Core.TimeEntries.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;
using Stripe;

namespace AgencyPro.Services.Invoices
{
    public partial class ProjectInvoiceService
    {
        private async Task<InvoiceResult> FinalizeInvoice([NotNull] ProjectInvoice entity)
        {
            _logger.LogInformation($@"Finalizing Invoice: {entity.InvoiceId}");
            var retVal = new InvoiceResult()
            {
                InvoiceId = entity.InvoiceId,
                ProjectId = entity.ProjectId
            };

            var invoice = _invoiceService.FinalizeInvoice(entity.InvoiceId, new InvoiceFinalizeOptions());

            _logger.LogInformation($@"Invoice Status: {invoice.Status}");

            if (invoice.Status == "open")
            {
                retVal.Succeeded = true;
                await Task.Run(() => RaiseEvent(new InvoiceFinalizedEvent()
                {
                    InvoiceId = entity.InvoiceId
                }));
            }

            return retVal;
        }

        public async Task<InvoiceResult> FinalizeInvoice(IProviderAgencyOwner agencyOwner, string invoiceId)
        {
            _logger.LogInformation($@"Finalizing Invoice: {invoiceId}");

            var entity = await Repository.Queryable()
                .Include(x=>x.Project)
                .Where(x => x.InvoiceId == invoiceId && x.ProviderOrganizationId == agencyOwner.OrganizationId)
                .FirstOrDefaultAsync();

            return await FinalizeInvoice(entity);
        }

        public async Task<InvoiceResult> FinalizeInvoice(IOrganizationAccountManager am, string invoiceId)
        {
            _logger.LogInformation($@"Finalizing Invoice: {invoiceId}");

            var entity = await Repository.Queryable()
                .Where(x => x.InvoiceId == invoiceId)
                .FirstOrDefaultAsync();

           return await FinalizeInvoice(entity);
        }


        public Task<InvoiceResult> InvoiceFinalized(Invoice invoice)
        {
            _logger.LogInformation(GetLogMessage("Invoice: {0}"), invoice.Id);

            var retVal = new InvoiceResult()
            {
                InvoiceId = invoice.Id
            };

            if (invoice.Metadata.ContainsKey("proj_id"))
            {
                retVal.ProjectId = Guid.Parse(invoice.Metadata["proj_id"]);

                var entity = _invoices
                    .Queryable()
                    .Include(x => x.Items)
                    .ThenInclude(x => x.TimeEntries)
                    .First(x => x.Id == invoice.Id);

                entity.InjectFrom(invoice);

                entity.Updated = DateTimeOffset.UtcNow;
                entity.Id = invoice.Id;
                entity.AmountDue = Convert.ToDecimal(invoice.AmountDue / 100m);
                entity.AmountRemaining = Convert.ToDecimal(invoice.AmountRemaining / 100m);
                entity.Attempted = invoice.Attempted;
                entity.CustomerId = invoice.CustomerId;
                entity.InvoicePdf = invoice.InvoicePdf;
                entity.Status = invoice.Status;
                entity.SubscriptionId = invoice.SubscriptionId;
                entity.AmountPaid = Convert.ToDecimal(invoice.AmountPaid / 100m);
                entity.Total = Convert.ToDecimal(invoice.Total / 100m);
                entity.Subtotal = Convert.ToDecimal(invoice.Subtotal / 100m);
                entity.HostedInvoiceUrl = invoice.HostedInvoiceUrl;
                entity.BillingReason = invoice.BillingReason;
                entity.AttemptCount = invoice.AttemptCount;
                entity.Attempted = invoice.Attempted;
                entity.Number = invoice.Number;
                entity.ObjectState = ObjectState.Modified;

                var processableTimeEntries = entity.Items.SelectMany(x => x.TimeEntries).ToList();

                _logger.LogDebug(GetLogMessage("Time Entries to Process: {0}"), processableTimeEntries.Count);

                foreach (var timeEntry in processableTimeEntries)
                {
                    var originalStatus = timeEntry.Status;

                    if (originalStatus != TimeStatus.InvoiceSent)
                    {
                        timeEntry.Status = TimeStatus.InvoiceSent;
                        timeEntry.Updated = DateTimeOffset.UtcNow;
                        timeEntry.ObjectState = ObjectState.Modified;

                        timeEntry.StatusTransitions.Add(new TimeEntryStatusTransition()
                        {
                            Status = TimeStatus.InvoiceSent,
                            ObjectState = ObjectState.Added
                        });
                    }
                }

                var records = _invoices.InsertOrUpdateGraph(entity, true);

                _logger.LogDebug(GetLogMessage("{0} Records updated"), records);

                if (records > 0)
                {
                    retVal.Succeeded = true;
                }
            }

            return Task.FromResult(retVal);
        }

    }
}