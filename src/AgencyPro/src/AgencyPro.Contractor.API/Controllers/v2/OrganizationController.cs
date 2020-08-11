// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Contractor.API.Controllers.v2
{
    public class OrganizationController : OrganizationUserController
    {
        private readonly IOrganizationContractorService _contractorService;
        private readonly IOrganizationContractor _contractor;

        public OrganizationController(IOrganizationContractor contractor,
            IOrganizationContractorService contractorService,
            IServiceProvider provider) : base(provider)
        {
            _contractor = contractor;
            _contractorService = contractorService;
        }

        /// <summary>
        /// get org info from co perspective
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ContractorOrganizationOutput), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetOrganizationInfo(
            [FromRoute] Guid organizationId
        )
        {
            var org = await _contractorService.GetOrganization<ContractorOrganizationOutput>(_contractor);
            return Ok(org);
        }

        /// <summary>
        /// get counts of objects for organization account manager
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("counts")]
        [ProducesResponseType(typeof(ContractorCounts), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCounts([FromRoute] Guid organizationId)
        {
            var counts = await _contractorService.GetCounts(_contractor);

            return Ok(counts);
        }
    }
}