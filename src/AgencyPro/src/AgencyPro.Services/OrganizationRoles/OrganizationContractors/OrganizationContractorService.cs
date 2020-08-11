// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.OrganizationPeople.Models;
using AgencyPro.Core.OrganizationRoles.Extensions;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors;
using AgencyPro.Core.Projects.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationPeople.Filters;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels;
using AgencyPro.Core.UserAccount.Services;
using AgencyPro.Core.Common;
using AgencyPro.Core.Extensions;

namespace AgencyPro.Services.OrganizationRoles.OrganizationContractors
{
    public partial class OrganizationContractorService : Service<OrganizationContractor>, IOrganizationContractorService
    {
        private readonly IUserInfo _userInfo;
        private readonly IRepositoryAsync<OrganizationPerson> _personRepository;
        private readonly IRepositoryAsync<OrganizationRecruiter> _recuiterRepository;
        private readonly IRepositoryAsync<Project> _projectRepository;
        private readonly ILogger<OrganizationContractorService> _logger;

        public OrganizationContractorService(
            IServiceProvider serviceProvider,
            IUserInfo userInfo,
            ILogger<OrganizationContractorService> logger) : base(serviceProvider)
        {
            _userInfo = userInfo;
            _logger = logger;
            _personRepository = UnitOfWork.RepositoryAsync<OrganizationPerson>();
            _projectRepository = UnitOfWork.RepositoryAsync<Project>();
            _recuiterRepository = UnitOfWork.RepositoryAsync<OrganizationRecruiter>();
        }

        public Task<List<T>> GetFeaturedContractors<T>(Guid organizationId)
            where T : OrganizationContractorOutput
        {
            return Repository.Queryable()
                .Where(x => x.IsFeatured)
                .Where(x => x.OrganizationId == organizationId)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }
        
        public async Task<IOrganizationContractor> GetPrincipal(Guid personId, Guid organizationId)
        {
            var principal = await Get(personId, organizationId);
            return principal;
        }

        public Task<OrganizationContractorOutput> Get(Guid personId, Guid organizationId)
        {
            return GetById<OrganizationContractorOutput>(personId, organizationId);
        }

        public Task<T> GetById<T>(Guid personId, Guid organizationId)
            where T : OrganizationContractorOutput
        {
            return Repository.Queryable()
                .FindById(personId, organizationId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetById<T>(OrganizationContractorInput input)
            where T : OrganizationContractorOutput
        {
            return GetById<T>(input.ContractorId, input.OrganizationId);
        }

        public Task<List<T>> GetForOrganization<T>(Guid organizationId, ContractorFilters filters)
            where T : OrganizationContractorOutput
        {
            return Repository.Queryable()
                .ApplyWhereFilters(filters)
                .Where(x => x.OrganizationId == organizationId)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<PackedList<T>> GetForOrganization<T>(Guid organizationId,
            ContractorFilters filters, CommonFilters pagingFilters) where T : OrganizationContractorOutput
        {
            return Repository.Queryable()
                .ApplyWhereFilters(filters)
                .Where(x => x.OrganizationId == organizationId)
                .PaginateProjection<OrganizationContractor, T>(pagingFilters, ProjectionMapping);
        }

        public Task<List<T>> GetForOrganization<T>(Guid organization, Guid[] personIds) where T : OrganizationContractorOutput
        {
            return Repository.Queryable()
                .Where(x => x.OrganizationId == organization && personIds.Contains(x.ContractorId))
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<T> GetOrganization<T>(IOrganizationContractor principal) where T : ContractorOrganizationOutput
        {
            return Repository.Queryable()
                .Where(x => x.ContractorId == principal.ContractorId && x.OrganizationId == principal.OrganizationId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<ContractorCounts> GetCounts(IOrganizationContractor principal)
        {
            return Repository.Queryable()
                .Where(x => x.ContractorId == principal.ContractorId && x.OrganizationId == principal.OrganizationId)
                .ProjectTo<ContractorCounts>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[OrganizationContractorService.{callerName}] - {message}";
        }
    }
}