// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Admin.API.Controllers.v2
{
    [Route("organization")]
    public class OrganizationController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult GetOrganizations()
        {
            return Ok();
        }

        [HttpGet("{organizationId}")]
        public IActionResult GetOrganization([FromRoute]Guid organizationId)
        {
            return Ok();
        }
    }
}
