// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Common;
using AgencyPro.Core.CustomerAccounts.Models;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.Extensions;
using AgencyPro.Core.OrganizationPeople.Filters;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Extensions;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationCustomers;
using AgencyPro.Core.Organizations.ViewModels;
using AgencyPro.Core.Projects.Extensions;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.UserAccount.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.OrganizationRoles.OrganizationCustomers
{
    public partial class OrganizationCustomerService : Service<OrganizationCustomer>, IOrganizationCustomerService
    {
        private readonly IUserInfo _userInfo;
        private readonly ILogger<OrganizationCustomerService> _logger;
        private readonly IRepositoryAsync<Project> _projectRepo;
        private readonly IRepositoryAsync<CustomerAccount> _customerAccountRepo;

        public OrganizationCustomerService(
            IServiceProvider serviceProvider,
            IUserInfo userInfo,
            ILogger<OrganizationCustomerService> logger) : base(serviceProvider)
        {
            _userInfo = userInfo;
            _logger = logger;
            _projectRepo = UnitOfWork.RepositoryAsync<Project>();
            _customerAccountRepo = UnitOfWork.RepositoryAsync<CustomerAccount>();
        }

        public async Task<IOrganizationCustomer> GetPrincipal(Guid personId, Guid organizationId)
        {
            _logger.LogInformation(GetLogMessage("Person {0}, Organization {1}"), personId, organizationId);

            var principal = await Get(personId, organizationId);
            return principal;
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(OrganizationCustomerService)}.{callerName}] - {message}";
        }

        public async Task<IAgencyOwner> GetAgencyOwner(Guid personId, Guid organizationId)
        {
            _logger.LogInformation(GetLogMessage("Person {0}, Organization {1}"), personId, organizationId);
           
            var principal = await Get(personId, organizationId);
            return principal;
        }

        public Task<List<T>> GetCustomers<T>(IAgencyOwner agencyOwner) where T : OrganizationCustomerOutput
        {
            return _customerAccountRepo.Queryable()
                .Where(x => x.AccountManagerOrganizationId == agencyOwner.OrganizationId)
                .Select(x => x.OrganizationCustomer)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<OrganizationCustomerOutput> Get(Guid personId, Guid organizationId)
        {
            return GetById<OrganizationCustomerOutput>(personId, organizationId);
        }

        public Task<T> GetById<T>(Guid personId, Guid organizationId) where T : OrganizationCustomerOutput
        {
            return Repository.Queryable()
                .Where(x => x.CustomerId == personId && x.OrganizationId == organizationId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetById<T>(OrganizationCustomerInput input) where T : OrganizationCustomerOutput
        {
            return GetById<T>(input.CustomerId, input.OrganizationId);
        }

        public Task<List<T>> GetForOrganization<T>(Guid organizationId, CustomerFilters filters) where T : OrganizationCustomerOutput
        {
            return Repository.Queryable()
                .ApplyWhereFilters(filters)
                .Where(x => x.OrganizationId == organizationId)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<PackedList<T>> GetForOrganization<T>(Guid organizationId,
            CustomerFilters filters, CommonFilters pagingFilters) where T : OrganizationCustomerOutput
        {
            return Repository.Queryable()
                .ApplyWhereFilters(filters)
                .Where(x => x.OrganizationId == organizationId)
                .PaginateProjection<OrganizationCustomer, T>(pagingFilters, ProjectionMapping);
        }


        public Task<T> GetCustomerForProject<T>(Guid projectId) where T : OrganizationCustomerOutput
        {
            return _projectRepo.Queryable()
                .Where(x => x.Id == projectId)
                .Select(x => x.CustomerAccount.OrganizationCustomer)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<List<T>> GetCustomers<T>(IOrganizationAccountManager organizationAccountManager)
            where T : OrganizationCustomerOutput
        {
            return _customerAccountRepo.Queryable()
                .Where(x => x.AccountManagerOrganizationId == organizationAccountManager.OrganizationId && x.AccountManagerId == organizationAccountManager.AccountManagerId)
                .Select(x => x.OrganizationCustomer)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public async Task<List<T>> GetCustomers<T>(IOrganizationProjectManager projectManager) where T : OrganizationCustomerOutput
        {
            var a = await _projectRepo.Queryable()
                .Include(x=>x.OrganizationCustomer)
                .ThenInclude(x=>x.Customer)
                .ThenInclude(x=>x.Person)
                .Include(x=>x.OrganizationCustomer)
                .ThenInclude(x=>x.Organization)
                .ForOrganizationProjectManager(projectManager)
                .Select(x=>x.OrganizationCustomer)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();

            return a;
        }

        public Task<List<T>> GetForOrganization<T>(Guid organization, Guid[] personIds) where T : OrganizationCustomerOutput
        {
            return Repository.Queryable()
                .Where(x => x.OrganizationId == organization && personIds.Contains(x.CustomerId))
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public async Task<T> GetOrganization<T>(IOrganizationCustomer principal) where T : CustomerOrganizationOutput
        {
            return await Repository.Queryable()
                .Where(x => x.CustomerId == principal.CustomerId && x.OrganizationId == principal.OrganizationId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<CustomerCounts> GetCounts(IOrganizationCustomer principal)
        {
            return Repository.Queryable()
                .Where(x => x.CustomerId == principal.CustomerId && x.OrganizationId == principal.OrganizationId)
                .ProjectTo<CustomerCounts>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }
    }
}