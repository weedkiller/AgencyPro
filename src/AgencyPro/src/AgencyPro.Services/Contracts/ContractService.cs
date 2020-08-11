// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Contracts.Extensions;
using AgencyPro.Core.Contracts.Filters;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.Contracts.Services;
using AgencyPro.Core.Contracts.ViewModels;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.Services;
using AgencyPro.Core.Proposals.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AgencyPro.Core.Agreements.Models;
using AgencyPro.Core.Common;
using AgencyPro.Core.CustomerAccounts.Models;
using AgencyPro.Core.Extensions;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.UserAccount.Services;
using AgencyPro.Services.Contracts.EventHandlers;
using AgencyPro.Services.Stories.EmailNotifications;

namespace AgencyPro.Services.Contracts
{
    public partial class ContractService : Service<Contract>, IContractService
    {
        private readonly IOrganizationAccountManagerService _amService;
        private readonly IOrganizationContractorService _coService;
        private readonly IOrganizationCustomerService _cuService;
        private readonly IUserInfo _userInfo;
        private readonly ILogger<ContractService> _logger;
        private readonly IOrganizationMarketerService _maService;
        private readonly IOrganizationService _organizationService;
        private readonly IOrganizationProjectManagerService _pmService;
        private readonly IOrganizationRecruiterService _reService;
        private readonly IRepositoryAsync<Project> _projectRepository;
        private readonly IRepositoryAsync<RecruitingAgreement> _recruitingAgreements;
        private readonly IRepositoryAsync<CustomerAccount> _customerAccounts;


        public ContractService(
            IServiceProvider serviceProvider,
            IOrganizationService organizationService,
            IOrganizationAccountManagerService amService,
            IOrganizationProjectManagerService pmService,
            IOrganizationRecruiterService reService,
            IOrganizationMarketerService maService,
            IOrganizationContractorService coService,
            IOrganizationCustomerService cuService,
            IUserInfo userInfo,
            MultiContractEventHandler multiHandler,
            StoryEventHandlers storyEvents,
            ILogger<ContractService> logger) : base(serviceProvider)
        {
            _organizationService = organizationService;
            _amService = amService;
            _pmService = pmService;
            _reService = reService;
            _maService = maService;
            _coService = coService;
            _cuService = cuService;
            _userInfo = userInfo;
            _logger = logger;

            _projectRepository = UnitOfWork.RepositoryAsync<Project>();
            _recruitingAgreements = UnitOfWork.RepositoryAsync<RecruitingAgreement>();
            _customerAccounts = UnitOfWork.RepositoryAsync<CustomerAccount>();
            
            AddEventHandler(multiHandler, storyEvents);
        }

