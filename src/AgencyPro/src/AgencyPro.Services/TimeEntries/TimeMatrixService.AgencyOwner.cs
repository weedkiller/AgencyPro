// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Contracts.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationAccountManagers;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationCustomers;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationMarketers;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationProjectManagers;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationRecruiters;
using AgencyPro.Core.Projects.ViewModels;
using AgencyPro.Core.Stories.ViewModels;
using AgencyPro.Core.TimeEntries.Extensions;
using AgencyPro.Core.TimeEntries.ViewModels;
using AgencyPro.Core.TimeEntries.ViewModels.TimeMatrix;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Services.TimeEntries
{
    public partial class TimeMatrixService
    {
        public async Task<ProviderAgencyOwnerTimeMatrixComposedOutput> GetProviderAgencyComposedOutput(IProviderAgencyOwner owner,
            TimeMatrixFilters filters)
        {
            // this is essentially the provider organization id
            filters.ProviderOrganizationId = owner.OrganizationId;

            var matrix = await _context.TimeMatrix.ApplyWhereFilters(filters)
                .ProjectTo<ProviderAgencyOwnerTimeMatrixOutput>(_mapperConfiguration)
                .ToListAsync();

            var uniqueProjectIds = matrix.Select(x => x.ProjectId).Distinct().ToArray();
            var uniqueContractIds = matrix.Select(x => x.ContractId).Distinct().ToArray();
            var uniqueContractorIds = matrix.Select(x => x.ContractorId).Distinct().ToArray();
            var uniqueAccountManagerIds = matrix.Select(x => x.AccountManagerId).Distinct().ToArray();
            var uniqueProjectManagerIds = matrix.Select(x => x.MarketerId).Distinct().ToArray();
            var uniqueCustomerIds = matrix.Select(x => x.CustomerId).Distinct().ToArray();
            var uniqueStoryIds = matrix
                .Where(x => x.StoryId != null).Select(x => x.StoryId).Distinct().ToArray();

            var projectTask = _projectService.GetProjects<AgencyOwnerProjectOutput>(owner, uniqueProjectIds);
            var contractsTask = _contractService.GetContracts<AgencyOwnerProviderContractOutput>(owner, uniqueContractIds);
            var accountManagers =
                _accountManagerService.GetForOrganization<AgencyOwnerOrganizationAccountManagerOutput>(
                    owner.OrganizationId, uniqueAccountManagerIds);
            var contractors =
                _contractorService.GetForOrganization<AgencyOwnerOrganizationContractorOutput>(owner.OrganizationId,
                    uniqueContractorIds);
            var projectManagers =
                _projectManagerService.GetForOrganization<AgencyOwnerOrganizationProjectManagerOutput>(
                    owner.OrganizationId, uniqueProjectManagerIds);
            var customers =
                _customerService.GetForOrganization<AgencyOwnerOrganizationCustomerOutput>(owner.OrganizationId,
                    uniqueCustomerIds);

            var stories = _storyService.GetStories<AgencyOwnerStoryOutput>(owner, uniqueStoryIds);

            Task.WaitAll(projectTask, contractsTask, accountManagers, contractors, projectManagers, customers);

            return new ProviderAgencyOwnerTimeMatrixComposedOutput
            {
                Matrix = matrix,
                Projects = projectTask.Result,
                Contracts = contractsTask.Result,
                AccountManagers = accountManagers.Result,
                Contractors = contractors.Result,
                ProjectManagers = projectManagers.Result,
                Customers = customers.Result,
                Stories = stories.Result
            };
        }

        public async Task<MarketingAgencyOwnerTimeMatrixComposedOutput> GetMarketingAgencyComposedOutput(IMarketingAgencyOwner owner,
            TimeMatrixFilters filters)
        {
            // this is essentially the provider organization id
            filters.MarketerOrganizationId = owner.OrganizationId;

            var matrix = await _context.TimeMatrix.ApplyWhereFilters(filters)
                .ProjectTo<MarketingAgencyOwnerTimeMatrixOutput>(_mapperConfiguration)
                .ToListAsync();

            var uniqueMarketerIds = matrix.Select(x => x.MarketerId).Distinct().ToArray();

            var marketers =
                _marketerService.GetForOrganization<AgencyOwnerOrganizationMarketerOutput>(owner.OrganizationId,
                    uniqueMarketerIds);

            Task.WaitAll(marketers);

            return new MarketingAgencyOwnerTimeMatrixComposedOutput
            {
                Matrix = matrix,
                Marketers = marketers.Result
            };
        }

        public async Task<RecruitingAgencyOwnerTimeMatrixComposedOutput> GetRecruitingAgencyComposedOutput(IRecruitingAgencyOwner owner,
            TimeMatrixFilters filters)
        {
            // this is essentially the provider organization id
            filters.RecruiterOrganizationId = owner.OrganizationId;

            var matrix = await _context.TimeMatrix.ApplyWhereFilters(filters)
                .ProjectTo<RecruitingAgencyOwnerTimeMatrixOutput>(_mapperConfiguration)
                .ToListAsync();

            var uniqueRecruiterIds = matrix.Select(x => x.RecruiterId).Distinct().ToArray();

            var recruiters =
                _recruiterService.GetForOrganization<AgencyOwnerOrganizationRecruiterOutput>(owner.OrganizationId,
                    uniqueRecruiterIds);

            Task.WaitAll(recruiters);

            return new RecruitingAgencyOwnerTimeMatrixComposedOutput
            {
                Matrix = matrix,

                Recruiters = recruiters.Result
            };
        }
    }
}