// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using AgencyPro.Core.Invoices.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using AgencyPro.Core.Invoices.Events;
using AgencyPro.Core.Invoices.Extensions;
using AgencyPro.Core.Invoices.Models;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace AgencyPro.Services.Invoices
{
    public partial class ProjectInvoiceService
    {
        private async Task<InvoiceResult> VoidInvoice(ProjectInvoice invoice)
        {
            var retVal = new InvoiceResult()
            {
                InvoiceId = invoice.InvoiceId
            };

            var service = new InvoiceService();
            var result = service.VoidInvoice(invoice.InvoiceId);
            if (result.Status == "void")
            {
                retVal.Succeeded = true;
                await Task.Run(() => RaiseEvent(new InvoiceVoidedEvent()
                {
                   InvoiceId = invoice.InvoiceId
                }));
            }

            return retVal;
        }

        public async Task<InvoiceResult> VoidInvoice(IProviderAgencyOwner ao, string invoiceId)
        {
            _logger.LogInformation(GetLogMessage("AO:{0};Invoice:{1}"), ao.OrganizationId, invoiceId);

            var invoice = await Repository.Queryable()
                .Include(x=>x.Invoice)
                .ForAgencyOwner(ao)
                .Where(x => x.InvoiceId == invoiceId)
                .FirstAsync();

            return await VoidInvoice(invoice);
        }

    }
}