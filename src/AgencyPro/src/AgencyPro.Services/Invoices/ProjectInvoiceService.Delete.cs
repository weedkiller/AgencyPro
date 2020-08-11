// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Invoices.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;

namespace AgencyPro.Services.Invoices
{
    public partial class ProjectInvoiceService
    {
        public Task<InvoiceResult> DeleteInvoice(IProviderAgencyOwner agencyOwner, string invoiceId)
        {
            throw new NotImplementedException();
        }

        public Task<InvoiceResult> DeleteInvoice(IOrganizationAccountManager accountManager, string invoiceId)
        {
            throw new NotImplementedException();

        }
    }
}