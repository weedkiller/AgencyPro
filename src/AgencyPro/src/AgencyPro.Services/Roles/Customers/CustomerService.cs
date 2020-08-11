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
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Roles.Extensions;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Roles.ViewModels.Customers;
using AgencyPro.Services.Roles.Contractors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Roles.Customers
{
    public partial class CustomerService : Service<Customer>, ICustomerService
    {
        private readonly ILogger<ContractorService> _logger;
        private readonly IRepositoryAsync<OrganizationMarketer> _marketerRepository;

        public CustomerService(
            IServiceProvider serviceProvider,
            ILogger<ContractorService> logger) : base(serviceProvider)
        {
            _logger = logger;
            _marketerRepository = UnitOfWork.RepositoryAsync<OrganizationMarketer>();
        }
        
        public Task<T> GetById<T>(Guid customerId)
            where T : CustomerOutput
        {
            return Repository.Queryable()
                .FindById(customerId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<CustomerOutput> Get(Guid id)
        {
            return GetById<CustomerOutput>(id);
        }
        
        public async Task<ICustomer> GetPrincipal(Guid customerId)
        {
            var principal = await Get(customerId);
            return principal;
        }

        public Task<PackedList<T>> GetList<T>(IOrganizationMarketer ma, CommonFilters filters)
            where T : MarketerCustomerOutput
        {
            return Repository.Queryable()
                .ForOrganizationMarketer(ma)
                .OrderByDescending(x => x.Updated)
                .PaginateProjection<Customer, T>(filters, ProjectionMapping);
        }


        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[CustomerService.{callerName}] - {message}";
        }
    }
}