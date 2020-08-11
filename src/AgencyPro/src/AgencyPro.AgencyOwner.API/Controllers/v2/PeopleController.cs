// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Common;
using AgencyPro.Core.OrganizationPeople;
using AgencyPro.Core.OrganizationPeople.Services;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class PeopleController : OrganizationUserController
    {
        private readonly IAgencyOwner _agencyOwner;
        private readonly IOrganizationPersonService _organizationPersonService;
        public PeopleController(IOrganizationPersonService organizationPersonService, 
            IAgencyOwner agencyOwner,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _agencyOwner = agencyOwner;
            _organizationPersonService = organizationPersonService;
        }

        /// <summary>
        ///     creates a new person with options to add to several roles
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(OrganizationPersonResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreatePerson([FromRoute] Guid organizationId,
            [FromBody] CreateOrganizationPersonInput input)
        {
            var person = await _organizationPersonService.Create(_agencyOwner, input);
            
            return Ok(person);
        }

        /// <summary>
        ///     adds an existing person to the organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(OrganizationPersonResult), StatusCodes.Status200OK)]
        
        public async Task<IActionResult> AddPerson([FromRoute] Guid organizationId, [FromBody] AddExistingPersonInput input )
        {
            var person =
                await _organizationPersonService.AddExistingPerson(_agencyOwner,
                    input);

            return Ok(person);
        }

        /// <summary>
        ///     removes a person from the organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpDelete("{personId}")]
        [ProducesResponseType(typeof(OrganizationPersonResult), StatusCodes.Status200OK)]
        public  async Task<IActionResult> RemovePerson([FromRoute] Guid organizationId, [FromRoute] Guid personId)
        {
            if (personId == _agencyOwner.CustomerId)
            {
                ModelState.AddModelError("", "You cannot remove yourself");
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var result = await _organizationPersonService.Remove(_agencyOwner, personId);
            return Ok(result);
        }

        /// <summary>
        ///     gets a person by id
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet("{personId}")]
        [ProducesResponseType(typeof(AgencyOwnerOrganizationPersonDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPerson([FromRoute] Guid organizationId, [FromRoute] Guid personId)
        {
            var result =
                await _organizationPersonService.GetOrganizationPerson<AgencyOwnerOrganizationPersonDetailsOutput>(personId,
                    organizationId);
            return Ok(result);
        }

        /// <summary>
        ///     gets all people in an organization
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<AgencyOwnerOrganizationPersonOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPeopleInOrganization([FromRoute] Guid organizationId, OrganizationPeopleFilters filters)
        {
            var result =
                await _organizationPersonService.GetPeople<AgencyOwnerOrganizationPersonOutput>(_agencyOwner, filters);
            AddPagination(filters, result.Total);
            return Ok(result.Data);
        }
        
    }
}