// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;
using AgencyPro.Core.Chart.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.TimeEntries.ViewModels;

namespace AgencyPro.Core.Chart.Services
{
    public interface  IChartService
    {
        Task<ProviderAgencyOwnerChartOutput> GetProviderChartData(IProviderAgencyOwner owner, TimeMatrixFilters filters, ChartParams chartParams);
        Task<MarketingAgencyOwnerChartOutput> GetMarketingChartData(IMarketingAgencyOwner owner, TimeMatrixFilters filters, ChartParams chartParams);
        Task<RecruitingAgencyOwnerChartOutput> GetRecruitingChartData(IRecruitingAgencyOwner owner, TimeMatrixFilters filters, ChartParams chartParams);

        Task<AccountManagerChartOutput> GetProviderChartData(IOrganizationAccountManager owner, TimeMatrixFilters filters, ChartParams chartParams);
        Task<ProjectManagerChartOutput> GetProviderChartData(IOrganizationProjectManager pm, TimeMatrixFilters filters, ChartParams chartParams);
        Task<CustomerChartOutput> GetProviderChartData(IOrganizationCustomer cu, TimeMatrixFilters filters, ChartParams chartParams);
        Task<ContractorChartOutput> GetProviderChartData(IOrganizationContractor co, TimeMatrixFilters filters, ChartParams chartParams);
        Task<RecruiterChartOutput> GetProviderChartData(IOrganizationRecruiter re, TimeMatrixFilters filters, ChartParams chartParams);
        Task<MarketerChartOutput> GetProviderChartData(IOrganizationMarketer ma, TimeMatrixFilters filters, ChartParams chartParams);
    }
}
