// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.RecruitingOrganizations.ViewModels;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Recruiter.API.Controllers.v2
{
    public class OrganizationController : OrganizationUserController
    {
        private readonly IOrganizationRecruiter _recruiter;
        private readonly IOrganizationRecruiterService _recruiterService;

        public OrganizationController(
            IOrganizationRecruiter recruiter,
            IOrganizationRecruiterService recruiterService,
            IServiceProvider provider) : base(provider)
        {
            _recruiter = recruiter;
            _recruiterService = recruiterService;
        }

        /// <summary>
        /// get organization from recruiter perspective
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(RecruiterOrganizationOutput), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetOrganizationInfo(
            [FromRoute] Guid organizationId
        )
        {
            var org = await _recruiterService.GetOrganization<RecruiterOrganizationOutput>(_recruiter);
            return Ok(org);
        }

        /// <summary>
        /// Get counts for recruiter
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("counts")]
        [ProducesResponseType(typeof(RecruiterCounts), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCounts([FromRoute] Guid organizationId)
        {
            var counts = await _recruiterService.GetCounts(_recruiter);

            return Ok(counts);
        }
    }
}