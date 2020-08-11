// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
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
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationMarketers;
using AgencyPro.Core.Organizations.MarketingOrganizations.ViewModels;
using AgencyPro.Core.Organizations.Services;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.UserAccount.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AgencyPro.Services.OrganizationRoles.OrganizationMarketers
{
    public partial class OrganizationMarketerService : Service<OrganizationMarketer>, IOrganizationMarketerService
    {
        private readonly IOrganizationService _orgService;
        private readonly IUserInfo _userInfo;
        private readonly ILogger<OrganizationMarketerService> _logger;
        private readonly IRepositoryAsync<OrganizationPerson> _personRepository;
        private readonly IRepositoryAsync<Project> _projectRepository;

        public OrganizationMarketerService(
            IServiceProvider serviceProvider,
            IOrganizationService orgService,
            IUserInfo userInfo,
            ILogger<OrganizationMarketerService> logger
        ) : base(serviceProvider)
        {
            _orgService = orgService;
            _userInfo = userInfo;
            _logger = logger;
            _personRepository = UnitOfWork.RepositoryAsync<OrganizationPerson>();
            _projectRepository = UnitOfWork.RepositoryAsync<Project>();
        }

        public Task<T> GetMarketerForProject<T>(Guid inputProjectId)
            where T : OrganizationMarketerOutput
        {
            
            return UnitOfWork.RepositoryAsync<Project>()
                .Queryable()
                .Include(x => x.CustomerAccount)
                .ThenInclude(x => x.OrganizationCustomer)
                .ThenInclude(x => x.Customer)
                .ThenInclude(x=>x.OrganizationMarketer)
                .Where(x => x.Id == inputProjectId)
                .Select(x => x.CustomerAccount.Customer.OrganizationMarketer)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public async Task<T> GetMarketerOrDefault<T>(Guid? organizationId, Guid? marketerId, string referralCode) where T : OrganizationMarketerOutput
        {
            _logger.LogInformation(GetLogMessage("Organization: {0}; Marketer: {1}; ReferralCode: {2}"), organizationId.GetValueOrDefault(), marketerId.GetValueOrDefault(), referralCode);

            T ma = null;


            if (organizationId.HasValue && marketerId.HasValue)
            {
                _logger.LogDebug(GetLogMessage("organization/marketer found"));

                ma = await GetById<T>(marketerId.Value, organizationId.Value);
            }
            else
            {
                _logger.LogDebug(GetLogMessage("No organization/marketer found"));

            }

            if (ma == null)
            {
                _logger.LogDebug(GetLogMessage("No org_marketer found"));


                if (organizationId.HasValue)
                {
                    _logger.LogDebug(GetLogMessage("getting default marketer from organization"));

                    ma = await Repository.Queryable()
                        .Include(x => x.OrganizationDefaults)
                        .Where(x => x.OrganizationId == organizationId.Value && x.OrganizationDefaults.Any())
                        .ProjectTo<T>(ProjectionMapping)
                        .FirstOrDefaultAsync();
                }

                if (ma == null)
                {
                    _logger.LogDebug(GetLogMessage("getting system default marketer"));

                    ma = await Repository.Queryable().Where(x => x.IsSystemDefault)
                        .ProjectTo<T>(ProjectionMapping)
                        .FirstAsync();
                }
            }

            if (ma != null)
            {
                _logger.LogInformation(GetLogMessage("marketer found"));
            }

            return ma;
        }



        public async Task<IOrganizationMarketer> GetPrincipal(Guid personId, Guid organizationId)
        {
            //_logger.LogInformation(GetLogMessage("Getting marketer {0} in organization {1}"), personId, organizationId);

            var principal = await GetById<OrganizationMarketerOutput>(personId, organizationId);

            return principal;
        }

        public Task<OrganizationMarketerOutput> Get(Guid personId, Guid organizationId)
        {
            //_logger.LogInformation(GetLogMessage("Getting marketer {0} in organization {1}"), personId, organizationId);

            return GetById<OrganizationMarketerOutput>(personId, organizationId);
        }

        public Task<T> GetById<T>(Guid personId, Guid organizationId)
            where T : OrganizationMarketerOutput
        {
            return Repository.Queryable()
                .Where(x => x.MarketerId == personId && x.OrganizationId == organizationId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetById<T>(OrganizationMarketerInput input)
            where T : OrganizationMarketerOutput
        {
            return GetById<T>(input.MarketerId, input.OrganizationId);
        }

        public Task<List<T>> GetForOrganization<T>(Guid organizationId, MarketerFilters filters)
            where T : OrganizationMarketerOutput
        {
            return Repository.Queryable()
                .Where(x => x.OrganizationId == organizationId)
                .ApplyWhereFilters(filters)
                .ProjectTo<T>(ProjectionMapping)
                .OrderByDescending(x => x.IsDefault)
                .ToListAsync();
        }

        public async Task<PackedList<T>> GetForOrganization<T>(Guid organizationId,
            MarketerFilters filters, CommonFilters pagingFilters) where T : OrganizationMarketerOutput
        {
            var organizationMarketers = Repository.Queryable()
                .Where(x => x.OrganizationId == organizationId)
                .ApplyWhereFilters(filters)
                .ProjectTo<T>(ProjectionMapping)
                .OrderByDescending(x => x.IsDefault);

            return new PackedList<T>
            {
                Data = await organizationMarketers.Paginate(pagingFilters.Page, pagingFilters.PageSize).ToListAsync(),
                Total = await organizationMarketers.CountAsync()
            };
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[OrganizationMarketerService.{callerName}] - {message}";
        }

        public Task<List<T>> GetForOrganization<T>(Guid organization, Guid[] personIds) where T : OrganizationMarketerOutput
        {
            return Repository.Queryable()
                .Where(x => x.OrganizationId == organization && personIds.Contains(x.MarketerId))
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<T> GetOrganization<T>(IOrganizationMarketer principal) where T : MarketerOrganizationOutput
        {
            return Repository.Queryable()
                .Where(x => x.MarketerId == principal.MarketerId && x.OrganizationId == principal.OrganizationId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<MarketerCounts> GetCounts(IOrganizationMarketer principal)
        {
            _logger.LogInformation(GetLogMessage("Getting Marketer Counts"));

            return Repository.Queryable()
                .Where(x => x.MarketerId == principal.MarketerId && x.OrganizationId == principal.OrganizationId)
                .ProjectTo<MarketerCounts>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }
    }
}