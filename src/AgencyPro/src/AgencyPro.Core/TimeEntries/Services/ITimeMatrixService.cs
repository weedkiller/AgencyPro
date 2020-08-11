// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.TimeEntries.ViewModels;
using AgencyPro.Core.TimeEntries.ViewModels.TimeMatrix;

namespace AgencyPro.Core.TimeEntries.Services
{
    public interface ITimeMatrixService
    {
        Task<AccountManagerTimeMatrixComposedOutput> GetComposedOutput(IOrganizationAccountManager am, TimeMatrixFilters filters);
        Task<ProjectManagerTimeMatrixComposedOutput> GetComposedOutput(IOrganizationProjectManager pm, TimeMatrixFilters filters);
        Task<CustomerTimeMatrixComposedOutput> GetComposedOutput(IOrganizationCustomer cu, TimeMatrixFilters filters);
        Task<ContractorTimeMatrixComposedOutput> GetComposedOutput(IOrganizationContractor co, TimeMatrixFilters filters);
        Task<RecruiterTimeMatrixComposedOutput> GetComposedOutput(IOrganizationRecruiter re, TimeMatrixFilters filters);
        Task<MarketerTimeMatrixComposedOutput> GetComposedOutput(IOrganizationMarketer ma, TimeMatrixFilters filters);

        Task<ProviderAgencyOwnerTimeMatrixComposedOutput> GetProviderAgencyComposedOutput(IProviderAgencyOwner owner, TimeMatrixFilters filters);
        Task<MarketingAgencyOwnerTimeMatrixComposedOutput> GetMarketingAgencyComposedOutput(IMarketingAgencyOwner owner, TimeMatrixFilters filters);
        Task<RecruitingAgencyOwnerTimeMatrixComposedOutput> GetRecruitingAgencyComposedOutput(IRecruitingAgencyOwner owner, TimeMatrixFilters filters);
       
        
    }
}