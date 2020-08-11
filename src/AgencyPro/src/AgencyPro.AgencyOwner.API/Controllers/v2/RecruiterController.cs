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

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class RecruiterController : OrganizationUserController
    {
        private readonly IAgencyOwner _agencyOwner;
        private readonly IOrganizationRecruiterService _recruiterService;

        public RecruiterController(
            IServiceProvider serviceProvider,
            IAgencyOwner agencyOwner,
            IOrganizationRecruiterService recruiterService
            ) : base(serviceProvider)
        {
            _agencyOwner = agencyOwner;
            _recruiterService = recruiterService;
        }

        /// <summary>
        /// removes a person from the recruiter role within an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpDelete("{personId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteRecruiter([FromRoute] Guid organizationId, [FromRoute]Guid personId)
        {
            if (personId == _agencyOwner.CustomerId)
            {
                ModelState.AddModelError("", "You cannot remove yourself");
                return new UnprocessableEntityObjectResult(ModelState);
            }
            var result = await _recruiterService
                .Remove(_agencyOwner, personId);

            return Ok(result);
        }

        /// <summary>
        /// Gets all the recruiters in an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ProducesResponseType(typeof(List<AgencyOwnerOrganizationRecruiterOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRecruiterList([FromRoute] Guid organizationId, [FromQuery]RecruiterFilters filters)
        {
            var result =
                await _recruiterService
                    .GetForOrganization<AgencyOwnerOrganizationRecruiterOutput>(_agencyOwner
                    .OrganizationId,filters);

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
            [FromQuery]RecruiterFilters filters, [FromQuery] CommonFilters pagingFilters)
        {
            var result = await _recruiterService
                .GetForOrganization<OrganizationRecruiterStatistics>(_agencyOwner.OrganizationId, filters, pagingFilters);
            AddPagination(pagingFilters, result.Total);
            return Ok(result.Data);
        }

        /// <summary>
        /// get a specific recruiter in organization
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
                    .GetById<OrganizationRecruiterStatistics>(recruiterId, _agencyOwner
                        .OrganizationId);

            return Ok(result);
        }

        /// <summary>
        ///     Update recruiter within an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="personId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPatch("{personId}")]
        [ProducesResponseType(typeof(AgencyOwnerOrganizationRecruiterOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateRecruiter([FromRoute] Guid organizationId, [FromRoute] Guid personId,
            [FromBody] OrganizationRecruiterInput input)
        {
            var result = await _recruiterService
                .Update<AgencyOwnerOrganizationRecruiterOutput>(_agencyOwner, input);

            return Ok(result);
        }

        /// <summary>
        ///     adds an existing recruiter to an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="personId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{personId}")]
        
        [ProducesResponseType(typeof(AgencyOwnerOrganizationRecruiterOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddRecruiter([FromRoute] Guid organizationId, [FromRoute] Guid personId,
            [FromBody] OrganizationRecruiterInput input)
        {
            var result = await _recruiterService
                .Create<AgencyOwnerOrganizationRecruiterOutput>(_agencyOwner, input);

            return Ok(result);
        }
    }
}