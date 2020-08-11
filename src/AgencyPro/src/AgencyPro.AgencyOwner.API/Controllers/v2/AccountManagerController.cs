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

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class AccountManagerController : OrganizationUserController
    {
        private readonly IOrganizationAccountManagerService _accountManagerService;
        private readonly IAgencyOwner _agencyOwner;

        public AccountManagerController(IServiceProvider serviceProvider, 
            IOrganizationAccountManagerService accountManagerService,
            IAgencyOwner agencyOwner) : base(serviceProvider)
        {
            _accountManagerService = accountManagerService;
            _agencyOwner = agencyOwner;
        }

        /// <summary>
        ///    Get account managers within an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ProducesResponseType(typeof(List<AgencyOwnerOrganizationAccountManagerOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccountManagerList([FromRoute] Guid organizationId, [FromQuery]AccountManagerFilters filters)
        {
            var result =
                await _accountManagerService
                    .GetForOrganization<AgencyOwnerOrganizationAccountManagerOutput>(
                    _agencyOwner.OrganizationId, filters);

            return Ok(result);
        }

        /// <summary>
        ///    Get account managers within an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <param name="pagingFilters"></param>
        /// <returns></returns>
        [HttpGet("statistics")]
        [ProducesResponseType(typeof(List<OrganizationAccountManagerStatistics>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccountManagerStatistics([FromRoute] Guid organizationId,
            [FromQuery] AccountManagerFilters filters, [FromQuery] CommonFilters pagingFilters)
        {
            var result = await _accountManagerService
                    .GetForOrganization<OrganizationAccountManagerStatistics>(_agencyOwner.OrganizationId, filters, pagingFilters);
            AddPagination(pagingFilters, result.Total);
            return Ok(result.Data);
        }

        /// <summary>
        ///    Get a specific account manager within an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet("{personId}")]
        [ProducesResponseType(typeof(OrganizationAccountManagerStatistics), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccountManager([FromRoute] Guid organizationId, [FromRoute]Guid personId)
        {
            var result =
                await _accountManagerService
                    .GetById<OrganizationAccountManagerStatistics>(personId, organizationId);

            return Ok(result);
        }

        /// <summary>
        ///     Update an account manager within an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="personId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPatch("{personId}")]
        
        [ProducesResponseType(typeof(AgencyOwnerOrganizationAccountManagerOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAccountManager([FromRoute] Guid organizationId, [FromRoute] Guid personId,
            [FromBody] OrganizationAccountManagerInput input)
        {
            var result = await _accountManagerService
                .Update<AgencyOwnerOrganizationAccountManagerOutput>(_agencyOwner, input);
            return Ok(result);
        }

        /// <summary>
        /// adds an existing account manager to the org
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="personId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{personId}")]
        
        [ProducesResponseType(typeof(AgencyOwnerOrganizationAccountManagerOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAccountManager([FromRoute] Guid organizationId, [FromRoute] Guid personId,
            [FromBody] OrganizationAccountManagerInput input)
        {
            var result = await _accountManagerService
                .Create<AgencyOwnerOrganizationAccountManagerOutput>(_agencyOwner, input);
            return Ok(result);
        }

        /// <summary>
        /// removes a person from the account manager role within an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpDelete("{personId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAccountManager([FromRoute] Guid organizationId, [FromRoute]Guid personId)
        {
            if (personId == _agencyOwner.CustomerId)
            {
                ModelState.AddModelError("", "You cannot remove yourself");
                return new UnprocessableEntityObjectResult(ModelState);
            }
            var result = await _accountManagerService.Remove(_agencyOwner, personId);

            return Ok(result);
        }
    }
}
