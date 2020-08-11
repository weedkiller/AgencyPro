// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Leads.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Leads.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationMarketers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace AgencyPro.Agency.API.Controllers
{
    [AllowAnonymous]
    [Route("Lead")]
    public class LeadController : ControllerBase
    {
        private readonly ILeadService _leadService;
        private readonly IOrganizationMarketerService _marketerService;

        public LeadController(ILeadService leadService, IOrganizationMarketerService marketerService)
        {
            _marketerService = marketerService;
            _leadService = leadService;
        }

        /// <summary>
        /// Submit a lead to an organization with referral code
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="referralCode"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{organizationId}")]
        [ProducesResponseType(typeof(LeadResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateLead([FromRoute] Guid organizationId, [FromQuery]string referralCode, [FromBody]LeadInput input)
        {
            var marketer =
                await _marketerService.GetMarketerOrDefault<OrganizationMarketerOutput>(organizationId, null,
                    referralCode);

            var lead = await _leadService.CreateInternalLead(marketer, input);

            return Ok(lead);
        }
    }
}
