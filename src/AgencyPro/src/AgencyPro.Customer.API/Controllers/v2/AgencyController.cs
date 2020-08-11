// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Organizations.Services;
using AgencyPro.Core.Organizations.ViewModels;
using AgencyPro.Core.Roles.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Customer.API.Controllers.v2
{
    [Route("{version}/organization")]
    public class AgencyController : ControllerBase
    {
        private readonly IOrganizationService _service;
        private readonly ICustomer _customer;

        public AgencyController(IOrganizationService service, ICustomer customer)
        {
            _service = service;
            _customer = customer;
        }

        /// <summary>
        /// Creates a new agency
        /// </summary>
        /// <param name="version"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(CustomerOrganizationOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAgency([FromRoute] string version, [FromBody]OrganizationCreateInput input)
        {
            var result = await _service.CreateOrganization(_customer, input);
            if (result.Succeeded)
            {
                var org = await _service.GetOrganization<CustomerOrganizationOutput>(result.OrganizationId.Value);
                return Ok(org);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return Ok(result);
        }
    }
}