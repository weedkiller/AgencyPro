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
    public class RecruitingAgreementController : OrganizationUserController
    {
        private readonly IRecruitingAgencyOwner _recruitingAgencyOwner;
        private readonly IRecruitingAgreementService _agreementService;

        public RecruitingAgreementController(IServiceProvider serviceProvider,
            IRecruitingAgencyOwner recruitingAgencyOwner, IRecruitingAgreementService agreementService) : base(serviceProvider)
        {
            _recruitingAgencyOwner = recruitingAgencyOwner;
            _agreementService = agreementService;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(List<AgencyOwnerRecruitingAgreementOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProviderAgreements([FromRoute] Guid organizationId)
        {
            var results = await _agreementService.GetAgreements<AgencyOwnerRecruitingAgreementOutput>(_recruitingAgencyOwner);
            return Ok(results);
        }


        [HttpGet("{providerOrganizationId}")]
        [ProducesResponseType(typeof(List<AgencyOwnerRecruitingAgreementOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProviderAgreement([FromRoute] Guid organizationId, [FromRoute]Guid providerOrganizationId)
        {
            var results = await _agreementService
                .GetAgreement<AgencyOwnerRecruitingAgreementOutput>(_recruitingAgencyOwner, providerOrganizationId);
            return Ok(results);
        }


        /// <summary>
        /// creates agreement from the recruiting agency perspective
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="providerOrganizationId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{providerOrganizationId}")]
        public async Task<IActionResult> CreateProviderAgreement([FromRoute] Guid organizationId,
            [FromRoute]Guid providerOrganizationId,
            [FromBody]RecruitingAgreementInput input)
        {
            var result =
                await _agreementService.CreateAgreement(_recruitingAgencyOwner, providerOrganizationId, input);

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
                await _agreementService.AcceptAgreement(_recruitingAgencyOwner, providerOrganizationId);

            if (result.Succeeded)
                return await GetProviderAgreement(organizationId, providerOrganizationId);

            return Ok(result);
        }
    }
}