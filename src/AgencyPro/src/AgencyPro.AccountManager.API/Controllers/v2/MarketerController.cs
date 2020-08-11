// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Common;
using AgencyPro.Core.OrganizationPeople.Filters;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationMarketers;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.AccountManager.API.Controllers.v2
{
    public class MarketerController : OrganizationUserController
    {
        private readonly IOrganizationAccountManager _accountManager;
        private readonly IOrganizationMarketerService _marketerService;

        public MarketerController(
            IServiceProvider serviceProvider,
            IOrganizationAccountManager accountManager,
            IOrganizationMarketerService marketerService
            ) : base(serviceProvider)
        {
            _accountManager = accountManager;
            _marketerService = marketerService;
        }

        /// <summary>
        /// get marketers within an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ProducesResponseType(typeof(List<AccountManagerOrganizationMarketerOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMarketerList([FromRoute] Guid organizationId, [FromQuery]MarketerFilters filters)
        {
            var result =
                await _marketerService.GetForOrganization<AccountManagerOrganizationMarketerOutput>(_accountManager
                    .OrganizationId, filters);

            return Ok(result);
        }

        /// <summary>
        /// get a specific marketer in organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet("{personId}")]
        [ProducesResponseType(typeof(OrganizationMarketerStatistics), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMarketer([FromRoute] Guid organizationId, [FromRoute]Guid personId)
        {
            var result =
                await _marketerService
                    .GetById<OrganizationMarketerStatistics>(personId, _accountManager
                        .OrganizationId);

            return Ok(result);
        }

        /// <summary>
        /// get marketers within an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <param name="pagingFilters"></param>
        /// <returns></returns>
        [HttpGet("statistics")]
        [ProducesResponseType(typeof(List<OrganizationMarketerStatistics>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMarketerStatistics([FromRoute] Guid organizationId, 
            [FromQuery]MarketerFilters filters, [FromQuery]CommonFilters pagingFilters)
        {
            var result = await _marketerService
                .GetForOrganization<OrganizationMarketerStatistics>(_accountManager.OrganizationId, filters, pagingFilters);
            AddPagination(pagingFilters, result.Total);
            return Ok(result.Data);
        }
    }
}
