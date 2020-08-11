// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Skills.Services;
using AgencyPro.Core.Skills.ViewModels;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class SkillController : OrganizationUserController
    {
        private readonly IAgencyOwner _agencyOwner;
        private readonly IOrganizationSkillService _organizationSkillService;

        public SkillController(IServiceProvider serviceProvider,
            IAgencyOwner agencyOwner,
            IOrganizationSkillService organizationSkillService) : base(serviceProvider)
        {
            _agencyOwner = agencyOwner;
            _organizationSkillService = organizationSkillService;
        }

        /// <summary>
        /// Gets the skills of the organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<OrganizationSkillOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSkills([FromRoute] Guid organizationId)
        {
            var output = await _organizationSkillService.GetSkills(organizationId);
            return Ok(output);
        }


        /// <summary>
        /// Add skill to an organization 
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="skillId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{skillId}")]
        [ProducesResponseType(typeof(OrganizationSkillOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddSkill(
            [FromRoute]Guid organizationId, [FromRoute]Guid skillId,
            [FromBody]OrganizationSkillInput input)
        {
            var result = await _organizationSkillService.Add(_agencyOwner, skillId, input);
            return Ok(result);
        }

        /// <summary>
        /// Update the organization skill
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="skillId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPatch("{skillId}")]
        [ProducesResponseType(typeof(OrganizationSkillOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateSkill(
            [FromRoute]Guid organizationId, [FromRoute]Guid skillId,
            [FromBody]OrganizationSkillInput input)
        {
            var result = await _organizationSkillService.Update(_agencyOwner, skillId, input);
            return Ok(result);
        }


        /// <summary>
        /// Removes a skill from an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="skillId"></param>
        /// <returns></returns>
        [HttpDelete("{skillId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]

        public async Task<IActionResult> RemoveSkill(
            [FromRoute]Guid organizationId, [FromRoute]Guid skillId)
        {
            var result = await _organizationSkillService.Remove(_agencyOwner, skillId);
            return Ok(result);
        }

    }
}
