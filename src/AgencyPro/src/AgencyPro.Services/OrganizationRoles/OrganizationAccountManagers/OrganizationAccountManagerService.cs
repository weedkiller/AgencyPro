// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Common;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.Extensions;
using AgencyPro.Core.OrganizationPeople.Filters;
using AgencyPro.Core.OrganizationPeople.Models;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Extensions;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationAccountManagers;
using AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels;
using AgencyPro.Core.Organizations.Services;
using AgencyPro.Core.Projects.Extensions;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.UserAccount.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.OrganizationRoles.OrganizationAccountManagers
{
    public partial class OrganizationAccountManagerService : Service<OrganizationAccountManager>,
        IOrganizationAccountManagerService
    {
        private readonly IUserInfo _userInfo;
        private readonly IOrganizationService _orgService;
        private readonly IRepositoryAsync<OrganizationPerson> _personRepository;
        private readonly ILogger<OrganizationAccountManagerService> _logger;
        private readonly IRepositoryAsync<Project> _projectRepo;

        public OrganizationAccountManagerService(
            IServiceProvider serviceProvider,
            IUserInfo userInfo,
            IOrganizationService orgService,
            ILogger<OrganizationAccountManagerService> logger) : base(serviceProvider)
        {
            _userInfo = userInfo;
            _orgService = orgService;
            _personRepository = UnitOfWork.RepositoryAsync<OrganizationPerson>();
            _logger = logger;
            _projectRepo = UnitOfWork.RepositoryAsync<Project>();
        }

        public async Task<T> GetAccountManagerForProject<T>(Guid projectId) where T : OrganizationAccountManagerOutput
        {
            return await _projectRepo.Queryable()
                .FindById(projectId)
                .Select(x => x.CustomerAccount.OrganizationAccountManager)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public async Task<T> GetAccountManagerOrDefault<T>(Guid organizationId, Guid? accountManagerId) where T : OrganizationAccountManagerOutput
        {
            var amCandidate = await Repository
                .Queryable().Where(x => x.OrganizationId == organizationId && x.AccountManagerId == accountManagerId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();

            if (amCandidate != null)
                return amCandidate;

            var defaultAm = await Repository
                .Queryable().Include(x => x.Organization)
                .ThenInclude(x => x.ProviderOrganization)
                .Where(x => x.OrganizationId == organizationId)
                .Select(x => x.Organization.ProviderOrganization.DefaultAccountManager)
                .ProjectTo<T>(ProjectionMapping).FirstAsync();

            return defaultAm;
        }

        public async Task<IOrganizationAccountManager> GetPrincipal(Guid personId, Guid organizationId)
        {
            var principal = await Get(personId, organizationId);
            return principal;
        }

        public Task<OrganizationAccountManagerOutput> Get(Guid personId, Guid organizationId)
        {
            return GetById<OrganizationAccountManagerOutput>(personId, organizationId);
        }

        public Task<T> GetById<T>(Guid personId, Guid organizationId)
            where T : OrganizationAccountManagerOutput
        {
            return Repository.Queryable()
                .FindById(personId, organizationId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetById<T>(OrganizationAccountManagerInput input) where T : OrganizationAccountManagerOutput
        {
            return GetById<T>(input.AccountManagerId, input.OrganizationId);
        }

        public Task<List<T>> GetForOrganization<T>(Guid organizationId, AccountManagerFilters filters) where T : OrganizationAccountManagerOutput
        {
            return Repository.Queryable()
                .ApplyWhereFilters(filters)
                .Where(x => x.OrganizationId == organizationId)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<PackedList<T>> GetForOrganization<T>(Guid organizationId, 
            AccountManagerFilters filters, CommonFilters pagingFilters) where T : OrganizationAccountManagerOutput
        {
            return Repository.Queryable()
                .ApplyWhereFilters(filters)
                .Where(x => x.OrganizationId == organizationId)
                .PaginateProjection<OrganizationAccountManager, T>(pagingFilters, ProjectionMapping);
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[OrganizationAccountManagerService.{callerName}] - {message}";
        }

        public Task<List<T>> GetForOrganization<T>(Guid organization, Guid[] personIds) where T : OrganizationAccountManagerOutput
        {
            return Repository.Queryable()
                .Where(x => x.OrganizationId == organization && personIds.Contains(x.AccountManagerId))
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }
        
        public async Task<T> GetOrganization<T>(IOrganizationAccountManager am) where T : AccountManagerOrganizationOutput
        {
            return await Repository.Queryable()
                .Where(x => x.AccountManagerId == am.AccountManagerId && x.OrganizationId == am.OrganizationId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<AccountManagerCounts> GetCounts(IOrganizationAccountManager am)
        {
            return Repository.Queryable()
                .Where(x => x.AccountManagerId == am.AccountManagerId && x.OrganizationId == am.OrganizationId)
                .ProjectTo<AccountManagerCounts>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }
    }
}