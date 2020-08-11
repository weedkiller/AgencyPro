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
using AgencyPro.Core.Orders;
using AgencyPro.Core.Orders.Model;
using AgencyPro.Core.Orders.Services;
using AgencyPro.Core.Orders.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Services.Orders.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Orders
{
    public partial class WorkOrderService : Service<WorkOrder>, IWorkOrderService
    {
        private readonly ILogger<WorkOrderService> _logger;

        public async Task<int> GetNextBuyerNumber(
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

        public async Task<int> GetNextProviderNumber(
            Guid organizationId)
        {
            if (await Repository.Queryable().IgnoreQueryFilters()
                .Where(x => x.AccountManagerOrganizationId == organizationId).AnyAsync())
            {
                var number = await Repository.Queryable()

                    .Where(x => x.AccountManagerOrganizationId == organizationId)
                    .IgnoreQueryFilters()
                    .Select(x => x.ProviderNumber)
                    .MaxAsync();

                return number + 1;
            }

            return 1000;
        }

        private readonly IRepositoryAsync<CustomerAccount> _customerAccountRepository;

        public WorkOrderService(
            MultiWorkOrderEventsHandler events,
            IServiceProvider serviceProvider, 
            ILogger<WorkOrderService> logger) : base(serviceProvider)
        {
            _logger = logger;
            _customerAccountRepository = UnitOfWork.RepositoryAsync<CustomerAccount>();

            AddEventHandler(events);
        }

        public Task<PackedList<T>> GetWorkOrders<T>(IOrganizationCustomer customer, WorkOrderFilters filters) where T : BuyerWorkOrderOutput
        {
            return Repository.Queryable()
                .ApplyWhereFilters(filters)
                .Where(x => x.CustomerId == customer.CustomerId
                    && x.CustomerOrganizationId == customer.OrganizationId)
                .PaginateProjection<WorkOrder, T>(filters, ProjectionMapping);
        }

        public Task<PackedList<T>> GetWorkOrders<T>(IOrganizationAccountManager accountManager, WorkOrderFilters filters) where T : ProviderWorkOrderOutput
        {
            return Repository.Queryable()
                .ApplyWhereFilters(filters)
                .Where(x => x.AccountManagerId == accountManager.AccountManagerId
                    && x.AccountManagerOrganizationId == accountManager.OrganizationId
                    && x.Status != OrderStatus.Draft)
                .PaginateProjection<WorkOrder, T>(filters, ProjectionMapping);
        }

        public Task<PackedList<T>> GetWorkOrders<T>(IAgencyOwner agencyOwner, WorkOrderFilters filters) where T : ProviderWorkOrderOutput
        {
            return Repository.Queryable()
                .ApplyWhereFilters(filters)
                .Where(x => agencyOwner.OrganizationId == x.AccountManagerOrganizationId 
                    && x.Status != OrderStatus.Draft)
                .PaginateProjection<WorkOrder, T>(filters, ProjectionMapping);
        }

        public Task<T> GetWorkOrder<T>(IOrganizationCustomer customer, Guid orderId) where T : BuyerWorkOrderOutput
        {
            return Repository.Queryable().Where(x =>
                    x.CustomerId == customer.CustomerId && x.CustomerOrganizationId == customer.OrganizationId && x.Id == orderId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstAsync();
        }

        public Task<T> GetWorkOrder<T>(IOrganizationAccountManager accountManager, Guid orderId) where T : ProviderWorkOrderOutput
        {
            return Repository.Queryable().Where(x =>
                    x.AccountManagerId == accountManager.AccountManagerId && x.AccountManagerOrganizationId == accountManager.OrganizationId && x.Id == orderId && x.Status != OrderStatus.Draft)
                .ProjectTo<T>(ProjectionMapping)
                .FirstAsync();
        }

        public Task<T> GetWorkOrder<T>(IAgencyOwner agencyOwner, Guid orderId) where T : ProviderWorkOrderOutput
        {
            return Repository.Queryable().Where(x =>
                    x.AccountManagerOrganizationId == agencyOwner.OrganizationId && x.Id == orderId && x.Status != OrderStatus.Draft)
                .ProjectTo<T>(ProjectionMapping)
                .FirstAsync();
        }


        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(WorkOrderService)}.{callerName}] - {message}";
        }
    }
}
