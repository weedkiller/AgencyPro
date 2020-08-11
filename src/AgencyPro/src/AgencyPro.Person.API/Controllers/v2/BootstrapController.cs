// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Lookup.Services;
using AgencyPro.Core.OrganizationPeople.Services;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.Organizations.Services;
using AgencyPro.Core.People.Services;
using AgencyPro.Core.UserAccount.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Person.API.Controllers.v2
{
   


    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("bootstrap")]
    [Produces("application/json")]
    public class BootstrapController : ControllerBase
    {
        private readonly ILookupService _lookupService;
        private readonly IOrganizationPersonService _organizationPersonService;
        private readonly IOrganizationService _organizationService;
        private readonly IPersonService _personService;
        private readonly IUserInfo _user;

        public BootstrapController(
            IOrganizationService organizationService,
            IOrganizationPersonService organizationPersonService,
            ILookupService lookupService,
            IPersonService personService,
            IUserInfo user)
        {
            _lookupService = lookupService;
            _personService = personService;
            _user = user;
            _organizationService = organizationService;
            _organizationPersonService = organizationPersonService;
        }

        [HttpGet("person")]
        public async Task<IActionResult> GetPerson()
        {
            var output = await _personService.Get(_user.UserId);
            return Ok(output);
        }

        [HttpGet("lookup")]
        public ActionResult GetLookup()
        {
            var output = _lookupService.GetAll();
            return Ok(output);
        }

        [HttpGet("affiliation")]
        [ProducesResponseType(typeof(List<AffiliationOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAffiliations()
        {
            var affiliations = await _organizationService.GetAffiliationsForPerson(_user.UserId);

            return Ok(affiliations);
        }

        /// <summary>
        /// Show an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpPatch("affiliation/{organizationId}/show")]
        [ProducesResponseType(typeof(OrganizationPersonResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> ShowAffiliation([FromRoute]Guid organizationId)
        {
            var person = await _organizationPersonService.Get(_user.UserId, organizationId);

            var result = await _organizationPersonService.ShowOrganization(person);

            return Ok(result);
        }

        /// <summary>
        /// Hides an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpPatch("affiliation/{organizationId}/hide")]
        [ProducesResponseType(typeof(OrganizationPersonResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> HideAffiliation([FromRoute]Guid organizationId)
        {
            var person = await _organizationPersonService.Get(_user.UserId, organizationId);

            var result = await _organizationPersonService.HideOrganization(person);

            return Ok(result);
        }
    }
}