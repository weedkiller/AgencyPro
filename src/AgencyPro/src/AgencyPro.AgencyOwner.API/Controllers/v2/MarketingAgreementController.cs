// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Agreements.Services;
using AgencyPro.Core.Agreements.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class MarketingAgreementController : OrganizationUserController
    {
        private readonly IMarketingAgreementService _agreementService;
        private readonly IMarketingAgencyOwner _marketingAgencyOwner;

        public MarketingAgreementController(
            IServiceProvider serviceProvider, 
           IMarketingAgencyOwner marketingAgencyOwner,
            IMarketingAgreementService agreementService) : base(serviceProvider)
        {
            _marketingAgencyOwner = marketingAgencyOwner;
            _agreementService = agreementService;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(List<AgencyOwnerMarketingAgreementOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProviderAgreements([FromRoute] Guid organizationId)
        {
            var result = await _agreementService
                .GetAgreements<AgencyOwnerMarketingAgreementOutput>(_marketingAgencyOwner);
            return Ok(result);
        }

        [HttpGet("{providerOrganizationId}")]
        [ProducesResponseType(typeof(AgencyOwnerMarketingAgreementOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProviderAgreement([FromRoute] Guid organizationId, [FromRoute]Guid providerOrganizationId)
        {
            var result = await _agreementService
                .GetAgreement<AgencyOwnerMarketingAgreementOutput>(_marketingAgencyOwner, providerOrganizationId);
            return Ok(result);
        }


        /// <summary>
        /// creates agreement from the marketing agency perspective
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="providerOrganizationId"></param>
        /// <returns></returns>
        [HttpPost("{providerOrganizationId}")]
        public async Task<IActionResult> CreateProviderAgreement([FromRoute] Guid organizationId,
            [FromRoute]Guid providerOrganizationId)
        {
            var result =
                await _agreementService.CreateAgreement(_marketingAgencyOwner, providerOrganizationId);

            return Ok(result);
        }

        /// <summary>
        /// Accept agreement 
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="providerOrganizationId"></param>
        /// <returns></returns>
        [HttpPatch("{providerOrganizationId}/accept")]
        public async Task<IActionResult> AcceptProviderAgreement([FromRoute] Guid organizationId,
            [FromRoute]Guid providerOrganizationId)
        {
            var result =
                await _agreementService.AcceptAgreement(_marketingAgencyOwner, providerOrganizationId);

            if (result.Succeeded)
                return await GetProviderAgreement(organizationId, providerOrganizationId);

            return Ok(result);
        }

    }
}
