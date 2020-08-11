// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Positions.Services;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AgencyPro.Core.Positions.ViewModels;

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class PositionController : OrganizationUserController
    {
        private readonly IAgencyOwner _agencyOwner;
        private readonly IPositionService _positionService;

        public PositionController(IServiceProvider serviceProvider,
            IAgencyOwner agencyOwner,
            IPositionService positionService) : base(serviceProvider)
        {
            _agencyOwner = agencyOwner;
            _positionService = positionService;
        }

        /// <summary>
        /// Gets the positions of the organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<OrganizationPositionOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPositions([FromRoute] Guid organizationId)
        {
            var output = await _positionService.GetPositions(organizationId);
            return Ok(output);
        }


        /// <summary>
        /// Add position to an organization 
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="positionId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{positionId}")]
        [ProducesResponseType(typeof(OrganizationPositionOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddPosition(
            [FromRoute]Guid organizationId, [FromRoute]int positionId)
        {
            var result = await _positionService.Add(_agencyOwner, positionId);
            return Ok(result);
        }
        

        /// <summary>
        /// Removes a position from an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="positionId"></param>
        /// <returns></returns>
        [HttpDelete("{positionId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> RemovePosition(
            [FromRoute]Guid organizationId, [FromRoute]int positionId)
        {
            var result = await _positionService.Remove(_agencyOwner, positionId);
            return Ok(result);
        }

    }
}
