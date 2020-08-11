// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Common;
using AgencyPro.Core.OrganizationPeople.Filters;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationAccountManagers;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.AccountManager.API.Controllers.v2
{
    public class AccountManagerController : OrganizationUserController
    {
        private readonly IOrganizationAccountManagerService _accountManagerService;
        private readonly IOrganizationAccountManager _accountManager;

        public AccountManagerController(IServiceProvider serviceProvider, 
            IOrganizationAccountManagerService accountManagerService,
            IOrganizationAccountManager accountManager) : base(serviceProvider)
        {
            _accountManagerService = accountManagerService;
            _accountManager = accountManager;
        }

        /// <summary>
        ///    Gets all account managers in an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ProducesResponseType(typeof(List<AccountManagerOrganizationAccountManagerOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccountManagerList([FromRoute] Guid organizationId, 
            [FromQuery]AccountManagerFilters filters)
        {
            var result = await _accountManagerService
                    .GetForOrganization<AccountManagerOrganizationAccountManagerOutput>(_accountManager.OrganizationId, filters);
            
            return Ok(result);
        }

        /// <summary>
        ///    Gets all account managers in an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <param name="pagingFilters"></param>
        /// <returns></returns>
        [HttpGet("statistics")]
        [ProducesResponseType(typeof(List<OrganizationAccountManagerStatistics>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccountManagerStatistics([FromRoute] Guid organizationId,
            [FromQuery]AccountManagerFilters filters, [FromQuery]CommonFilters pagingFilters)
        {
            var result = await _accountManagerService
                    .GetForOrganization<OrganizationAccountManagerStatistics>(_accountManager.OrganizationId, filters, pagingFilters);
            AddPagination(pagingFilters, result.Total);
            return Ok(result.Data);
        }

        /// <summary>
        ///    Gets a specific account manager in an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet("{personId}")]
        [ProducesResponseType(typeof(AccountManagerOrganizationAccountManagerDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccountManager([FromRoute] Guid organizationId, [FromRoute]Guid personId)
        {
            var result =
                await _accountManagerService
                    .GetById<AccountManagerOrganizationAccountManagerDetailsOutput>(personId, organizationId);

            return Ok(result);
        }

        /// <summary>
        ///     Gets statistics about a specific account manager in an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet("statistics/{personId}")]
        [ProducesResponseType(typeof(OrganizationAccountManagerStatistics), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccountManagerStatistics([FromRoute] Guid organizationId, [FromRoute]Guid personId)
        {
            var result =
                await _accountManagerService
                    .GetById<OrganizationAccountManagerStatistics>(personId, organizationId);

            return Ok(result);
        }

    }
}
