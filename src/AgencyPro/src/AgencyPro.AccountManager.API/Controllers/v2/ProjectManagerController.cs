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

namespace AgencyPro.AccountManager.API.Controllers.v2
{
    public class ProjectManagerController : OrganizationUserController
    {
        private readonly IOrganizationProjectManagerService _projectManagerService;
        private readonly IOrganizationAccountManager _accountManager;

        public ProjectManagerController(
            IOrganizationAccountManager accountManager, 
            IOrganizationProjectManagerService projectManagerService, 
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _accountManager = accountManager;
            _projectManagerService = projectManagerService;
        }

        /// <summary>
        ///     Gets project managers within an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ProducesResponseType(typeof(List<AccountManagerOrganizationProjectManagerOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectManagerList([FromRoute] Guid organizationId, [FromQuery]ProjectManagerFilters filters)
        {
            var result =
                await _projectManagerService.GetForOrganization<AccountManagerOrganizationProjectManagerOutput>(
                    _accountManager.OrganizationId, filters);

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
                    .GetById<OrganizationProjectManagerStatistics>(personId, _accountManager
                        .OrganizationId);

            return Ok(result);
        }

        /// <summary>
        ///     Gets project managers within an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <param name="pagingFilters"></param>
        /// <returns></returns>
        [HttpGet("statistics")]
        [ProducesResponseType(typeof(List<OrganizationProjectManagerStatistics>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectManagerStatistics([FromRoute] Guid organizationId, 
            [FromQuery]ProjectManagerFilters filters, [FromQuery]CommonFilters pagingFilters)
        {
            var result = await _projectManagerService
                .GetForOrganization<OrganizationProjectManagerStatistics>(_accountManager.OrganizationId, filters, pagingFilters);
            AddPagination(pagingFilters, result.Total);
            return Ok(result.Data);
        }
    }
}
