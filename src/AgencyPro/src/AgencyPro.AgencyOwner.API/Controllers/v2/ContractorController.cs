// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Common;
using AgencyPro.Core.OrganizationPeople.Filters;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class ContractorController : OrganizationUserController
    {
        private readonly IOrganizationContractorService _contractorService;
        private readonly IAgencyOwner _agencyOwner;

        public ContractorController(
            IOrganizationContractorService contractorService,
            IAgencyOwner agencyOwner,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _contractorService = contractorService;
            _agencyOwner = agencyOwner;
        }

        /// <summary>
        /// removes a person from the contractor role within an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpDelete("{personId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteContractor([FromRoute] Guid organizationId, [FromRoute]Guid personId)
        {
            if (personId == _agencyOwner.CustomerId)
            {
                ModelState.AddModelError("", "You cannot remove yourself");
                return new UnprocessableEntityObjectResult(ModelState);
            }
            var result = await _contractorService.Remove(_agencyOwner, personId);
            return Ok(result);
        }

        /// <summary>
        /// gets contractors within an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ProducesResponseType(typeof(List<AgencyOwnerOrganizationContractorOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContractors([FromRoute] Guid organizationId, [FromQuery]ContractorFilters filters)
        {
            var result =
                await _contractorService.GetForOrganization<AgencyOwnerOrganizationContractorOutput>(_agencyOwner
                    .OrganizationId, filters);

            return Ok(result);
        }

        /// <summary>
        /// gets contractors within an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <param name="pagingFilters"></param>
        /// <returns></returns>
        [HttpGet("statistics")]
        [ProducesResponseType(typeof(List<OrganizationContractorStatistics>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContractorStatistics([FromRoute] Guid organizationId, 
            [FromQuery]ContractorFilters filters, [FromQuery] CommonFilters pagingFilters)
        {
            var result = await _contractorService
                .GetForOrganization<OrganizationContractorStatistics>(_agencyOwner.OrganizationId, filters, pagingFilters);
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
        [ProducesResponseType(typeof(AgencyOwnerOrganizationContractorDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContractor([FromRoute] Guid organizationId, [FromRoute]Guid personId)
        {
            var result =
                await _contractorService
                    .GetById<AgencyOwnerOrganizationContractorDetailsOutput>(personId, _agencyOwner
                        .OrganizationId);

            return Ok(result);
        }

        /// <summary>
        /// update a contractor within an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="personId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPatch("{personId}")]
        
        [ProducesResponseType(typeof(AgencyOwnerOrganizationContractorOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateContractor([FromRoute] Guid organizationId, [FromRoute] Guid personId,
            [FromBody] OrganizationContractorInput input)
        {
            var result = await _contractorService
                .Update<AgencyOwnerOrganizationContractorOutput>(_agencyOwner, input);
            return Ok(result);
        }

        /// <summary>
        /// update a contractor within an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="personId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPatch("{personId}/recruiter")]
        
        [ProducesResponseType(typeof(AgencyOwnerOrganizationContractorOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateContractorRecruiter([FromRoute] Guid organizationId, [FromRoute] Guid personId,
            [FromBody] UpdateContractorRecruiterInput input)
        {
            var result = await _contractorService
                .UpdateRecruiter<AgencyOwnerOrganizationContractorOutput>(_agencyOwner, personId, input);
            return Ok(result);
        }

        /// <summary>
        /// adds an existing contractor to an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="personId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{personId}")]
        
        [ProducesResponseType(typeof(List<AgencyOwnerOrganizationContractorOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddContractor([FromRoute] Guid organizationId, [FromRoute] Guid personId,
            [FromBody] OrganizationContractorInput input)
        {
            var result = await _contractorService
                .Create<AgencyOwnerOrganizationContractorOutput>(_agencyOwner, input);
            return Ok(result);
        }
    }
}
