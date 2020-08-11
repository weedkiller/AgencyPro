// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Orders.Model;
using AgencyPro.Core.Orders.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;
using Omu.ValueInjecter;

namespace AgencyPro.Services.Orders
{
    public partial class WorkOrderService
    {

        private async Task<T> UpdateWorkOrder<T>(WorkOrder workOrder, UpdateWorkOrderInput input = null) where T : WorkOrderOutput
        {
            if(input != null)
                workOrder.InjectFrom(input);

            workOrder.Updated = DateTimeOffset.UtcNow;

            var result = await Repository.UpdateAsync(workOrder, true);

            return Repository.Queryable().Where(x => x.Id == workOrder.Id)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefault();
        }

        public async Task<T> UpdateWorkOrder<T>(IOrganizationCustomer customer, Guid orderId, UpdateWorkOrderInput input)
            where T : BuyerWorkOrderOutput
        {
            var workOrder = await Repository.Queryable()
                .Where(x => x.CustomerId == customer.CustomerId && x.Id == orderId)
                .FirstAsync();

            return await UpdateWorkOrder<T>(workOrder, input);
        }

        public async Task<T> UpdateWorkOrder<T>(IOrganizationAccountManager accountManager, Guid orderId,
            UpdateWorkOrderInput input) where T : ProviderWorkOrderOutput
        {
            var workOrder = await Repository.Queryable()
                .Where(x => x.AccountManagerId == accountManager.AccountManagerId && x.Id == orderId)
                .FirstAsync();

            return await UpdateWorkOrder<T>(workOrder, input);
        }

        public async Task<T> UpdateWorkOrder<T>(IAgencyOwner agencyOwner, Guid orderId, UpdateWorkOrderInput input)
            where T : ProviderWorkOrderOutput
        {
            var workOrder = await Repository.Queryable()
                .Where(x => x.AccountManagerOrganizationId == agencyOwner.OrganizationId && x.Id == orderId)
                .FirstAsync();

            return await UpdateWorkOrder<T>(workOrder, input);
        }
    }
}