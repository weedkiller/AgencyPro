// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels;
using AgencyPro.Core.Organizations.Services;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.AccountManager.API.Controllers.v2
{
    public class OrganizationController : OrganizationUserController
    {
        private readonly IOrganizationAccountManager _accountManager;
        private readonly IOrganizationAccountManagerService _accountManagerService;
        private readonly IOrganizationService _organizationService;

        public OrganizationController(
            IServiceProvider serviceProvider,
            IOrganizationAccountManager accountManager,
            IOrganizationAccountManagerService accountManagerService,
            IOrganizationService organizationService) : base(serviceProvider)
        {
            _accountManager = accountManager;
            _accountManagerService = accountManagerService;
            _organizationService = organizationService;
        }

        /// <summary>
        /// Get organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(AccountManagerOrganizationOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(
            [FromRoute] Guid organizationId
        )
        {
            var org = await _accountManagerService
                .GetOrganization<AccountManagerOrganizationOutput>
                    (_accountManager);

            return Ok(org);
        }

        /// <summary>
        /// get counts of objects for organization account manager
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("counts")]
        [ProducesResponseType(typeof(AccountManagerCounts), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCounts([FromRoute] Guid organizationId)
        {
            var counts = await _accountManagerService.GetCounts(_accountManager);

            return Ok(counts);
        }
    }
}