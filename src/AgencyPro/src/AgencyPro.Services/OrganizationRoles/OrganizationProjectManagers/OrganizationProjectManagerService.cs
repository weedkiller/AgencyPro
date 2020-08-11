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
using AgencyPro.Core.OrganizationPeople.Services;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Extensions;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationProjectManagers;
using AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels;
using AgencyPro.Core.Organizations.Services;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.UserAccount.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.OrganizationRoles.OrganizationProjectManagers
{
    public partial class OrganizationProjectManagerService : Service<OrganizationProjectManager>,
        IOrganizationProjectManagerService
    {
        private readonly ILogger<OrganizationProjectManagerService> _logger;
        private readonly IOrganizationPersonService _organizationPersonService;
        private readonly IOrganizationService _orgService;
        private readonly IUserInfo _userInfo;
        private readonly IRepositoryAsync<Project> _projectRepository;
        private readonly IRepositoryAsync<OrganizationPerson> _personRepository;

        public OrganizationProjectManagerService(
            ILogger<OrganizationProjectManagerService> logger,
            IOrganizationPersonService organizationPersonService,
            IOrganizationService orgService,
            IUserInfo userInfo,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = logger;
            _organizationPersonService = organizationPersonService;
            _orgService = orgService;
            _userInfo = userInfo;
            _personRepository = UnitOfWork.RepositoryAsync<OrganizationPerson>();
            _projectRepository = UnitOfWork.RepositoryAsync<Project>();
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[OrganizationProjectManagerService.{callerName}] - {message}";
        }


        public async Task<IOrganizationProjectManager> GetPrincipal(Guid personId, Guid organizationId)
        {
            var principal = await GetById<OrganizationProjectManagerOutput>(personId, organizationId);
            return principal;
        }

        public Task<OrganizationProjectManagerOutput> Get(Guid personId, Guid organizationId)
        {
            return GetById<OrganizationProjectManagerOutput>(personId, organizationId);
        }

        public Task<T> GetById<T>(Guid projectManagerId, Guid organizationId)
            where T : OrganizationProjectManagerOutput
        {
            return Repository.Queryable()
                .Where(x => x.ProjectManagerId == projectManagerId && x.OrganizationId == organizationId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public async Task<T> GetProjectManagerForProject<T>(Guid projectId)
            where T : OrganizationProjectManagerOutput
        {
            var project = await _projectRepository
                .Queryable()
                .Where(x => x.Id == projectId)
                .Select(x => x.OrganizationProjectManager)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();

            return project;
        }


        public Task<T> GetById<T>(OrganizationProjectManagerInput input)
            where T : OrganizationProjectManagerOutput
        {
            return GetById<T>(input.ProjectManagerId, input.OrganizationId);
        }

        public Task<List<T>> GetForOrganization<T>(Guid organizationId, ProjectManagerFilters filters)
            where T : OrganizationProjectManagerOutput
        {
            return Repository.Queryable()
                .ApplyWhereFilters(filters)
                .Where(x => x.OrganizationId == organizationId)
                .ProjectTo<T>(ProjectionMapping)
                .OrderByDescending(x => x.IsDefault)
                .ToListAsync();
        }

        public async Task<PackedList<T>> GetForOrganization<T>(Guid organizationId,
            ProjectManagerFilters filters, CommonFilters pagingFilters) where T : OrganizationProjectManagerOutput
        {
            var organizationProjectManagers = Repository.Queryable()
                .ApplyWhereFilters(filters)
                .Where(x => x.OrganizationId == organizationId)
                .ProjectTo<T>(ProjectionMapping)
                .OrderByDescending(x => x.IsDefault);

            return new PackedList<T>
            {
                Data = await organizationProjectManagers.Paginate(pagingFilters.Page, pagingFilters.PageSize).ToListAsync(),
                Total = await organizationProjectManagers.CountAsync()
            };
        }

        public Task<List<T>> GetForOrganization<T>(Guid organization, Guid[] personIds) where T : OrganizationProjectManagerOutput
        {
            return Repository.Queryable()
                .Where(x => x.OrganizationId == organization && personIds.Contains(x.ProjectManagerId))
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<T> GetOrganization<T>(IOrganizationProjectManager principal) where T : ProjectManagerOrganizationOutput
        {
            return Repository.Queryable()
                .Where(x => x.ProjectManagerId == principal.ProjectManagerId && x.OrganizationId == principal.OrganizationId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<ProjectManagerCounts> GetCounts(IOrganizationProjectManager principal)
        {
            return Repository.Queryable()
                .Where(x => x.ProjectManagerId == principal.ProjectManagerId && x.OrganizationId == principal.OrganizationId)
                .ProjectTo<ProjectManagerCounts>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }
    }
}