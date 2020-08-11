// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Commission.ViewModels;
using AgencyPro.Core.CustomerBalance.Services;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Customer.API.Controllers.v2
{
    public class BalanceController : OrganizationUserController
    {
        private readonly IOrganizationCustomer _customer;
        private readonly ICustomerBalanceService _balanceService;

        public BalanceController(IServiceProvider serviceProvider, 
            IOrganizationCustomer customer,
            ICustomerBalanceService balanceService) : base(serviceProvider)
        {
            _customer = customer;
            _balanceService = balanceService;
        }

        /// <summary>
        /// Get customer balance
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("balance")]
        [ProducesResponseType(typeof(StreamOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Balance([FromRoute]Guid organizationId)
        {
            var commission = await _balanceService.GetBalance(_customer);

            return Ok(commission);
        }
    }
}