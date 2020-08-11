// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Contracts.Services;
using AgencyPro.Core.Contracts.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationAccountManagers;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationCustomers;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationProjectManagers;
using AgencyPro.Core.Projects.Services;
using AgencyPro.Core.Projects.ViewModels;
using AgencyPro.Core.Stories.Services;
using AgencyPro.Core.Stories.ViewModels;
using AgencyPro.Core.TimeEntries.Extensions;
using AgencyPro.Core.TimeEntries.Services;
using AgencyPro.Core.TimeEntries.ViewModels;
using AgencyPro.Core.TimeEntries.ViewModels.TimeMatrix;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Services.TimeEntries
{
    public partial class TimeMatrixService : ITimeMatrixService
    {
        private readonly AppDbContext _context;
        private readonly IOrganizationContractorService _contractorService;
        private readonly IOrganizationRecruiterService _recruiterService;
        private readonly IOrganizationMarketerService _marketerService;
        private readonly IOrganizationProjectManagerService _projectManagerService;
        private readonly IOrganizationAccountManagerService _accountManagerService;
        private readonly IOrganizationCustomerService _customerService;
        private readonly IContractService _contractService;
        private readonly IProjectService _projectService;
        private readonly IStoryService _storyService;
        private readonly MapperConfiguration _mapperConfiguration;

        public TimeMatrixService(AppDbContext context, IOrganizationContractorService contractorService,
            IOrganizationRecruiterService recruiterService, IOrganizationMarketerService marketerService,
            IOrganizationProjectManagerService projectManagerService,
            IOrganizationAccountManagerService accountManagerService, IOrganizationCustomerService customerService,
            IContractService contractService, IProjectService projectService, IStoryService storyService,
            MapperConfiguration mapperConfiguration)
        {
            _mapperConfiguration = mapperConfiguration;
            _context = context;
            _contractorService = contractorService;
            _recruiterService = recruiterService;
            _marketerService = marketerService;
            _projectManagerService = projectManagerService;
            _accountManagerService = accountManagerService;
            _customerService = customerService;
            _contractService = contractService;
            _projectService = projectService;
            _storyService = storyService;
        }

        public async Task<AccountManagerTimeMatrixComposedOutput> GetComposedOutput(IOrganizationAccountManager am, TimeMatrixFilters filters)
        {
            filters.AccountManagerId = am.AccountManagerId;
            filters.ProviderOrganizationId = am.OrganizationId;

            var matrix = await _context.TimeMatrix.ApplyWhereFilters(filters)
                .ProjectTo<AccountManagerTimeMatrixOutput>(_mapperConfiguration)
                .ToListAsync();

            var uniqueProjectIds = matrix.Select(x => x.ProjectId).Distinct().ToArray();
            var uniqueContractIds = matrix.Select(x => x.ContractId).Distinct().ToArray();
            var uniqueContractorIds = matrix.Select(x => x.ContractorId).Distinct().ToArray();
            var uniqueAccountManagerIds = matrix.Select(x => x.AccountManagerId).Distinct().ToArray();
            var uniqueProjectManagerIds = matrix.Select(x => x.MarketerId).Distinct().ToArray();
            var uniqueCustomerIds = matrix.Select(x => x.CustomerId).Distinct().ToArray();
            var uniqueStoryIds = matrix
                .Where(x => x.StoryId != null).Select(x => x.StoryId).Distinct().ToArray();

            var projectTask = _projectService.GetProjects<AccountManagerProjectOutput>(am, uniqueProjectIds);
            var contractsTask = _contractService.GetContracts<AccountManagerContractOutput>(am, uniqueContractIds);
            var accountManagers =
                _accountManagerService.GetForOrganization<AccountManagerOrganizationAccountManagerOutput>(
                    am.OrganizationId, uniqueAccountManagerIds);
            var contractors =
                _contractorService.GetForOrganization<AccountManagerOrganizationContractorOutput>(am.OrganizationId,
                    uniqueContractorIds);
            var projectManagers =
                _projectManagerService.GetForOrganization<AccountManagerOrganizationProjectManagerOutput>(
                    am.OrganizationId, uniqueProjectManagerIds);
            var customers =
                _customerService.GetForOrganization<AccountManagerOrganizationCustomerOutput>(am.OrganizationId,
                    uniqueCustomerIds);

            var stories = _storyService.GetStories<AccountManagerStoryOutput>(am, uniqueStoryIds);

            Task.WaitAll(projectTask, contractsTask, accountManagers, contractors, projectManagers, customers);

            return new AccountManagerTimeMatrixComposedOutput
            {
                Matrix = matrix,
                Projects = projectTask.Result,
                Contracts = contractsTask.Result,
                Contractors = contractors.Result,
                ProjectManagers = projectManagers.Result,
                Customers = customers.Result,
                Stories = stories.Result
            };
        }

        public async Task<ProjectManagerTimeMatrixComposedOutput> GetComposedOutput(IOrganizationProjectManager pm, TimeMatrixFilters filters)
        {
            filters.ProjectManagerId = pm.ProjectManagerId;
            filters.ProviderOrganizationId = pm.OrganizationId;
            
            var matrix = await _context.TimeMatrix.ApplyWhereFilters(filters)
                .ProjectTo<ProjectManagerTimeMatrixOutput>(_mapperConfiguration)
                .ToListAsync();

            var uniqueProjectIds = matrix.Select(x => x.ProjectId).Distinct().ToArray();
            var uniqueContractIds = matrix.Select(x => x.ContractId).Distinct().ToArray();
            var uniqueContractorIds = matrix.Select(x => x.ContractorId).Distinct().ToArray();
            var uniqueAccountManagerIds = matrix.Select(x => x.AccountManagerId).Distinct().ToArray();
            var uniqueProjectManagerIds = matrix.Select(x => x.MarketerId).Distinct().ToArray();
            var uniqueCustomerIds = matrix.Select(x => x.CustomerId).Distinct().ToArray();
            var uniqueStoryIds = matrix
                .Where(x => x.StoryId != null).Select(x => x.StoryId).Distinct().ToArray();

            var projectTask = _projectService.GetProjects<ProjectManagerProjectOutput>(pm, uniqueProjectIds);
            var contractsTask = _contractService.GetContracts<ProjectManagerContractOutput>(pm, uniqueContractIds);
            var accountManagers =
                _accountManagerService.GetForOrganization<ProjectManagerOrganizationAccountManagerOutput>(
                    pm.OrganizationId, uniqueAccountManagerIds);
            var contractors =
                _contractorService.GetForOrganization<ProjectManagerOrganizationContractorOutput>(pm.OrganizationId,
                    uniqueContractorIds);
            var projectManagers =
                _projectManagerService.GetForOrganization<OrganizationProjectManagerOutput>(
                    pm.OrganizationId, uniqueProjectManagerIds);
            var customers =
                _customerService.GetForOrganization<ProjectManagerOrganizationCustomerOutput>(pm.OrganizationId,
                    uniqueCustomerIds);

            var stories = _storyService.GetStories<ProjectManagerStoryOutput>(pm, uniqueStoryIds);

            Task.WaitAll(projectTask, contractsTask, accountManagers, contractors, projectManagers, customers);

            return new ProjectManagerTimeMatrixComposedOutput
            {
                Matrix = matrix,
                Projects = projectTask.Result,
                Contracts = contractsTask.Result,
                AccountManagers = accountManagers.Result,
                Contractors = contractors.Result,
                Customers = customers.Result,
                Stories = stories.Result
            };
        }

        public async Task<CustomerTimeMatrixComposedOutput> GetComposedOutput(IOrganizationCustomer cu, TimeMatrixFilters filters)
        {

            filters.CustomerId = cu.CustomerId;
            filters.CustomerOrganizationId = cu.OrganizationId;

            var matrix = await _context.TimeMatrix.ApplyWhereFilters(filters)
                .ProjectTo<CustomerTimeMatrixOutput>(_mapperConfiguration)
                .ToListAsync();

            var uniqueProjectIds = matrix.Select(x => x.ProjectId).Distinct().ToArray();
            var uniqueContractIds = matrix.Select(x => x.ContractId).Distinct().ToArray();
            var uniqueContractorIds = matrix.Select(x => x.ContractorId).Distinct().ToArray();
            var uniqueAccountManagerIds = matrix.Select(x => x.AccountManagerId).Distinct().ToArray();
            var uniqueProjectManagerIds = matrix.Select(x => x.MarketerId).Distinct().ToArray();
            var uniqueCustomerIds = matrix.Select(x => x.CustomerId).Distinct().ToArray();
            var uniqueStoryIds = matrix
                .Where(x => x.StoryId != null).Select(x => x.StoryId).Distinct().ToArray();

            var projectTask = _projectService.GetProjects<CustomerProjectOutput>(cu, uniqueProjectIds);
            var contractsTask = _contractService.GetContracts<CustomerContractOutput>(cu, uniqueContractIds);
            var accountManagers =
                _accountManagerService.GetForOrganization<CustomerOrganizationAccountManagerOutput>(
                    cu.OrganizationId, uniqueAccountManagerIds);
            var contractors =
                _contractorService.GetForOrganization<CustomerOrganizationContractorOutput>(cu.OrganizationId,
                    uniqueContractorIds);
            var projectManagers =
                _projectManagerService.GetForOrganization<CustomerOrganizationProjectManagerOutput>(
                    cu.OrganizationId, uniqueProjectManagerIds);
            var customers =
                _customerService.GetForOrganization<CustomerOrganizationCustomerOutput>(cu.OrganizationId,
                    uniqueCustomerIds);

            var stories = _storyService.GetStories<CustomerStoryOutput>(cu, uniqueStoryIds);

            Task.WaitAll(projectTask, contractsTask, accountManagers, contractors, projectManagers, customers);

            return new CustomerTimeMatrixComposedOutput
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

        public async Task<ContractorTimeMatrixComposedOutput> GetComposedOutput(IOrganizationContractor co, TimeMatrixFilters filters)
        {
            filters.ContractorId = co.ContractorId;
            filters.ProviderOrganizationId = co.OrganizationId;

            var matrix = await _context.TimeMatrix.ApplyWhereFilters(filters)
                .ProjectTo<ContractorTimeMatrixOutput>(_mapperConfiguration)
                .ToListAsync();

            var uniqueProjectIds = matrix.Select(x => x.ProjectId).Distinct().ToArray();
            var uniqueContractIds = matrix.Select(x => x.ContractId).Distinct().ToArray();
            var uniqueContractorIds = matrix.Select(x => x.ContractorId).Distinct().ToArray();
            var uniqueAccountManagerIds = matrix.Select(x => x.AccountManagerId).Distinct().ToArray();
            var uniqueProjectManagerIds = matrix.Select(x => x.MarketerId).Distinct().ToArray();
            var uniqueRecruiterIds = matrix.Select(x => x.RecruiterId).Distinct().ToArray();
            var uniqueCustomerIds = matrix.Select(x => x.CustomerId).Distinct().ToArray();
            var uniqueStoryIds = matrix
                .Where(x => x.StoryId != null).Select(x => x.StoryId).Distinct().ToArray();

            var projectTask = _projectService.GetProjects<ContractorProjectOutput>(co, uniqueProjectIds);
            var contractsTask = _contractService.GetContracts<ContractorContractOutput>(co, uniqueContractIds);
            var accountManagers =
                _accountManagerService.GetForOrganization<ContractorOrganizationAccountManagerOutput>(
                    co.OrganizationId, uniqueAccountManagerIds);
            var contractors =
                _contractorService.GetForOrganization<OrganizationContractorOutput>(co.OrganizationId,
                    uniqueContractorIds);
            var projectManagers =
                _projectManagerService.GetForOrganization<ContractorOrganizationProjectManagerOutput>(
                    co.OrganizationId, uniqueProjectManagerIds);
            var customers =
                _customerService.GetForOrganization<ContractorOrganizationCustomerOutput>(co.OrganizationId,
                    uniqueCustomerIds);
         

            var stories = _storyService.GetStories<ContractorStoryOutput>(co, uniqueStoryIds);

            Task.WaitAll(projectTask, contractsTask, accountManagers, contractors, projectManagers, customers);

            return new ContractorTimeMatrixComposedOutput
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

        public async Task<RecruiterTimeMatrixComposedOutput> GetComposedOutput(IOrganizationRecruiter re, TimeMatrixFilters filters)
        {
            filters.ProjectId = null;
            filters.ProjectManagerId = null;
            filters.ProviderOrganizationId = null;
            filters.AccountManagerId = null;

            filters.RecruiterOrganizationId = re.OrganizationId;
            filters.RecruiterId = re.RecruiterId;

            var matrix = await _context.TimeMatrix.ApplyWhereFilters(filters)
                .ProjectTo<RecruiterTimeMatrixOutput>(_mapperConfiguration)
                .ToListAsync();

            var uniqueContractIds = matrix.Select(x => x.ContractId).Distinct().ToArray();
            var uniqueContractorIds = matrix.Select(x => x.ContractorId).Distinct().ToArray();
            var uniqueAccountManagerIds = matrix.Select(x => x.AccountManagerId).Distinct().ToArray();
      
            var contractsTask = _contractService.GetContracts<RecruiterContractOutput>(re, uniqueContractIds);
            var accountManagers =
                _accountManagerService.GetForOrganization<RecruiterOrganizationAccountManagerOutput>(
                    re.OrganizationId, uniqueAccountManagerIds);
            var contractors =
                _contractorService.GetForOrganization<RecruiterOrganizationContractorOutput>(re.OrganizationId,
                    uniqueContractorIds);

            Task.WaitAll(contractsTask, accountManagers, contractors);

            return new RecruiterTimeMatrixComposedOutput
            {
                Matrix = matrix,
                Contracts = contractsTask.Result,
                AccountManagers = accountManagers.Result,
                Contractors = contractors.Result,
            };
        }

        public async Task<MarketerTimeMatrixComposedOutput> GetComposedOutput(IOrganizationMarketer ma, TimeMatrixFilters filters)
        {
            filters.MarketerId = ma.MarketerId;
            filters.MarketerOrganizationId = ma.OrganizationId;

            var matrix = await _context.TimeMatrix.ApplyWhereFilters(filters)
                .ProjectTo<MarketerTimeMatrixOutput>(_mapperConfiguration)
                .ToListAsync();

            var uniqueContractIds = matrix.Select(x => x.ContractId).Distinct().ToArray();
            var uniqueCustomerIds = matrix.Select(x => x.CustomerId).Distinct().ToArray();

            var contractsTask = _contractService.GetContracts<MarketerContractOutput>(ma, uniqueContractIds);

            var customers =
                _customerService.GetForOrganization<MarketerOrganizationCustomerOutput>(ma.OrganizationId,
                    uniqueCustomerIds);

            var contractors =
                _contractorService.GetForOrganization<MarketerOrganizationContractorOutput>(ma.OrganizationId,
                    uniqueCustomerIds);

            Task.WaitAll(contractsTask, customers, contractors);

            return new MarketerTimeMatrixComposedOutput()
            {
                Matrix = matrix,
                Contracts = contractsTask.Result,
                Contractors = contractors.Result,
                Customers = customers.Result
            };
        }
        
    }
}
