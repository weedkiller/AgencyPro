// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationPeople.Filters;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationAccountManagers;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.ProjectManager.API.Controllers.v2
{
    public class AccountManagerController : OrganizationUserController
    {
        private readonly IOrganizationAccountManagerService _accountManagerService;
        private readonly IOrganizationProjectManager _projectManager;

        public AccountManagerController(IServiceProvider serviceProvider, 
            IOrganizationAccountManagerService accountManagerService,
            IOrganizationProjectManager projectManager) : base(serviceProvider)
        {
            _accountManagerService = accountManagerService;
            _projectManager = projectManager;
        }

        /// <summary>
        ///    Get account managers within an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ProducesResponseType(typeof(List<ProjectManagerOrganizationAccountManagerOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccountManagerList([FromRoute] Guid organizationId, [FromQuery]AccountManagerFilters filters)
        {
            var result =
                await _accountManagerService
                    .GetForOrganization<ProjectManagerOrganizationAccountManagerOutput>(
                    _projectManager.OrganizationId, filters);

            return Ok(result);
        }
        
    }
}
