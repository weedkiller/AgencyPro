// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Chart;
using AgencyPro.Core.Chart.Enums;
using AgencyPro.Core.Chart.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationAccountManagers;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationMarketers;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationProjectManagers;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationRecruiters;
using AgencyPro.Core.Projects.ViewModels;
using AgencyPro.Core.TimeEntries.ViewModels;
using AgencyPro.Core.TimeEntries.ViewModels.TimeMatrix;

namespace AgencyPro.Services.Chart
{
    public partial class ChartService
    {
        public async Task<ProviderAgencyOwnerChartOutput> GetProviderChartData(IProviderAgencyOwner owner,
            TimeMatrixFilters filters, ChartParams chartParams)
        {
            var result = await _matrixService.GetProviderAgencyComposedOutput(owner, filters);

            var status = result.Matrix
                .ToStatusData<ProviderAgencyOwnerTimeMatrixOutput, ProviderAgencyOwnerChartDataItem>()
                .FillMissingDays(chartParams.DateBreakdown, filters).TopLevelGrouping(chartParams.DateBreakdown)
                .SecondLevelGrouping(chartParams.DateBreakdown);

            var projs = result.Matrix
                .ToProjData<ProviderAgencyOwnerTimeMatrixOutput, AgencyOwnerProjectOutput,
                    ProviderAgencyOwnerChartDataItem>(result.Projects)
                .FillMissingDays(chartParams.DateBreakdown, filters).TopLevelGrouping(chartParams.DateBreakdown)
                .SecondLevelGrouping(chartParams.DateBreakdown);

            var am = result.Matrix
                .ToAmData<ProviderAgencyOwnerTimeMatrixOutput, AgencyOwnerOrganizationAccountManagerOutput,
                    ProviderAgencyOwnerChartDataItem>(result.AccountManagers)
                .FillMissingDays(chartParams.DateBreakdown, filters).TopLevelGrouping(chartParams.DateBreakdown)
                .SecondLevelGrouping(chartParams.DateBreakdown);
            
            var pm = result.Matrix
                .ToPmData<ProviderAgencyOwnerTimeMatrixOutput, AgencyOwnerOrganizationProjectManagerOutput,
                    ProviderAgencyOwnerChartDataItem>(result.ProjectManagers)
                .FillMissingDays(chartParams.DateBreakdown, filters).TopLevelGrouping(chartParams.DateBreakdown)
                .SecondLevelGrouping(chartParams.DateBreakdown);

            var co = result.Matrix
                .ToCoData<ProviderAgencyOwnerTimeMatrixOutput, AgencyOwnerOrganizationContractorOutput,
                    ProviderAgencyOwnerChartDataItem>(result.Contractors)
                .FillMissingDays(chartParams.DateBreakdown, filters).TopLevelGrouping(chartParams.DateBreakdown)
                .SecondLevelGrouping(chartParams.DateBreakdown);


            return new ProviderAgencyOwnerChartOutput
            {
                Proj = projs,
                Pm = pm,
                Co = co,
                Am = am,
                Status = status,
                CurrentBreakdown = "co",
                CurrentDateRange = chartParams.DateBreakdown == DateBreakdown.ByMonth ? "m0" : "w0",
                DateRanges = GetDateRange(chartParams.DateBreakdown, co),
                Breakdowns = new Dictionary<string, string>
                {
                    {"proj", "By Project"},
                    {"am", "By Account Manager"},
                    {"pm", "By Project Manager"},
                    {"co", "By Contractor"},
                    {"status", "By Status"}
                }
            };
        }

        public async Task<MarketingAgencyOwnerChartOutput> GetMarketingChartData(IMarketingAgencyOwner owner,
            TimeMatrixFilters filters, ChartParams chartParams)
        {
            var result = await _matrixService.GetMarketingAgencyComposedOutput(owner, filters);

            var status = result.Matrix
                .ToStatusData<MarketingAgencyOwnerTimeMatrixOutput, MarketingAgencyOwnerChartDataItem>()
                .FillMissingDays(chartParams.DateBreakdown, filters).TopLevelGrouping(chartParams.DateBreakdown)
                .SecondLevelGrouping(chartParams.DateBreakdown);

            
            var ma = result.Matrix.ToMaData<MarketingAgencyOwnerTimeMatrixOutput, AgencyOwnerOrganizationMarketerOutput, MarketingAgencyOwnerChartDataItem>(result.Marketers).
                                    FillMissingDays(chartParams.DateBreakdown, filters).
                                    TopLevelGrouping(chartParams.DateBreakdown).
                                    SecondLevelGrouping(chartParams.DateBreakdown);

            return new MarketingAgencyOwnerChartOutput
            {
                Ma = ma,
                Status = status,
                CurrentBreakdown = "ma",
                CurrentDateRange = chartParams.DateBreakdown == DateBreakdown.ByMonth ? "m0" : "w0",
                DateRanges = GetDateRange(chartParams.DateBreakdown, ma),
                Breakdowns = new Dictionary<string, string>
                {
                    {"ma", "By Marketer"},
                    {"status", "By Status"}
                }
            };
        }

        public async Task<RecruitingAgencyOwnerChartOutput> GetRecruitingChartData(IRecruitingAgencyOwner owner,
            TimeMatrixFilters filters, ChartParams chartParams)
        {
            var result = await _matrixService.GetRecruitingAgencyComposedOutput(owner, filters);

            var status = result.Matrix
                .ToStatusData<RecruitingAgencyOwnerTimeMatrixOutput, RecruitingAgencyOwnerChartDataItem>()
                .FillMissingDays(chartParams.DateBreakdown, filters).TopLevelGrouping(chartParams.DateBreakdown)
                .SecondLevelGrouping(chartParams.DateBreakdown);

           

            var re = result.Matrix.ToReData<RecruitingAgencyOwnerTimeMatrixOutput, AgencyOwnerOrganizationRecruiterOutput, RecruitingAgencyOwnerChartDataItem>(result.Recruiters).
                                    FillMissingDays(chartParams.DateBreakdown, filters).
                                    TopLevelGrouping(chartParams.DateBreakdown).
                                    SecondLevelGrouping(chartParams.DateBreakdown);
            

            return new RecruitingAgencyOwnerChartOutput
            {
                Re = re,
                Status = status,
                CurrentBreakdown = "re",
                CurrentDateRange = chartParams.DateBreakdown == DateBreakdown.ByMonth ? "m0" : "w0",
                DateRanges = GetDateRange(chartParams.DateBreakdown, re),
                Breakdowns = new Dictionary<string, string>
                {
                    {"re", "By Recruiter"},
                    {"status", "By Status"}
                }
            };
        }
    }
}