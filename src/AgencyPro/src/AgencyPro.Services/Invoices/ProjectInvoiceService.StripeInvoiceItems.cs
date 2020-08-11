// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Invoices.ViewModels;
using AgencyPro.Core.Stripe.Model;
using AgencyPro.Core.TimeEntries.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;
using Stripe;

namespace AgencyPro.Services.Invoices
{
    public partial class ProjectInvoiceService
    {
        public async Task<InvoiceItemResult> InvoiceItemCreated(InvoiceItem invoiceItem)
        {
            _logger.LogInformation(GetLogMessage("Item Id: {0}"), invoiceItem.Id);

            var retVal = new InvoiceItemResult()
            {
                InvoiceItemId = invoiceItem.Id
            };


            var entity = new StripeInvoiceItem
            {
                ObjectState = ObjectState.Added,
                Id = invoiceItem.Id,
                Created = invoiceItem.Date,
                PeriodStart = invoiceItem.Period.Start,
                PeriodEnd = invoiceItem.Period.End,
                IsDeleted = invoiceItem.Deleted.GetValueOrDefault()
            };

            entity.InjectFrom(invoiceItem);


            if (invoiceItem.Metadata.ContainsKey("contract-id"))
            {
                _logger.LogDebug(GetLogMessage("contract id found in metadata: {0}"),
                    invoiceItem.Metadata["contract-id"]);


                entity.ContractId = Guid.Parse(invoiceItem.Metadata["contract-id"]);
                retVal.ContractId = entity.ContractId.Value;
                
            }
            else
            {
                _logger.LogDebug(GetLogMessage("Contract id not found in metadata"));
            }


            var entityRecords = _items.InsertOrUpdateGraph(entity, true);

            _logger.LogDebug(GetLogMessage("{0} records updated"), entityRecords);

            if (entityRecords > 0)
            {
                retVal.Succeeded = true;
            }

            var contract = await _contracts
                .Queryable()
                .Include(x => x.TimeEntries)
                .Where(x => x.Id == retVal.ContractId)
                .FirstAsync();

            var timeEntriesToProcess = contract
                .TimeEntries
                .Where(x => x.Status == TimeStatus.Approved && x.InvoiceItemId == null)
                .ToList();

            _logger.LogDebug(GetLogMessage("Ready to process {0} time entries"), timeEntriesToProcess.Count);


            foreach (var entry in timeEntriesToProcess)
            {
                _logger.LogDebug(GetLogMessage("Entry: {0}; Invoice Item: {1}"), entry.Id, invoiceItem.Id);

                entry.InvoiceItemId = invoiceItem.Id;
                entry.ObjectState = ObjectState.Modified;
                entry.Updated = DateTimeOffset.UtcNow;
            }

            if (timeEntriesToProcess.Count > 0)
            {
                _logger.LogDebug(GetLogMessage("Preparing to update database with updated data"));

                retVal.TimeEntriesUpdated = timeEntriesToProcess.Count;
                var records = _contracts.InsertOrUpdateGraph(contract, true);
                _logger.LogDebug(GetLogMessage("{0} Records Updated"), records);
            }

            return retVal;
        }

        public async Task<InvoiceItemResult> InvoiceItemDeleted(InvoiceItem invoiceItem)
        {
            _logger.LogInformation(GetLogMessage("Item: {0}"), invoiceItem.Id);

            var retVal = new InvoiceItemResult()
            {
                InvoiceItemId = invoiceItem.Id
            };

            if (invoiceItem.Metadata.ContainsKey("contract-id"))
            {
                _logger.LogDebug(GetLogMessage("contract id found in metadata: {0}"),
                    invoiceItem.Metadata["contract-id"]);

                retVal.ContractId = Guid.Parse(invoiceItem.Metadata["contract-id"]);

                var entity = await _items.Queryable()
                    .Where(x => x.Id == invoiceItem.Id)
                    .FirstAsync();

                entity.IsDeleted = true;
                entity.Updated = DateTimeOffset.Now;
                entity.ObjectState = ObjectState.Modified;

                var records = _items.InsertOrUpdateGraph(entity, true);
                _logger.LogDebug(GetLogMessage("{0} records updated"), records);

                if (records > 0)
                {
                    retVal.Succeeded = true;
                }
            }
            else
            {
                _logger.LogDebug(GetLogMessage("Contract id not found in metadata"));
            }

            return retVal;
        }

        public async Task<InvoiceItemResult> InvoiceItemUpdated(InvoiceItem invoiceItem)
        {
            _logger.LogInformation(GetLogMessage("Item ID: {0}; Invoice ID: {1}"), invoiceItem.Id,
                invoiceItem.InvoiceId);
            
            var retVal = new InvoiceItemResult()
            {
                InvoiceItemId = invoiceItem.Id
            };
        
            var inv = _invoices
                .Queryable()
                .Any(x => x.Id == invoiceItem.InvoiceId);


            _logger.LogDebug(GetLogMessage("Invoice Found Locally: {0}"), inv);

            if (inv)
            {
                var entity = await _items.Queryable()
                    .Where(x => x.Id == invoiceItem.Id)
                    .FirstAsync();
                
                if (invoiceItem.Metadata.ContainsKey("contract-id"))
                {
                    _logger.LogDebug(GetLogMessage("contract id found in metadata: {0}"),
                        invoiceItem.Metadata["contract-id"]);


                    var contractId = Guid.Parse(invoiceItem.Metadata["contract-id"]);
                    retVal.ContractId = contractId;


                }
                else
                {
                    _logger.LogDebug(GetLogMessage("Contract id not found in metadata"));
                }


                entity.InjectFrom(invoiceItem);

                entity.InvoiceId = invoiceItem.InvoiceId;
                entity.IsDeleted = invoiceItem.Deleted.GetValueOrDefault();

                entity.ObjectState = ObjectState.Modified;

                var result = _items.InsertOrUpdateGraph(entity, true);

                _logger.LogDebug(GetLogMessage("{0} results updated"), result);

                if (result > 0)
                {
                    retVal.Succeeded = true;
                }
            }
            
            return retVal;
        }
    }
}