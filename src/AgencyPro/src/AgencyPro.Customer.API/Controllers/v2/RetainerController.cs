// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Retainers.Services;
using AgencyPro.Core.Retainers.ViewModels;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Customer.API.Controllers.v2
{
    public class RetainerController : OrganizationUserController
    {
        private readonly IOrganizationCustomer _customer;
        private readonly IRetainerService _retainerService;


        public RetainerController(
            IOrganizationCustomer customer,
            IServiceProvider serviceProvider, IRetainerService retainerService) : base(serviceProvider)
        {
            _customer = customer;
            _retainerService = retainerService;
        }

        /// <summary>
        /// Gets retainer information
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("{projectId}")]
        [ProducesResponseType(typeof(RetainerDetails),200)]
        public async Task<IActionResult> GetRetainer([FromRoute]Guid organizationId, [FromRoute]Guid projectId)
        {
            var result = await _retainerService.GetRetainer(_customer, projectId);
            return Ok(result);
        }
    }
}