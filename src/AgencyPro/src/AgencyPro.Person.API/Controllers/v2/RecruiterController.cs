// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Threading.Tasks;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Roles.ViewModels.Recruiters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Person.API.Controllers.v2
{
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("recruiter")]
    [Produces("application/json")]
    public class RecruiterController : ControllerBase
    {
        private readonly IRecruiter _recruiter;
        private readonly IRecruiterService _service;

        public RecruiterController(IRecruiter recruiter, IRecruiterService service)
        {
            _service = service;
            _recruiter = recruiter;
        }

        [HttpPatch]
        [ProducesResponseType(typeof(RecruiterDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody]RecruiterUpdateInput input)
        {
            var result = await _service.Update<RecruiterDetailsOutput>(_recruiter, input);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(RecruiterDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetById<RecruiterDetailsOutput>(_recruiter.Id);
            return Ok(result);
        }
    }
}