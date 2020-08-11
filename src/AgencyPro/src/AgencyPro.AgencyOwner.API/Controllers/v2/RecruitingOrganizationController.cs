// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.RecruitingOrganizations.Services;
using AgencyPro.Core.Organizations.RecruitingOrganizations.ViewModels;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class RecruitingOrganizationController : OrganizationUserController
    {
        private readonly IProviderAgencyOwner _providerAgencyOwner;
        private readonly IRecruitingOrganizationService _service;

        public RecruitingOrganizationController(IServiceProvider serviceProvider, 
            IRecruitingOrganizationService service, 
            IProviderAgencyOwner providerAgencyOwner) : base(serviceProvider)
        {
            _service = service;
            _providerAgencyOwner = providerAgencyOwner;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(List<ProviderAgencyOwnerRecruitingOrganizationOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Discover([FromRoute] Guid organizationId)
        {
            var pm = await _service.Discover<ProviderAgencyOwnerRecruitingOrganizationOutput>(_providerAgencyOwner);
            return Ok(pm);
        }

    }
}