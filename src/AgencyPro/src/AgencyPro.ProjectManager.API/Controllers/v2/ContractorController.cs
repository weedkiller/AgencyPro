// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationPeople.Filters;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.ProjectManager.API.Controllers.v2
{
    public class ContractorController : OrganizationUserController
    {
        private readonly IOrganizationContractorService _contractorService;

        public ContractorController(IOrganizationContractorService contractorService,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _contractorService = contractorService;
        }

        /// <summary>
        ///     Get all contractors that belong to an organization
        /// </summary>
        /// <param name="organizationId">The Organization ID</param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<ProjectManagerOrganizationContractorOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContractors(
            [FromRoute] Guid organizationId, [FromQuery]ContractorFilters filters
        )
        {
            var co = await _contractorService
                .GetForOrganization<ProjectManagerOrganizationContractorOutput>(organizationId, filters);

            return Ok(co);
        }

    }
}