        public async Task<List<T>> GetContracts<T>(Guid[] ids)
            where T : ContractOutput
        {
            return await Repository
                .Queryable()
                .Where(x => ids.Contains(x.Id))
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public async Task<int> GetNextProviderContractId(Guid organizationId)
        {
            if (await Repository.Queryable().IgnoreQueryFilters().Where(x => x.ContractorOrganizationId == organizationId)
                .AnyAsync())
            {
                var number = await Repository
                    .Queryable()
                    .IgnoreQueryFilters()
                    .Where(x => x.ContractorOrganizationId == organizationId)
                    .Select(x => x.ProviderNumber)
                    .MaxAsync();

                return number + 1;
            }

            return 1000;
        }
        
        public async Task<int> GetNextMarketingContractId(Guid organizationId)
        {
            if (await Repository.Queryable().IgnoreQueryFilters().Where(x => x.MarketerOrganizationId == organizationId)
                .AnyAsync())
            {
                var number = await Repository
                    .Queryable()
                    .IgnoreQueryFilters()
                    .Where(x => x.MarketerOrganizationId == organizationId)
                    .Select(x => x.MarketingNumber)
                    .MaxAsync();

                return number + 1;
            }

            return 1000;
        }

        public async Task<int> GetNextRecruitingContractId(Guid organizationId)
        {
            if (await Repository.Queryable().IgnoreQueryFilters().Where(x => x.RecruiterOrganizationId == organizationId)
                .AnyAsync())
            {
                var number = await Repository
                    .Queryable()
                    .IgnoreQueryFilters()
                    .Where(x => x.RecruiterOrganizationId == organizationId)
                    .Select(x => x.RecruitingNumber)
                    .MaxAsync();

                return number + 1;
            }

            return 1000;
        }

        public async Task<int> GetNextBuyerContractId(Guid organizationId)
        {
            if (!await Repository.Queryable()
                .Include(x => x.Project)
                .IgnoreQueryFilters().Where(x => x.BuyerOrganizationId == organizationId)
                .AnyAsync()) return 1000;
            {
                var number = await Repository
                    .Queryable()
                    .IgnoreQueryFilters()
                    .Where(x => x.BuyerOrganizationId == organizationId)
                    .Select(x => x.BuyerNumber)
                    .MaxAsync();

                return number + 1;
            }

        }

        public Task<List<T>> GetContracts<T>(IProviderAgencyOwner owner, Guid[] uniqueContractIds) where T : AgencyOwnerProviderContractOutput
        {
            return Repository
                .Queryable().ForAgencyOwner(owner)
                .Where(x => uniqueContractIds.Contains(x.Id))
                .OrderByDescending(x => x.Updated)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<List<T>> GetContracts<T>(IOrganizationAccountManager am, Guid[] uniqueContractIds) where T : AccountManagerContractOutput
        {
            return Repository
                .Queryable()
                .ForOrganizationAccountManager(am)
                .Where(x => uniqueContractIds.Contains(x.Id))
                .OrderByDescending(x => x.Updated)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<List<T>> GetContracts<T>(IOrganizationProjectManager pm, Guid[] uniqueContractIds) where T : ProjectManagerContractOutput
        {
            return Repository
                .Queryable()
                .ForOrganizationProjectManager(pm)
                .Where(x => uniqueContractIds.Contains(x.Id))
                .OrderByDescending(x => x.Updated)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<List<T>> GetContracts<T>(IOrganizationRecruiter re, Guid[] uniqueContractIds) where T : RecruiterContractOutput
        {
            return Repository
                .Queryable()
                .ForOrganizationRecruiter(re)
                .Where(x => uniqueContractIds.Contains(x.Id))
                .OrderByDescending(x => x.Updated)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<List<T>> GetContracts<T>(IOrganizationMarketer ma, Guid[] uniqueContractIds) where T : MarketerContractOutput
        {
            return Repository
                .Queryable()
                .ForOrganizationMarketer(ma)
                .Where(x => uniqueContractIds.Contains(x.Id))
                .OrderByDescending(x => x.Updated)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<List<T>> GetContracts<T>(IOrganizationContractor co, Guid[] uniqueContractIds) where T : ContractorContractOutput
        {
            return Repository
                .Queryable()
                .ForOrganizationContractor(co)
                .Where(x => uniqueContractIds.Contains(x.Id))
                .OrderByDescending(x => x.Updated)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<List<T>> GetContracts<T>(IOrganizationCustomer cu, Guid[] uniqueContractIds) where T : CustomerContractOutput
        {
            return Repository
                .Queryable()
                .ForOrganizationCustomer(cu)
                .Where(x => uniqueContractIds.Contains(x.Id))
                .OrderByDescending(x => x.Updated)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }


        public Task<PackedList<T>> GetContracts<T>(
            IOrganizationAccountManager am, ContractFilters filters)
            where T : AccountManagerContractOutput
        {
            return Repository.Queryable()
                .ForOrganizationAccountManager(am)
                .ApplyWhereFilters(filters)
                .OrderByDescending(x => x.Updated)
                .PaginateProjection<Contract, T>(filters, ProjectionMapping);
        }


        public Task<PackedList<T>> GetContracts<T>(IOrganizationProjectManager pm, ContractFilters filters)
            where T : ProjectManagerContractOutput
        {
            return Repository.Queryable()
                .ForOrganizationProjectManager(pm)
                .ApplyWhereFilters(filters)
                .OrderByDescending(x => x.Updated)
                .PaginateProjection<Contract, T>(filters, ProjectionMapping);
        }


        public Task<PackedList<T>> GetContracts<T>(IOrganizationCustomer cu, ContractFilters filters)
            where T : CustomerContractOutput
        {
            return Repository.Queryable().ForOrganizationCustomer(cu)
                 .Where(x => x.Project.Proposal != null && x.Project.Proposal.Status == ProposalStatus.Accepted)
                 .ApplyWhereFilters(filters)
                 .OrderByDescending(x => x.Updated)
                 .PaginateProjection<Contract, T>(filters, ProjectionMapping);
        }


        public Task<PackedList<T>> GetContracts<T>(IOrganizationContractor co, ContractFilters filters)
            where T : ContractorContractOutput
        {
            return Repository.Queryable().ForOrganizationContractor(co)
                .ApplyWhereFilters(filters)
                .OrderByDescending(x => x.Updated)
                .PaginateProjection<Contract, T>(filters, ProjectionMapping);
        }

        public async Task<T> GetContract<T>(Guid id)
            where T : ContractOutput
        {
            return await Repository.Queryable()
                .FindById(id)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<PackedList<T>> GetProviderContracts<T>(IProviderAgencyOwner ao, ContractFilters filters)
            where T : AgencyOwnerProviderContractOutput
        {
            return Repository.Queryable().ForAgencyOwner(ao)
                .ApplyWhereFilters(filters)
                .OrderByDescending(x => x.Updated)
                .PaginateProjection<Contract, T>(filters, ProjectionMapping);
        }

        public Task<PackedList<T>> GetMarketingContracts<T>(IMarketingAgencyOwner ao, ContractFilters filters) where T : AgencyOwnerMarketingContractOutput
        {
            return Repository.Queryable().ForAgencyOwner(ao)
                .ApplyWhereFilters(filters)
                .OrderByDescending(x => x.Updated)
                .PaginateProjection<Contract, T>(filters, ProjectionMapping);
        }

        public Task<PackedList<T>> GetRecruitingContracts<T>(IRecruitingAgencyOwner ao, ContractFilters filters) where T : AgencyOwnerRecruitingContractOutput
        {
            return Repository.Queryable().ForAgencyOwner(ao)
                .ApplyWhereFilters(filters)
                .OrderByDescending(x => x.Updated)
                .PaginateProjection<Contract, T>(filters, ProjectionMapping);
        }

        public Task<T> GetContract<T>(IOrganizationRecruiter re, Guid id) where T : RecruiterContractOutput
        {
            return Repository.Queryable()
                .ForOrganizationRecruiter(re)
                .FindById(id)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }


        public Task<T> GetContract<T>(IOrganizationCustomer cu, Guid contractId)
            where T : CustomerContractOutput
        {
            return Repository.Queryable()
                .ForOrganizationCustomer(cu)
                .FindById(contractId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetContract<T>(IOrganizationMarketer ma, Guid id) where T : MarketerContractOutput
        {
            return Repository.Queryable()
                .ForOrganizationMarketer(ma)
                .FindById(id)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetProviderContract<T>(IProviderAgencyOwner agencyOwner, Guid contractId)
            where T : AgencyOwnerProviderContractOutput
        {
            return Repository.Queryable().ForAgencyOwner(agencyOwner)
                .FindById(contractId)
                .Include(a => a.TimeEntries).ThenInclude(x => x.Story)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetMarketingContract<T>(IMarketingAgencyOwner ao, Guid contractId) where T : AgencyOwnerMarketingContractOutput
        {
            return Repository.Queryable().ForAgencyOwner(ao)
                .FindById(contractId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetRecruitingContract<T>(IRecruitingAgencyOwner ao, Guid contractId) where T : AgencyOwnerRecruitingContractOutput
        {
            return Repository.Queryable().ForAgencyOwner(ao)
                .FindById(contractId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public async Task<T> GetContract<T>(IOrganizationAccountManager am, Guid contractId)
            where T : AccountManagerContractOutput
        {
            var contract = await Repository.Queryable()
                .FindById(contractId)
                .ForOrganizationAccountManager(am)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();

            return contract;
        }

        public Task<T> GetContract<T>(IOrganizationContractor co, Guid contractId)
            where T : ContractorContractOutput
        {
            var contract = Repository.Queryable()
                .ForOrganizationContractor(co)
                .FindById(contractId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();

            return contract;
        }


        public Task<T> GetContract<T>(IOrganizationProjectManager organizationProjectManager, Guid contractId)
            where T : ProjectManagerContractOutput
        {
            return Repository
                .Queryable()
                .ForOrganizationProjectManager(organizationProjectManager)
                .FindById(contractId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<bool> DoesContractAlreadyExist(IOrganizationContractor co, IOrganizationCustomer cu, Guid projectId)
        {
            return Repository.Queryable()
                .ForOrganizationCustomer(cu)
                .ForOrganizationContractor(co)
                .FindById(projectId)
                .AnyAsync();
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[ContractService.{callerName}] - {message}";
        }


        public Task<PackedList<T>> GetContracts<T>(IOrganizationMarketer ma, ContractFilters filters)
            where T : MarketerContractOutput
        {
            return Repository.Queryable().ForOrganizationMarketer(ma)
                .ApplyWhereFilters(filters)
                .OrderByDescending(x => x.Updated)
                .PaginateProjection<Contract, T>(filters, ProjectionMapping);
        }


        public Task<PackedList<T>> GetContracts<T>(IOrganizationRecruiter re, ContractFilters filters)
            where T : RecruiterContractOutput
        {
            return Repository.Queryable().ForOrganizationRecruiter(re)
                .ApplyWhereFilters(filters)
                .OrderByDescending(x => x.Updated)
                .PaginateProjection<Contract, T>(filters, ProjectionMapping);
        }
    }
}