// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Threading.Tasks;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Roles.ViewModels.Marketers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Person.API.Controllers.v2
{
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("marketer")]
    [Produces("application/json")]
    public class MarketerController : ControllerBase
    {
        private readonly IMarketer _marketer;
        private readonly IMarketerService _marketerService;

        public MarketerController(IMarketer marketer, IMarketerService marketerService)
        {
            _marketerService = marketerService;
            _marketer = marketer;
        }


        [HttpPatch]
        [ProducesResponseType(typeof(MarketerDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody]MarketerUpdateInput input)
        {
            var result = await _marketerService.Update<MarketerDetailsOutput>(_marketer, input);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(MarketerDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _marketerService.GetById<MarketerDetailsOutput>(_marketer.Id);
            return Ok(result);
        }
    }
}