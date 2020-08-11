// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Common;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Roles.ViewModels.Contractors;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Recruiter.API.Controllers.v2
{
    public class ContractorController : OrganizationUserController
    {
        private readonly IContractorService _contractorService;
        private readonly IOrganizationRecruiter _recruiter;

        public ContractorController(
            IContractorService contractorService, 
            IOrganizationRecruiter recruiter,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _contractorService = contractorService;
            _recruiter = recruiter;
        }

        /// <summary>
        ///     Gets all the customers for the current marketer
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<RecruiterContractorOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContractors(
            [FromRoute] Guid organizationId, [FromQuery] CommonFilters filters
        )
        {
            var contractors = await _contractorService
                .GetContractors<RecruiterContractorOutput>(_recruiter, filters);
            AddPagination(filters, contractors.Total);
            return Ok(contractors.Data);
        }
    }
}