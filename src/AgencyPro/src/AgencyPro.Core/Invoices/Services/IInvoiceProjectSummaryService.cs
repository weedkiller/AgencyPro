// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Invoices.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;

namespace AgencyPro.Core.Invoices.Services
{
    public interface IInvoiceProjectSummaryService
    {
        Task<ProjectInvoiceSummary> GetProjectInvoiceSummary(IProviderAgencyOwner agencyOwner);
        Task<ProjectInvoiceDetails> GetProjectInvoiceDetails(IProviderAgencyOwner agencyOwner, Guid projectId);
    }
}