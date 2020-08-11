// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Agreements.Services;
using AgencyPro.Core.Agreements.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class ProviderAgreementController : OrganizationUserController
    {
        private readonly IMarketingAgreementService _marketingService;
        private readonly IRecruitingAgreementService _recruitingService;
        private readonly IProviderAgencyOwner _providerAgencyOwner;

        public ProviderAgreementController(
            IMarketingAgreementService marketingService,
            IRecruitingAgreementService recruitingService,
            IProviderAgencyOwner providerAgencyOwner,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _marketingService = marketingService;
            _recruitingService = recruitingService;
            _providerAgencyOwner = providerAgencyOwner;
        }

        /// <summary>
        /// Get marketing agreements from provider agency owner's perspective
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("marketing")]
        [ProducesResponseType(typeof(List<AgencyOwnerMarketingAgreementOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMarketingAgreements([FromRoute] Guid organizationId)
        {
            var agreements =
                await _marketingService.GetAgreements<AgencyOwnerMarketingAgreementOutput>(_providerAgencyOwner);
            return Ok(agreements);
        }

        /// <summary>
        /// Get marketing agreement from provider agency owner's perspective
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="marketingOrganizationId"></param>
        /// <returns></returns>
        [HttpGet("marketing/{marketingOrganizationId}")]
        [ProducesResponseType(typeof(AgencyOwnerMarketingAgreementOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMarketingAgreement([FromRoute] Guid organizationId, [FromRoute]Guid marketingOrganizationId)
        {
            var agreements =
                await _marketingService.GetAgreement<AgencyOwnerMarketingAgreementOutput>(_providerAgencyOwner, marketingOrganizationId);
            return Ok(agreements);
        }

        /// <summary>
        /// Creates marketing agreement with marketing agency from provider agency owner's perspective
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="marketingOrganizationId"></param>
        /// <returns></returns>
        [HttpPost("marketing/{marketingOrganizationId}")]
        public async Task<IActionResult> CreateDefaultMarketingAgreement([FromRoute] Guid organizationId,
            [FromRoute]Guid marketingOrganizationId)
        {

            await _marketingService.CreateAgreement(_providerAgencyOwner, marketingOrganizationId);

            return await GetMarketingAgreement(organizationId, marketingOrganizationId);
        }

        [HttpPost("recruiting/{recruitingOrganizationId}")]
        public async Task<IActionResult> CreateDefaultRecruitingAgreement([FromRoute] Guid organizationId,
            [FromRoute]Guid recruitingOrganizationId)
        {
            await _recruitingService.CreateAgreement(_providerAgencyOwner, recruitingOrganizationId);

            return await GetRecruitingAgreement(organizationId, recruitingOrganizationId);
        }

        /// <summary>
        /// Gets recruiting agreements with recruiting agency from a provider agency owner's perspective
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("recruiting")]
        [ProducesResponseType(typeof(List<AgencyOwnerRecruitingAgreementOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRecruitingAgreements([FromRoute] Guid organizationId)
        {
            var agreements =
                await _recruitingService.GetAgreements<AgencyOwnerRecruitingAgreementOutput>(_providerAgencyOwner);
            return Ok(agreements);
        }

        /// <summary>
        /// Gets the recruiting agreement from provider agency owner's perspective
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="recruitingOrganizationId"></param>
        /// <returns></returns>
        [HttpGet("recruiting/{recruitingOrganizationId}")]
        [ProducesResponseType(typeof(AgencyOwnerRecruitingAgreementOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRecruitingAgreement([FromRoute] Guid organizationId, [FromRoute]Guid recruitingOrganizationId)
        {
            var agreement =
                await _recruitingService.GetAgreement<AgencyOwnerRecruitingAgreementOutput>(_providerAgencyOwner, recruitingOrganizationId);
            return Ok(agreement);
        }
    }
}
