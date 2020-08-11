// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Orders.Events;
using AgencyPro.Core.Orders.Model;
using AgencyPro.Core.Orders.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace AgencyPro.Services.Orders
{
    public partial class WorkOrderService
    {
        public async Task<T> CreateWorkOrder<T>(IOrganizationCustomer customer, WorkOrderInput input)
            where T : BuyerWorkOrderOutput
        {

            _logger.LogInformation(GetLogMessage("Cu: {0}"), customer.OrganizationId);

            //var account = await _customerAccountRepository.Queryable()
            //    .Where(x => x.AccountManagerOrganizationId == input.AccountManagerOrganizationId
            //                && x.AccountManagerId == input.AccountManagerId && x.CustomerId == customer.CustomerId &&
            //                x.CustomerOrganizationId == customer.OrganizationId)
            //    .FirstAsync();

            var entity = new WorkOrder()
            {
                BuyerNumber = await GetNextBuyerNumber(customer.OrganizationId),
                ProviderNumber = await GetNextProviderNumber(input.AccountManagerOrganizationId),
                Status = input.IsDraft ? OrderStatus.Draft : OrderStatus.Sent ,
                CustomerId = customer.CustomerId,
                CustomerOrganizationId = customer.OrganizationId,
                AccountManagerId = input.AccountManagerId,
                AccountManagerOrganizationId = input.AccountManagerOrganizationId
            }.InjectFrom(input) as WorkOrder;

            var result = await Repository.InsertAsync(entity, true);
            _logger.LogDebug(GetLogMessage("{0} records updated"), result);
            if (result > 0)
            {
                await Task.Run(() => RaiseEvent(new WorkOrderCreatedEvent()
                {
                    WorkOrderId = entity.Id
                }));
            } 

            var output = await Repository.Queryable()
                .Where(x => x.Id == entity.Id)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();

            return output;
        }
    }
}