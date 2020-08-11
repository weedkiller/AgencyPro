// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Orders.Events;
using AgencyPro.Core.Orders.Model;
using AgencyPro.Core.Orders.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyPro.Services.Orders
{
    public partial class WorkOrderService
    {
        private async Task<T> Reject<T>(WorkOrder workOrder)
        where T : WorkOrderOutput
        {
            _logger.LogInformation(GetLogMessage("WO: {0}"), workOrder.Id);


            workOrder.ProviderResponseTime = DateTimeOffset.UtcNow;
            workOrder.Status = OrderStatus.Rejected;
            workOrder.Updated = DateTimeOffset.UtcNow;

            var result = await Repository.UpdateAsync(workOrder, true);

            _logger.LogDebug(GetLogMessage("{0} records updated"), result);
            if (result > 0)
            {
                await Task.Run(() => RaiseEvent(new WorkOrderRejectedEvent()
                {
                    WorkOrderId = workOrder.Id
                }));

            }

            return Repository.Queryable().Where(x => x.Id == workOrder.Id)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefault();


        }

        public async Task<T> Reject<T>(IOrganizationAccountManager accountManager, Guid workOrderId)
            where T : ProviderWorkOrderOutput
        {
            _logger.LogInformation(GetLogMessage("AM: {0}"), accountManager.OrganizationId);

            var a = await Repository.Queryable()
                .Where(x => x.AccountManagerId == accountManager.AccountManagerId
                            && x.AccountManagerOrganizationId == accountManager.OrganizationId
                            && x.Id == workOrderId)
                .FirstAsync();


            return await Reject<T>(a);
        }

        public async Task<T> Reject<T>(IAgencyOwner agencyOwner, Guid workOrderId) where T : ProviderWorkOrderOutput
        {
            _logger.LogInformation(GetLogMessage("AO: {0}"), agencyOwner.OrganizationId);

            var a = await Repository.Queryable()
                .Where(x => x.AccountManagerOrganizationId == agencyOwner.OrganizationId
                            && x.Id == workOrderId)
                .FirstAsync();

            return await Reject<T>(a);
        }
    }
}