// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.ProjectManager.API.Controllers.v2
{
    public class OrganizationController : OrganizationUserController
    {
        private readonly IOrganizationProjectManager _projectManager;
        private readonly IOrganizationProjectManagerService _projectManagerService;

        public OrganizationController(
            IOrganizationProjectManager projectManager,
            IOrganizationProjectManagerService projectManagerService,
            IServiceProvider provider) : base(provider)
        {
            _projectManager = projectManager;
            _projectManagerService = projectManagerService;
        }

        [ProducesResponseType(typeof(ProjectManagerOrganizationOutput), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetOrganizationInfo(
            [FromRoute] Guid organizationId
        )
        {
            var org = await _projectManagerService
                .GetOrganization<ProjectManagerOrganizationOutput>(_projectManager);
            return Ok(org);
        }

        [HttpGet("counts")]
        [ProducesResponseType(typeof(ProjectManagerCounts), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCounts([FromRoute] Guid organizationId)
        {
            var counts = await _projectManagerService.GetCounts(_projectManager);

            return Ok(counts);
        }
    }
}