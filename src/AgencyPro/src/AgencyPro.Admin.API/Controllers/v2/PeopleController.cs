// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Admin.API.Controllers.v2
{
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("people")]
    [Produces("application/json")]
    public class PeopleController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPeople()
        {
            return Ok();
        }
    }
}
