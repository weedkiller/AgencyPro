// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Roles.ViewModels.Contractors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Person.API.Controllers.v2
{
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("contractor")]
    [Produces("application/json")]
    public class ContractorController : ControllerBase
    {
        private readonly IContractor _principal;
        private readonly IContractorService _service;

        public ContractorController(IContractor principal, IContractorService service)
        {
            _service = service;
            _principal = principal;
        }

        /// <summary>
        /// updates a contractor profile
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPatch]
        [ProducesResponseType(typeof(ContractorDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody] ContractorUpdateInput input)
        {
            var result = await _service.Update<ContractorDetailsOutput>(_principal, input);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ContractorDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetById<ContractorDetailsOutput>(_principal.Id);
            return Ok(result);
        }
    }
}