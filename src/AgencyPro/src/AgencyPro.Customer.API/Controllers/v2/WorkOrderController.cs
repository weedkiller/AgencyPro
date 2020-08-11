// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Orders;
using AgencyPro.Core.Orders.Services;
using AgencyPro.Core.Orders.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Customer.API.Controllers.v2
{
    public class WorkOrderController : OrganizationUserController
    {
        private readonly IOrganizationCustomer _customer;
        private readonly IWorkOrderService _workOrderService;

        public WorkOrderController(
            IOrganizationCustomer customer,
            IWorkOrderService workOrderService,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _customer = customer;
            _workOrderService = workOrderService;
        }

        /// <summary>
        ///  get's all work orders submitted by customer
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<BuyerWorkOrderOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWorkOrders([FromRoute] Guid organizationId, [FromQuery]WorkOrderFilters filters)
        {
            var orders = await _workOrderService.GetWorkOrders<BuyerWorkOrderOutput>(_customer, filters);
            AddPagination(filters, orders.Total);
            return Ok(orders.Data);
        }


        /// <summary>
        /// Update work order
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="workOrderId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPatch("{workOrderId}")]
        [ProducesResponseType(typeof(BuyerWorkOrderOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateWorkOrder([FromRoute] Guid organizationId, [FromRoute]Guid workOrderId, [FromBody]UpdateWorkOrderInput input)
        {
            var order = await _workOrderService.UpdateWorkOrder<BuyerWorkOrderOutput>(_customer, workOrderId,
                input);
            return Ok(order);
        }

        /// <summary>
        /// create a work order
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(BuyerWorkOrderOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateWorkOrder([FromRoute] Guid organizationId, [FromBody]WorkOrderInput input)
        {
            var order = await _workOrderService.CreateWorkOrder<BuyerWorkOrderOutput>(_customer, input);
            return Ok(order);
        }

        /// <summary>
        /// Gets work order by id
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="workOrderId"></param>
        /// <returns></returns>
        [HttpGet("{workOrderId}")]
        [ProducesResponseType(typeof(BuyerWorkOrderOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWorkOrders([FromRoute] Guid organizationId, [FromRoute]Guid workOrderId)
        {
            var order = await _workOrderService
                .GetWorkOrder<BuyerWorkOrderOutput>(_customer, workOrderId);

            return Ok(order);
        }

        [HttpPatch("{workOrderId}/send")]
        [ProducesResponseType(typeof(BuyerWorkOrderOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> SendWorkOrder([FromRoute] Guid organizationId, [FromRoute]Guid workOrderId)
        {
            var order = await _workOrderService
                .SendWorkOrder<BuyerWorkOrderOutput>(_customer, workOrderId);

            return Ok(order);
        }

        /// <summary>
        /// revoke work order that has already been sent
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="workOrderId"></param>
        /// <returns></returns>
        [HttpPatch("{workOrderId}/revoke")]
        [ProducesResponseType(typeof(BuyerWorkOrderOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> RevokeWorkOrder([FromRoute] Guid organizationId, [FromRoute]Guid workOrderId)
        {
            var order = await _workOrderService
                .Revoke<BuyerWorkOrderOutput>(_customer, workOrderId);

            return Ok(order);
        }
    }
}