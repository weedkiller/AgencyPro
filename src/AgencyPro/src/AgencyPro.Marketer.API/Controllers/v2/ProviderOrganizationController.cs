// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.ProviderOrganizations.Services;
using AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Marketer.API.Controllers.v2
{
    public class ProviderOrganizationController : OrganizationUserController
    {
        private readonly IProviderOrganizationService _service;
        private readonly IOrganizationMarketer _marketer;

        public ProviderOrganizationController(
            IProviderOrganizationService service,
            IOrganizationMarketer marketer,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _service = service;
            _marketer = marketer;
        }

        /// <summary>
        /// gets a list of all provider organizations that the agency has agreements with 
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<MarketerProviderOrganizationOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProviderOrganizations([FromRoute] Guid organizationId)
        {
            var results = await _service.GetProviderOrganizations(_marketer);
            return Ok(results);
        }

    }
}