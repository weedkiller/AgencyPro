// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Organizations.RecruitingOrganizations.ViewModels;
using AgencyPro.Core.Organizations.Services;
using AgencyPro.Core.Roles.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Recruiter.API.Controllers.v2
{
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("agency")]
    [Produces("application/json")]
    public class AgencyController : ControllerBase
    {
        private readonly IRecruiter _recruiter;
        private readonly IOrganizationService _organizationService;

        public AgencyController(IRecruiter recruiter,
            IOrganizationService organizationService)
        {
            _recruiter = recruiter;
            _organizationService = organizationService;
        }


        /// <summary>
        /// get organizations that marketer could join
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<RecruiterOrganizationOutput>), StatusCodes.Status200OK)]
        [HttpGet()]
        public async Task<IActionResult> GetOrganizations()
        {
            var org = await _organizationService.GetOrganizations<RecruiterOrganizationOutput>(_recruiter);
            return Ok(org);
        }

    }
}