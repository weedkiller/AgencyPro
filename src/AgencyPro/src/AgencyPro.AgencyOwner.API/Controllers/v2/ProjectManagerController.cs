// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Common;
using AgencyPro.Core.OrganizationPeople.Filters;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationProjectManagers;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class ProjectManagerController : OrganizationUserController
    {
        private readonly IAgencyOwner _agencyOwner;
        private readonly IOrganizationProjectManagerService _projectManagerService;

        public ProjectManagerController(IAgencyOwner agencyOwner, IOrganizationProjectManagerService projectManagerService, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _agencyOwner = agencyOwner;
            _projectManagerService = projectManagerService;
        }

        /// <summary>
        /// removes a person from the project manager role within an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpDelete("{personId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProjectManager([FromRoute] Guid organizationId, [FromRoute]Guid personId)
        {
            if (personId == _agencyOwner.CustomerId)
            {
                ModelState.AddModelError("", "You cannot remove yourself");
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var result = await _projectManagerService.Remove(_agencyOwner, personId);

            return Ok(result);
        }

        /// <summary>
        ///     Updates a project manager within an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="personId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPatch("{personId}")]
        
        [ProducesResponseType(typeof(List<AgencyOwnerOrganizationProjectManagerOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProjectManager([FromRoute] Guid organizationId, [FromRoute] Guid personId,
            [FromBody] OrganizationProjectManagerInput input)
        {
            var result = await _projectManagerService
                .Update<AgencyOwnerOrganizationProjectManagerOutput>(_agencyOwner, input);
            return Ok(result);
        }

        /// <summary>
        ///     Gets project managers within an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ProducesResponseType(typeof(List<AgencyOwnerOrganizationProjectManagerOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectManagerList([FromRoute] Guid organizationId, [FromQuery]ProjectManagerFilters filters)
        {
            var result =
                await _projectManagerService.GetForOrganization<AgencyOwnerOrganizationProjectManagerOutput>(
                    _agencyOwner.OrganizationId, filters);

            return Ok(result);
        }


        /// <summary>
        /// get a specific project manager in organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet("{personId}")]
        [ProducesResponseType(typeof(OrganizationProjectManagerStatistics), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectManager([FromRoute] Guid organizationId, [FromRoute]Guid personId)
        {
            var result =
                await _projectManagerService
                    .GetById<OrganizationProjectManagerStatistics>(personId, _agencyOwner
                        .OrganizationId);

            return Ok(result);
        }

        /// <summary>
        ///     Gets project managers within an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filter"></param>
        /// <param name="pagingFilters"></param>
        /// <returns></returns>
        [HttpGet("statistics")]
        [ProducesResponseType(typeof(List<OrganizationProjectManagerStatistics>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectManagerStatistics([FromRoute] Guid organizationId, 
            [FromQuery]ProjectManagerFilters filter, [FromQuery] CommonFilters pagingFilters)
        {
            var result = await _projectManagerService
                .GetForOrganization<OrganizationProjectManagerStatistics>(_agencyOwner.OrganizationId, filter, pagingFilters);
            AddPagination(pagingFilters, result.Total);
            return Ok(result.Data);
        }

        /// <summary>
        /// adds an existing project manager to an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="personId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{personId}")]
        
        [ProducesResponseType(typeof(List<AgencyOwnerOrganizationProjectManagerOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProjectManager([FromRoute] Guid organizationId, [FromRoute] Guid personId,
            [FromBody] OrganizationProjectManagerInput input)
        {
            var result = await _projectManagerService
                .Create<AgencyOwnerOrganizationProjectManagerOutput>(_agencyOwner, input);
            return Ok(result);
        }
    }
}
