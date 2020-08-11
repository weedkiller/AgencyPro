// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Organizations.MarketingOrganizations.ViewModels;
using AgencyPro.Core.Organizations.Services;
using AgencyPro.Core.Roles.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Marketer.API.Controllers.v2
{
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("agency")]
    [Produces("application/json")]
    public class AgencyController : ControllerBase
    {
        private readonly IMarketer _marketer;
        private readonly IOrganizationService _organizationService;

        public AgencyController(IMarketer marketer,
            IOrganizationService organizationService)
        {
            _marketer = marketer;
            _organizationService = organizationService;
        }


        /// <summary>
        /// get organizations that marketer could join
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<MarketerOrganizationOutput>), StatusCodes.Status200OK)]
        [HttpGet()]
        public async Task<IActionResult> GetOrganizations()
        {
            var org = await _organizationService.GetOrganizations<MarketerOrganizationOutput>(_marketer);
            return Ok(org);
        }

    }
}