// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Orders.Model;
using AgencyPro.Core.Orders.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Services.Orders
{
    public partial class WorkOrderService
    {
        public async Task<T> Revoke<T>(IOrganizationCustomer customer, Guid orderId) where T : BuyerWorkOrderOutput
        {
            var workOrder = await Repository.Queryable()
                .Where(x => x.CustomerId == customer.CustomerId && x.Id == orderId)
                .FirstAsync();

            if (workOrder.Status == OrderStatus.Sent)
                workOrder.Status = OrderStatus.Draft;

            return await UpdateWorkOrder<T>(workOrder);
        }
    }
}