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
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationRecruiters;
using AgencyPro.Core.Organizations.RecruitingOrganizations.ViewModels;
using AgencyPro.Core.Organizations.Services;
using AgencyPro.Core.UserAccount.Services;
using AgencyPro.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.OrganizationRoles.OrganizationRecruiters
{
    public partial class OrganizationRecruiterService : Service<OrganizationRecruiter>, IOrganizationRecruiterService
    {
        private readonly ILogger<OrganizationRecruiterService> _logger;
        private readonly IOrganizationService _orgService;
        private readonly IUserInfo _userInfo;
        private readonly IOrganizationPersonService _organizationPersonService;
        private readonly IRepositoryAsync<OrganizationPerson> _personRepository;

        public OrganizationRecruiterService(
            ILogger<OrganizationRecruiterService> logger,
            IOrganizationService orgService,
            IUserInfo userInfo,
            IOrganizationPersonService organizationPersonService,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = logger;
            _orgService = orgService;
            _userInfo = userInfo;
            _organizationPersonService = organizationPersonService;
            _personRepository = UnitOfWork.RepositoryAsync<OrganizationPerson>();
        }

        public async Task<IOrganizationRecruiter> GetPrincipal(Guid personId, Guid organizationId)
        {
            var recruiter = await GetById<OrganizationRecruiterOutput>(personId, organizationId);
            return recruiter;
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[OrganizationRecruiterService.{callerName}] - {message}";
        }

        public Task<OrganizationRecruiterOutput> Get(Guid personId, Guid organizationId)
        {
            return GetById<OrganizationRecruiterOutput>(personId, organizationId);
        }

        public Task<T> GetById<T>(Guid personId, Guid organizationId)
            where T : OrganizationRecruiterOutput
        {
            return Repository.Queryable()
                .GetById(organizationId, personId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetById<T>(OrganizationRecruiterInput input) where T : OrganizationRecruiterOutput
        {
            return GetById<T>(input.RecruiterId, input.OrganizationId);
        }

        public Task<List<T>> GetForOrganization<T>(Guid organizationId, RecruiterFilters filters) where T : OrganizationRecruiterOutput
        {
            return Repository.Queryable()

                .GetForOrganization(organizationId)
                .ApplyWhereFilters(filters)
                .ProjectTo<T>(ProjectionMapping)
                .OrderByDescending(x => x.IsDefault)
                .ToListAsync();
        }

        public async Task<PackedList<T>> GetForOrganization<T>(Guid organizationId,
            RecruiterFilters filters, CommonFilters pagingFilters) where T : OrganizationRecruiterOutput
        {
            var organizationRecruiters = Repository.Queryable()
                .GetForOrganization(organizationId)
                .ApplyWhereFilters(filters)
                .ProjectTo<T>(ProjectionMapping)
                .OrderByDescending(x => x.IsDefault);

            return new PackedList<T>
            {
                Data = await organizationRecruiters.Paginate(pagingFilters.Page, pagingFilters.PageSize).ToListAsync(),
                Total = await organizationRecruiters.CountAsync()
            };
        }

        public Task<List<T>> GetForOrganization<T>(Guid organization, Guid[] personIds) where T : OrganizationRecruiterOutput
        {
            return Repository.Queryable()
                .Where(x => x.OrganizationId == organization && personIds.Contains(x.RecruiterId))
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<T> GetOrganization<T>(IOrganizationRecruiter principal) where T : RecruiterOrganizationOutput
        {
            return Repository.Queryable()
                .Where(x => x.RecruiterId == principal.RecruiterId && x.OrganizationId == principal.OrganizationId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<RecruiterCounts> GetCounts(IOrganizationRecruiter principal)
        {
            return Repository.Queryable()
                .Where(x => x.RecruiterId == principal.RecruiterId && x.OrganizationId == principal.OrganizationId)
                .ProjectTo<RecruiterCounts>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }
    }
}