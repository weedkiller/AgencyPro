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

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class ProviderOrganizationController : OrganizationUserController
    {
        private readonly IProviderOrganizationService _service;
        private readonly Lazy<IMarketingAgencyOwner> _marketingAgencyOwner;
        private readonly Lazy<IRecruitingAgencyOwner> _recruitingAgencyOwner;

        public ProviderOrganizationController(
            IProviderOrganizationService service,
            Lazy<IMarketingAgencyOwner> marketingAgencyOwner,
            Lazy<IRecruitingAgencyOwner> recruitingAgencyOwner,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _service = service;
            _marketingAgencyOwner = marketingAgencyOwner;
            _recruitingAgencyOwner = recruitingAgencyOwner;
        }

        [HttpGet("marketing")]
        [ProducesResponseType(typeof(List<MarketingAgencyOwnerProviderOrganizationOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> MarketingAgencyDiscover([FromRoute] Guid organizationId)
        {
            var principal = _marketingAgencyOwner.Value;
            var pm = await _service.Discover<MarketingAgencyOwnerProviderOrganizationOutput>(principal);
            return Ok(pm);
        }

        [HttpGet("recruiting")]
        [ProducesResponseType(typeof(List<RecruitingAgencyOwnerProviderOrganizationOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> RecruitingAgencyDiscover([FromRoute] Guid organizationId)
        {
            var principal = _recruitingAgencyOwner.Value;
            var pm = await _service.Discover<RecruitingAgencyOwnerProviderOrganizationOutput>(principal);
            return Ok(pm);
        }
        
    }
}
