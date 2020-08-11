// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Common;
using AgencyPro.Core.OrganizationPeople.Filters;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationRecruiters;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.AccountManager.API.Controllers.v2
{
    public class RecruiterController : OrganizationUserController
    {
        private readonly IOrganizationAccountManager _accountManager;
        private readonly IOrganizationRecruiterService _recruiterService;

        public RecruiterController(
            IServiceProvider serviceProvider,
            IOrganizationAccountManager accountManager,
            IOrganizationRecruiterService recruiterService
            ) : base(serviceProvider)
        {
            _accountManager = accountManager;
            _recruiterService = recruiterService;
        }

        /// <summary>
        /// Gets all the recruiters in an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ProducesResponseType(typeof(List<AccountManagerOrganizationRecruiterOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRecruiterList([FromRoute] Guid organizationId, [FromQuery]RecruiterFilters filters)
        {
            var result =
                await _recruiterService
                    .GetForOrganization<AccountManagerOrganizationRecruiterOutput>(_accountManager
                    .OrganizationId, filters);

            return Ok(result);
        }

        /// <summary>
        /// Gets recruiters for organization with statistical data
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <param name="pagingFilters"></param>
        /// <returns></returns>
        [HttpGet("statistics")]
        [ProducesResponseType(typeof(List<OrganizationRecruiterStatistics>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRecruiterStatistics([FromRoute] Guid organizationId, 
            [FromQuery]RecruiterFilters filters, [FromQuery]CommonFilters pagingFilters)
        {
            var result = await _recruiterService
                    .GetForOrganization<OrganizationRecruiterStatistics>(_accountManager.OrganizationId, filters, pagingFilters);
            AddPagination(pagingFilters, result.Total);
            return Ok(result.Data);
        }

        /// <summary>
        ///     get a specific recruiter in organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="recruiterId"></param>
        /// <returns></returns>
        [HttpGet("{recruiterId}")]
        [ProducesResponseType(typeof(OrganizationRecruiterStatistics), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRecruiter([FromRoute] Guid organizationId, [FromRoute]Guid recruiterId)
        {
            var result =
                await _recruiterService
                    .GetById<OrganizationRecruiterStatistics>(recruiterId, _accountManager
                        .OrganizationId);

            return Ok(result);
        }
        
        
    }
}