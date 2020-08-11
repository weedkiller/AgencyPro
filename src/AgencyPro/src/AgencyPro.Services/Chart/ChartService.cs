// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Chart;
using AgencyPro.Core.Chart.Enums;
using AgencyPro.Core.Chart.Services;
using AgencyPro.Core.Chart.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationAccountManagers;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationProjectManagers;
using AgencyPro.Core.Projects.ViewModels;
using AgencyPro.Core.TimeEntries.Services;
using AgencyPro.Core.TimeEntries.ViewModels;
using AgencyPro.Core.TimeEntries.ViewModels.TimeMatrix;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyPro.Services.Chart
{
    public partial class ChartService : IChartService
    {

        private readonly ITimeMatrixService _matrixService;
        public ChartService(ITimeMatrixService matrixService)
        {
            _matrixService = matrixService;
        }


        public async Task<MarketerChartOutput> GetProviderChartData(IOrganizationMarketer marketer, TimeMatrixFilters filters, ChartParams chartParams)
        {
            var result = await _matrixService.GetComposedOutput(marketer, filters);
            var co = result.Matrix.ToCoData<MarketerTimeMatrixOutput, MarketerOrganizationContractorOutput, MarketerChartDataItem>(result.Contractors).
                                    FillMissingDays(chartParams.DateBreakdown, filters).
                                    TopLevelGrouping(chartParams.DateBreakdown).
                                    SecondLevelGrouping(chartParams.DateBreakdown);
            return new MarketerChartOutput
            {
                Co = co,
                CurrentBreakdown = "co",
                CurrentDateRange = chartParams.DateBreakdown == DateBreakdown.ByMonth ? "m0" : "w0",
                DateRanges = GetDateRange(chartParams.DateBreakdown, co),
                Breakdowns = new Dictionary<string, string>{
                                    {"co", "By Contractor"}
                              }
            };
        }



        public async Task<RecruiterChartOutput> GetProviderChartData(IOrganizationRecruiter recruiter, TimeMatrixFilters filters, ChartParams chartParams)
        {
            var result = await _matrixService.GetComposedOutput(recruiter, filters);

            var am = result.Matrix.ToAmData<RecruiterTimeMatrixOutput, RecruiterOrganizationAccountManagerOutput, RecruiterChartDataItem>(result.AccountManagers).
                                    FillMissingDays(chartParams.DateBreakdown, filters).
                                    TopLevelGrouping(chartParams.DateBreakdown).
                                    SecondLevelGrouping(chartParams.DateBreakdown);

            var co = result.Matrix.ToCoData<RecruiterTimeMatrixOutput, RecruiterOrganizationContractorOutput, RecruiterChartDataItem>(result.Contractors).
                                    FillMissingDays(chartParams.DateBreakdown, filters).
                                    TopLevelGrouping(chartParams.DateBreakdown).
                                    SecondLevelGrouping(chartParams.DateBreakdown);


            return new RecruiterChartOutput
            {
                Co = co,
                Am = am,
                CurrentBreakdown = "co",
                CurrentDateRange = chartParams.DateBreakdown == DateBreakdown.ByMonth ? "m0" : "w0",
                DateRanges = GetDateRange(chartParams.DateBreakdown, co),
                Breakdowns = new Dictionary<string, string>{
                                    {"am", "By Account Manager"},
                                    {"co", "By Contractor"}
                              }
            };
        }


        public async Task<ContractorChartOutput> GetProviderChartData(IOrganizationContractor contractor, TimeMatrixFilters filters, ChartParams chartParams)
        {
            var result = await _matrixService.GetComposedOutput(contractor, filters);

            var status = result.Matrix.ToStatusData<ContractorTimeMatrixOutput, ContractorChartDataItem>().
                FillMissingDays(chartParams.DateBreakdown, filters).
                TopLevelGrouping(chartParams.DateBreakdown).
                SecondLevelGrouping(chartParams.DateBreakdown);


            var projs = result.Matrix.ToProjData<ContractorTimeMatrixOutput, ContractorProjectOutput, ContractorChartDataItem>(result.Projects).
                                    FillMissingDays(chartParams.DateBreakdown, filters).
                                    TopLevelGrouping(chartParams.DateBreakdown).
                                    SecondLevelGrouping(chartParams.DateBreakdown);

      
            return new ContractorChartOutput
            {
                Proj = projs,
                Status = status,
                CurrentBreakdown = "status",
                CurrentDateRange = chartParams.DateBreakdown == DateBreakdown.ByMonth ? "m0" : "w0",
                DateRanges = GetDateRange(chartParams.DateBreakdown, projs),
                Breakdowns = new Dictionary<string, string>{
                                    {"proj", "By Project"},
                                    {"status", "By Status" }
                              }
            };
        }


        public async Task<CustomerChartOutput> GetProviderChartData(IOrganizationCustomer cu, TimeMatrixFilters filters, ChartParams chartParams)
        {
            var result = await _matrixService.GetComposedOutput(cu, filters);

            var status = result.Matrix.ToStatusData<CustomerTimeMatrixOutput, CustomerChartDataItem>().
                FillMissingDays(chartParams.DateBreakdown, filters).
                TopLevelGrouping(chartParams.DateBreakdown).
                SecondLevelGrouping(chartParams.DateBreakdown);

            var projs = result.Matrix.ToProjData<CustomerTimeMatrixOutput, CustomerProjectOutput, CustomerChartDataItem>(result.Projects).
                                    FillMissingDays(chartParams.DateBreakdown, filters).
                                    TopLevelGrouping(chartParams.DateBreakdown).
                                    SecondLevelGrouping(chartParams.DateBreakdown);
            var am = result.Matrix.ToAmData<CustomerTimeMatrixOutput, CustomerOrganizationAccountManagerOutput, CustomerChartDataItem>(result.AccountManagers).
                                    FillMissingDays(chartParams.DateBreakdown, filters).
                                    TopLevelGrouping(chartParams.DateBreakdown).
                                    SecondLevelGrouping(chartParams.DateBreakdown);
            var pm = result.Matrix.ToPmData<CustomerTimeMatrixOutput, CustomerOrganizationProjectManagerOutput, CustomerChartDataItem>(result.ProjectManagers).
                                    FillMissingDays(chartParams.DateBreakdown, filters).
                                    TopLevelGrouping(chartParams.DateBreakdown).
                                    SecondLevelGrouping(chartParams.DateBreakdown);
            var co = result.Matrix.ToCoData<CustomerTimeMatrixOutput, CustomerOrganizationContractorOutput, CustomerChartDataItem>(result.Contractors).
                                    FillMissingDays(chartParams.DateBreakdown, filters).
                                    TopLevelGrouping(chartParams.DateBreakdown).
                                    SecondLevelGrouping(chartParams.DateBreakdown);


            return new CustomerChartOutput
            {
                Proj = projs,
                Pm = pm,
                Am = am,
                Co = co,
                Status = status,
                CurrentBreakdown = "co",
                CurrentDateRange = chartParams.DateBreakdown == DateBreakdown.ByMonth ? "m0" : "w0",
                DateRanges = GetDateRange(chartParams.DateBreakdown, co),
                Breakdowns = new Dictionary<string, string>{
                                    {"proj", "By Project"},
                                    {"am", "By Account Manager"},
                                    {"pm", "By Project Manager"},
                                    {"co", "By Contractor"},
                                    {"status", "By Status" }
                              }
            };
        }


        public async Task<ProjectManagerChartOutput> GetProviderChartData(IOrganizationProjectManager projManager, TimeMatrixFilters filters, ChartParams chartParams)
        {
            var result = await _matrixService.GetComposedOutput(projManager, filters);

            var status = result.Matrix.ToStatusData<ProjectManagerTimeMatrixOutput, ProjectManagerChartDataItem>().
                FillMissingDays(chartParams.DateBreakdown, filters).
                TopLevelGrouping(chartParams.DateBreakdown).
                SecondLevelGrouping(chartParams.DateBreakdown);


            var proj = result.Matrix.ToProjData<ProjectManagerTimeMatrixOutput, ProjectManagerProjectOutput, ProjectManagerChartDataItem>(result.Projects).
                                    FillMissingDays(chartParams.DateBreakdown, filters).
                                    TopLevelGrouping(chartParams.DateBreakdown).
                                    SecondLevelGrouping(chartParams.DateBreakdown);

            var am = result.Matrix.ToAmData<ProjectManagerTimeMatrixOutput, ProjectManagerOrganizationAccountManagerOutput, ProjectManagerChartDataItem>(result.AccountManagers).
                                    FillMissingDays(chartParams.DateBreakdown, filters).
                                    TopLevelGrouping(chartParams.DateBreakdown).
                                    SecondLevelGrouping(chartParams.DateBreakdown);

            var co = result.Matrix.ToCoData<ProjectManagerTimeMatrixOutput, ProjectManagerOrganizationContractorOutput, ProjectManagerChartDataItem>(result.Contractors).
                                    FillMissingDays(chartParams.DateBreakdown, filters).
                                    TopLevelGrouping(chartParams.DateBreakdown).
                                    SecondLevelGrouping(chartParams.DateBreakdown);


            return new ProjectManagerChartOutput
            {
                Proj = proj,
                Am = am,
                Co = co,
                Status = status,
                CurrentBreakdown = "co",
                CurrentDateRange = chartParams.DateBreakdown == DateBreakdown.ByMonth ? "m0" : "w0",
                DateRanges = GetDateRange(chartParams.DateBreakdown, co),
                Breakdowns = new Dictionary<string, string>{
                                    {"proj", "By Project"},
                                    {"am", "By Account Manager"},
                                    {"co", "By Contractor"},
                                    {"status", "By Status" }
                              }
            };
        }


        public async Task<AccountManagerChartOutput> GetProviderChartData(IOrganizationAccountManager accountManager, TimeMatrixFilters filters, ChartParams chartParams)
        {
            var result = await _matrixService.GetComposedOutput(accountManager, filters);

            var status = result.Matrix.ToStatusData<AccountManagerTimeMatrixOutput, AccountManagerChartDataItem>().
                FillMissingDays(chartParams.DateBreakdown, filters).
                TopLevelGrouping(chartParams.DateBreakdown).
                SecondLevelGrouping(chartParams.DateBreakdown);

            var projs = result.Matrix.ToProjData<AccountManagerTimeMatrixOutput, AccountManagerProjectOutput, AccountManagerChartDataItem>(result.Projects).
                                    FillMissingDays(chartParams.DateBreakdown, filters).
                                    TopLevelGrouping(chartParams.DateBreakdown).
                                    SecondLevelGrouping(chartParams.DateBreakdown);
            
            //var re = result.Matrix.ToReData<AccountManagerTimeMatrixOutput, AccountManagerOrganizationRecruiterOutput, AccountManagerChartDataItem>(result.Recruiters).
            //                        FillMissingDays(chartParams.DateBreakdown, filters).
            //                        TopLevelGrouping(chartParams.DateBreakdown).
            //                        SecondLevelGrouping(chartParams.DateBreakdown);

            //var ma = result.Matrix.ToMaData<AccountManagerTimeMatrixOutput, AccountManagerOrganizationMarketerOutput, AccountManagerChartDataItem>(result.Marketers).
            //                        FillMissingDays(chartParams.DateBreakdown, filters).
            //                        TopLevelGrouping(chartParams.DateBreakdown).
            //                        SecondLevelGrouping(chartParams.DateBreakdown);

            var pm = result.Matrix.ToPmData<AccountManagerTimeMatrixOutput, AccountManagerOrganizationProjectManagerOutput, AccountManagerChartDataItem>(result.ProjectManagers).
                                    FillMissingDays(chartParams.DateBreakdown, filters).
                                    TopLevelGrouping(chartParams.DateBreakdown).
                                    SecondLevelGrouping(chartParams.DateBreakdown);

            var co = result.Matrix.ToCoData<AccountManagerTimeMatrixOutput, AccountManagerOrganizationContractorOutput, AccountManagerChartDataItem>(result.Contractors).
                                    FillMissingDays(chartParams.DateBreakdown, filters).
                                    TopLevelGrouping(chartParams.DateBreakdown).
                                    SecondLevelGrouping(chartParams.DateBreakdown);


            return new AccountManagerChartOutput
            {
                Proj = projs,
               // Re = re,
                Pm = pm,
                Co = co,
                //Ma = ma,
                Status = status,
                CurrentBreakdown = "co",
                CurrentDateRange = chartParams.DateBreakdown == DateBreakdown.ByMonth ? "m0" : "w0",
                DateRanges = GetDateRange(chartParams.DateBreakdown, co),
                Breakdowns = new Dictionary<string, string>{
                                    {"proj", "By Project"},
                                    {"pm", "By Project Manager"},
                                    {"co", "By Contractor"},
                                    {"status", "By Status"},
                }
            };
        }

        Dictionary<string, string> GetDateRange<T>(DateBreakdown dateBreakdown, Dictionary<string, Dictionary<string, List<T>>> data)
            where T : ChartDataItem
        {
            var range = new Dictionary<string, string>();
            if (dateBreakdown == DateBreakdown.ByMonth)
            {
                range.TryAdd("m0", "This Month");
                range.TryAdd("m1", "1 Month ago");
                for (var x = 2; x < data.Keys.ToList().Count(); x++)
                {
                    range.TryAdd($"m{x}", $"{x} Months ago");
                }
            }
            else
            {
                range.TryAdd("w0", "This Week");
                range.TryAdd("w1", "1 Week ago");
                for (var x = 2; x < data.Keys.ToList().Count(); x++)
                {
                    range.TryAdd($"w{x}", $"{x} Weeks ago");
                }
            }
            return range;
        }
    }
}

