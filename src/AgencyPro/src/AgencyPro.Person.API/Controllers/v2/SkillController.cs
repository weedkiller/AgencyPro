// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Skills.Services;
using AgencyPro.Core.Skills.ViewModels;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Person.API.Controllers.v2
{
    public class SkillController : OrganizationUserController
    {
        private readonly IContractor _contractor;
        private readonly IContractorSkillService _skills;

        public SkillController(IServiceProvider serviceProvider,
            IContractor contractor,
            IContractorSkillService skills) : base(serviceProvider)
        {
            _contractor = contractor;
            _skills = skills;
        }

        /// <summary>
        /// Gets the skills of the organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<ContractorSkillOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSkills([FromRoute] Guid organizationId)
        {
            var output = await _skills.GetSkills(_contractor);
            return Ok(output);
        }


        /// <summary>
        /// Assign skill to contractor
        /// </summary>
        /// <param name="skillId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{skillId}")]
        [ProducesResponseType(typeof(ContractorSkillOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddSkill([FromRoute]Guid skillId,
            [FromBody]ContractorSkillInput input)
        {
            var result = await _skills.Add(_contractor, skillId, input);
            return Ok(result);
        }

        /// <summary>
        /// Update contractor skill
        /// </summary>
        /// <param name="skillId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPatch("{skillId}")]
        [ProducesResponseType(typeof(ContractorSkillOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateSkill([FromRoute]Guid skillId,
            [FromBody]ContractorSkillInput input)
        {
            var result = await _skills.Update(_contractor, skillId, input);
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
            var result = await _skills.Remove(_contractor, skillId);
            return Ok(result);
        }

    }
}
