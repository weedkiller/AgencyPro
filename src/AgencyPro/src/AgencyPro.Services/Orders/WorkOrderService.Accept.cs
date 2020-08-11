// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Orders.Events;
using AgencyPro.Core.Orders.Model;
using AgencyPro.Core.Orders.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Orders
{
    public partial class WorkOrderService
    {
        private async Task<T> AcceptWorkOrder<T>(WorkOrder workOrder) where T : WorkOrderOutput
        {
            workOrder.Updated = DateTimeOffset.UtcNow;
            workOrder.Status = OrderStatus.AwaitingProposal;
            workOrder.ProviderResponseTime = DateTimeOffset.UtcNow;

            var result = await Repository.UpdateAsync(workOrder, true);

            _logger.LogDebug(GetLogMessage("{0} records updated"), result);
            if (result > 0)
            {
                await Task.Run(() => RaiseEvent(new WorkOrderAcceptedEvent()
                {
                    WorkOrderId = workOrder.Id
                }));
            }

            return Repository.Queryable().Where(x => x.Id == workOrder.Id)
                .ProjectTo<T>(ProjectionMapping)
                .First();

        }

        public async Task<T> AcceptWorkOrder<T>(IOrganizationAccountManager accountManager, Guid workOrderId,
            WorkOrderAcceptInput input) where T : ProviderWorkOrderOutput
        {
            var workOrder = await Repository.Queryable()
                .Where(x => x.AccountManagerId == accountManager.AccountManagerId && x.Id == workOrderId)
                .FirstAsync();

            return await AcceptWorkOrder<T>(workOrder);
        }

        public async Task<T> AcceptWorkOrder<T>(IAgencyOwner agencyOwner, Guid workOrderId, WorkOrderAcceptInput input)
            where T : ProviderWorkOrderOutput
        {
            var workOrder = await Repository.Queryable()
                .Where(x => x.AccountManagerOrganizationId == agencyOwner.OrganizationId && x.Id == workOrderId)
                .FirstAsync();
            
            return await AcceptWorkOrder<T>(workOrder);
        }
    }
}