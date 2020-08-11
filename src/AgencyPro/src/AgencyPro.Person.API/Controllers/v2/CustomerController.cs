// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Threading.Tasks;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Roles.ViewModels.Customers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Person.API.Controllers.v2
{
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("customer")]
    [Produces("application/json")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomer _principal;
        private readonly ICustomerService _service;

        public CustomerController(ICustomer principal, ICustomerService service)
        {
            _service = service;
            _principal = principal;
        }

        [HttpPatch]
        [ProducesResponseType(typeof(CustomerDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody]CustomerUpdateInput input)
        {
            var result = await _service.Update<CustomerDetailsOutput>(_principal, input);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomerDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetById<CustomerDetailsOutput>(_principal.Id);
            return Ok(result);
        }
    }
}