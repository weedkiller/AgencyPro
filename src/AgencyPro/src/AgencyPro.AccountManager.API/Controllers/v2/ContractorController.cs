// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationPeople.Filters;
using AgencyPro.Core.Common;

namespace AgencyPro.AccountManager.API.Controllers.v2
{
    public class ContractorController : OrganizationUserController
    {
        private readonly IOrganizationContractorService _contractorService;
        private readonly IOrganizationAccountManager _accountManager;

        public ContractorController(
            IOrganizationContractorService contractorService,
            IOrganizationAccountManager accountManager,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _contractorService = contractorService;
            _accountManager = accountManager;
        }


        /// <summary>
        /// gets contractors within an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ProducesResponseType(typeof(List<AccountManagerOrganizationContractorOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContractorList([FromRoute] Guid organizationId, [FromQuery]ContractorFilters filters)
        {
            var result =
                await _contractorService.GetForOrganization<AccountManagerOrganizationContractorOutput>(_accountManager
                    .OrganizationId, filters);

            return Ok(result);
        }

        /// <summary>
        /// gets contractors (w/ statistics) within an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <param name="pagingFilters"></param>
        /// <returns></returns>
        [HttpGet("statistics")]
        [ProducesResponseType(typeof(List<OrganizationContractorStatistics>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContractorStatistics([FromRoute] Guid organizationId, 
            [FromRoute]ContractorFilters filters, [FromQuery]CommonFilters pagingFilters)
        {
            var result = await _contractorService
                .GetForOrganization<OrganizationContractorStatistics>(_accountManager.OrganizationId, filters, pagingFilters);
            AddPagination(pagingFilters, result.Total);
            return Ok(result.Data);
        }

        /// <summary>
        /// Gets contractor record within an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet("{personId}")]
        [ProducesResponseType(typeof(AccountManagerOrganizationContractorDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContractor([FromRoute] Guid organizationId, [FromRoute]Guid personId)
        {
            var result =
                await _contractorService
                    .GetById<AccountManagerOrganizationContractorDetailsOutput>(personId, _accountManager
                        .OrganizationId);

            return Ok(result);
        }

        /// <summary>
        /// Get statistics for a person
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet("statistics/{personId}")]
        [ProducesResponseType(typeof(OrganizationContractorStatistics), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContractorStatistics([FromRoute] Guid organizationId, [FromRoute]Guid personId)
        {
            var result =
                await _contractorService
                    .GetById<OrganizationContractorStatistics>(personId, _accountManager
                        .OrganizationId);

            return Ok(result);
        }

    }
}
