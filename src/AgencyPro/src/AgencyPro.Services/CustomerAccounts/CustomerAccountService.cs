// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Agreements.Models;
using AgencyPro.Core.Common;
using AgencyPro.Core.CustomerAccounts.Extensions;
using AgencyPro.Core.CustomerAccounts.Models;
using AgencyPro.Core.CustomerAccounts.Services;
using AgencyPro.Core.CustomerAccounts.ViewModels;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.Extensions;
using AgencyPro.Core.Leads.Models;
using AgencyPro.Core.OrganizationPeople.Services;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Organizations.Services;
using AgencyPro.Core.People.Services;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.UserAccount.Models;
using AgencyPro.Core.UserAccount.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.CustomerAccounts
{
    public partial class CustomerAccountService : Service<CustomerAccount>, ICustomerAccountService
    {
        private readonly IOrganizationAccountManagerService _accountManagerService;
        private readonly IOrganizationCustomerService _customerService;
        private readonly IOrganizationService _organizationService;
        private readonly IPersonService _personService;
        private readonly IUserInfo _userInfo;
        private readonly ILogger<CustomerAccountService> _logger;
        private readonly IRepositoryAsync<MarketingAgreement> _marketingAgreements;
        private readonly IRepositoryAsync<Customer> _customers;
        private readonly IRepositoryAsync<Organization> _organizations;
        private readonly IRepositoryAsync<Lead> _leads;
        private readonly IRepositoryAsync<ApplicationUser> _applicationUsers;

        public CustomerAccountService(
            IServiceProvider serviceProvider,
            IOrganizationCustomerService customerService,
            IOrganizationService organizationService,
            IOrganizationPersonService orgPersonService,
            IPersonService personService,
            IUserInfo userInfo,
            ILogger<CustomerAccountService> logger,
            IOrganizationAccountManagerService accountManagerService)
            : base(serviceProvider)
        {
            _customerService = customerService;
            _organizationService = organizationService;
            _personService = personService;
            _userInfo = userInfo;
            _logger = logger;
            _accountManagerService = accountManagerService;
            _marketingAgreements = UnitOfWork.RepositoryAsync<MarketingAgreement>();
            _customers = UnitOfWork.RepositoryAsync<Customer>();
            _organizations = UnitOfWork.RepositoryAsync<Organization>();
            _leads = UnitOfWork.RepositoryAsync<Lead>();
            _applicationUsers = UnitOfWork.RepositoryAsync<ApplicationUser>();
        }
        

        public async Task<int> GetNextAccountId(
            Guid organizationId)
        {
            if (await Repository.Queryable().IgnoreQueryFilters()
                .Where(x => x.AccountManagerOrganizationId == organizationId).AnyAsync())
            {
                var number = await Repository.Queryable()
                   
                    .Where(x => x.AccountManagerOrganizationId == organizationId)
                    .IgnoreQueryFilters()
                    .Select(x => x.Number)
                    .MaxAsync();

                return number + 1;
            }

            return 1000;
        }

        public async Task<int> GetNextBuyerAccountId(
            Guid organizationId)
        {
            if (await Repository.Queryable().IgnoreQueryFilters()
                .Where(x => x.CustomerOrganizationId == organizationId).AnyAsync())
            {
                var number = await Repository.Queryable()

                    .Where(x => x.CustomerOrganizationId == organizationId)
                    .IgnoreQueryFilters()
                    .Select(x => x.BuyerNumber)
                    .MaxAsync();

                return number + 1;
            }

            return 1000;
        }

        public Task<PackedList<T>> GetAccounts<T>(
            IOrganizationAccountManager am, CommonFilters filters)
            where T : AccountManagerCustomerAccountOutput
        {
            return Repository.Queryable()
                .ForOrganizationAccountManager(am)
                .OrderByDescending(x => x.Updated)
                .PaginateProjection<CustomerAccount, T>(filters, ProjectionMapping);
        }


        public Task<CustomerAccount> Get(Guid accountManagerOrganizationId, Guid accountManagerId,
            Guid customerOrganizationId, Guid customerId)
        {
            return Repository
                .Queryable()
                .Where(x => x.AccountManagerOrganizationId == accountManagerOrganizationId &&
                            x.AccountManagerId == accountManagerId)
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync();
        }

        public Task<T> GetAccount<T>(Guid accountManagerOrganizationId, Guid accountManagerId,
            Guid customerOrganizationId, Guid customerId)
            where T : CustomerAccountOutput
        {
            return Repository
                .Queryable()
                .Where(x => x.AccountManagerOrganizationId == accountManagerOrganizationId &&
                            x.AccountManagerId == accountManagerId && x.CustomerId == customerId && x.CustomerOrganizationId == customerOrganizationId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

       
        public Task<CustomerAccount> GetAccount(IProviderAgencyOwner agencyOwner, int accountId)
        {
            return Repository.Queryable()
                .ForAgencyOwner(agencyOwner)
                .Where(x => x.Number == accountId)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetAccount<T>(
            IOrganizationCustomer cu, int accountId)
            where T : CustomerCustomerAccountOutput
        {
            return Repository.Queryable()
                .ForOrganizationCustomer(cu)
                .Where(x => x.BuyerNumber == accountId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<CustomerAccount> GetAccount(IOrganizationAccountManager am, int accountId)
        {
            return Repository.Queryable()
                .ForOrganizationAccountManager(am)
                .Where(x => x.Number == accountId)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetAccount<T>(IProviderAgencyOwner agencyOwner, int accountId) where T : AgencyOwnerCustomerAccountOutput
        {
            return Repository.Queryable()
                .ForAgencyOwner(agencyOwner)
                .Where(x => x.Number == accountId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetAccount<T>(Guid organizationId, int accountId)
            where T : CustomerAccountOutput
        {
            return Repository.Queryable()
                .Where(x => x.Number == accountId && x.AccountManagerOrganizationId == organizationId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetAccount<T>(IOrganizationAccountManager organizationAccountManager, int accountId)
            where T : AccountManagerCustomerAccountOutput
        {
            return Repository.Queryable()
                .ForOrganizationAccountManager(organizationAccountManager)
                .Where(x => x.Number == accountId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<PackedList<T>> GetAccounts<T>(
            IOrganizationCustomer cu, CommonFilters filters)
            where T : CustomerCustomerAccountOutput
        {
            return Repository.Queryable()
                .ForOrganizationCustomer(cu)
                .OrderByDescending(x => x.Updated)
                .PaginateProjection<CustomerAccount, T>(filters, ProjectionMapping);
        }

        public Task<PackedList<T>> GetAccounts<T>(
            IProviderAgencyOwner ao, CommonFilters filters)
            where T : AgencyOwnerCustomerAccountOutput
        {
            return Repository.Queryable()
                .ForAgencyOwner(ao)
                .OrderByDescending(x => x.Updated)
                .PaginateProjection<CustomerAccount, T>(filters, ProjectionMapping);
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[CustomerAccountService.{callerName}] - {message}";
        }
        
    }
}