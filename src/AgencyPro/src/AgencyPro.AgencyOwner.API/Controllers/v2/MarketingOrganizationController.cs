// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.MarketingOrganizations.Services;
using AgencyPro.Core.Organizations.MarketingOrganizations.ViewModels;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class MarketingOrganizationController : OrganizationUserController
    {
        private readonly IProviderAgencyOwner _providerAgencyOwner;
        private readonly IMarketingOrganizationService _service;

        public MarketingOrganizationController(
            IProviderAgencyOwner providerAgencyOwner,
            IMarketingOrganizationService service,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _providerAgencyOwner = providerAgencyOwner;
            _service = service;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(List<ProviderAgencyOwnerMarketingOrganizationOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Discover([FromRoute] Guid organizationId)
        {
            var pm = await _service.Discover<ProviderAgencyOwnerMarketingOrganizationOutput>(_providerAgencyOwner);
            return Ok(pm);
        }

    }
}