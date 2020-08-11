// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Common;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Roles.ViewModels.Customers;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Marketer.API.Controllers.v2
{
    public class CustomerController : OrganizationUserController
    {
        private readonly ICustomerService _customerService;
        private readonly IOrganizationMarketer _marketer;

        public CustomerController(ICustomerService customerService,
            IOrganizationMarketer marketer,
            IServiceProvider provider) : base(provider)
        {
            _marketer = marketer;
            _customerService = customerService;
        }

        /// <summary>
        ///     Gets all the customers for the current marketer
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<MarketerCustomerOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCustomers(
            [FromRoute] Guid organizationId, [FromQuery] CommonFilters filters)
        {
            var marketers = await _customerService.GetList<MarketerCustomerOutput>(_marketer, filters);
            AddPagination(filters, marketers.Total);
            return Ok(marketers.Data);
        }
    }
}