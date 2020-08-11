// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.EmailSending.Services;
using AgencyPro.Core.Invoices.Emails;
using AgencyPro.Core.Invoices.Events;
using AgencyPro.Core.Templates.Models;
using AgencyPro.Data.EFCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Invoices.Messaging
{
    public partial class InvoicesEventHandlers
    {
        private void InvoiceCreatedSendAgencyOwnerEmail(string invoiceId)
        {
            _logger.LogInformation(GetLogMessage("Invoice: {0}"), invoiceId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var invoice = context.Invoices.Where(x => x.InvoiceId == invoiceId)
                    .ProjectTo<AgencyOwnerInvoiceEmail>(ProjectionMapping)
                    .First();

                invoice.Initialize(Settings);


                Send(TemplateTypes.AgencyOwnerInvoiceCreated, invoice,
                    $@"[{invoice.ProviderOrganizationName}] Invoice created");
            }

           

        }

        private void InvoiceCreatedSendAccountManagerEmail(string invoiceId)
        {
            _logger.LogInformation(GetLogMessage("Invoice: {0}"), invoiceId);

            using (var context = new AppDbContext(DbContextOptions))
            {


                var invoice = context.Invoices.Where(x => x.InvoiceId == invoiceId)
                    .ProjectTo<AgencyOwnerInvoiceEmail>(ProjectionMapping)
                    .First();

                invoice.Initialize(Settings);


                Send(TemplateTypes.AccountManagerInvoiceCreated, invoice,
                    $@"[{invoice.ProviderOrganizationName}] Invoice created");
            }

        }

        public void Handle(InvoiceCreatedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Invoice Created Event Triggered"));
            
            Parallel.Invoke(new List<Action>
            {
                () => InvoiceCreatedSendAccountManagerEmail(evt.InvoiceId),
                () => InvoiceCreatedSendAgencyOwnerEmail(evt.InvoiceId)
            }.ToArray());
        }
    }
}