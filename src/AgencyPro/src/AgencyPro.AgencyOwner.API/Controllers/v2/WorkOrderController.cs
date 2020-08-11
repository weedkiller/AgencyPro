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

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class WorkOrderController : OrganizationUserController
    {
        private readonly IAgencyOwner _agencyOwner;
        private readonly IWorkOrderService _workOrderService;

        public WorkOrderController(
            IAgencyOwner agencyOwner,
            IWorkOrderService workOrderService,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _agencyOwner = agencyOwner;
            _workOrderService = workOrderService;
        }

        /// <summary>
        ///  get's all work orders submitted by customer
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ProviderWorkOrderOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWorkOrders([FromRoute] Guid organizationId, [FromQuery]WorkOrderFilters filters)
        {
            var orders = await _workOrderService.GetWorkOrders<ProviderWorkOrderOutput>(_agencyOwner, filters);
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
        [ProducesResponseType(typeof(ProviderWorkOrderOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateWorkOrder([FromRoute] Guid organizationId, [FromRoute]Guid workOrderId, [FromBody]UpdateWorkOrderInput input)
        {
            var order = await _workOrderService.UpdateWorkOrder<ProviderWorkOrderOutput>(_agencyOwner, workOrderId,
                input);
            return Ok(order);
        }

        /// <summary>
        /// Gets work order by id
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="workOrderId"></param>
        /// <returns></returns>
        [HttpGet("{workOrderId}")]
        [ProducesResponseType(typeof(ProviderWorkOrderOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWorkOrders([FromRoute] Guid organizationId, [FromRoute]Guid workOrderId)
        {
            var order = await _workOrderService
                .GetWorkOrder<ProviderWorkOrderOutput>(_agencyOwner, workOrderId);

            return Ok(order);
        }

        /// <summary>
        /// accept the work order
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="workOrderId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPatch("{workOrderId}/accept")]
        [ProducesResponseType(typeof(ProviderWorkOrderOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> AcceptWorkOrder([FromRoute] Guid organizationId, [FromRoute]Guid workOrderId, [FromBody]WorkOrderAcceptInput input)
        {
            var order = await _workOrderService
                .AcceptWorkOrder<ProviderWorkOrderOutput>(_agencyOwner, workOrderId, input);

            return Ok(order);
        }

        /// <summary>
        /// reject the work order
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="workOrderId"></param>
        /// <returns></returns>
        [HttpPatch("{workOrderId}/reject")]
        [ProducesResponseType(typeof(ProviderWorkOrderOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> RejectWorkOrder([FromRoute] Guid organizationId, [FromRoute]Guid workOrderId)
        {
            var order = await _workOrderService
                .Reject<ProviderWorkOrderOutput>(_agencyOwner, workOrderId);

            return Ok(order);
        }
    }
}