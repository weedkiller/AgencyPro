// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using AgencyPro.Core.Invoices.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using Stripe;
using System.Threading.Tasks;
using AgencyPro.Core.Invoices.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Services.Invoices
{
    public partial class ProjectInvoiceService
    {
        private async Task<InvoiceResult> SendInvoice(string invoiceId)
        {
            var service = new InvoiceService();
            var invoice = await service
                .SendInvoiceAsync(invoiceId, new InvoiceSendOptions());

            return await InvoiceUpdated(invoice);
        }

        public async Task<InvoiceResult> SendInvoice(IProviderAgencyOwner agencyOwner, string invoiceId)
        {
            var invoice = await Repository.Queryable()
                .ForAgencyOwner(agencyOwner)
                .Where(x => x.InvoiceId == invoiceId)
                .FirstOrDefaultAsync();

            return await SendInvoice(invoice.InvoiceId);
        }

        public async Task<InvoiceResult> SendInvoice(IOrganizationAccountManager am, string invoiceId)
        {
            var invoice = await Repository.Queryable()
                .ForOrganizationAccountManager(am)
                .Where(x => x.InvoiceId == invoiceId)
                .FirstOrDefaultAsync();

            return await SendInvoice(invoice.InvoiceId);
        }
    }
}