// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.MarketingOrganizations.ViewModels;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Marketer.API.Controllers.v2
{
    public class OrganizationController : OrganizationUserController
    {
        private readonly IOrganizationMarketer _orgMarketer;
        private readonly IOrganizationMarketerService _marketerService;

        public OrganizationController(
            IOrganizationMarketer orgMarketer,
            IOrganizationMarketerService marketerService,
            IServiceProvider provider) : base(provider)
        {
            _orgMarketer = orgMarketer;
            _marketerService = marketerService;
        }

        [ProducesResponseType(typeof(MarketerOrganizationOutput), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetOrganizationInfo(
            [FromRoute] Guid organizationId
        )
        {
            var org = await _marketerService.GetOrganization<MarketerOrganizationOutput>(_orgMarketer);
            return Ok(org);
        }
        
        /// <summary>
        /// Get counts for marketer
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("counts")]
        [ProducesResponseType(typeof(MarketerCounts), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCounts([FromRoute] Guid organizationId)
        {
            var counts = await _marketerService.GetCounts(_orgMarketer);

            return Ok(counts);
        }
    }
}