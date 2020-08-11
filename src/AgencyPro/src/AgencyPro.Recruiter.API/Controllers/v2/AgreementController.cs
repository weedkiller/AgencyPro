// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Agreements.Services;
using AgencyPro.Core.Agreements.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Recruiter.API.Controllers.v2
{
    public class AgreementController : OrganizationUserController
    {
        private readonly IRecruitingAgreementService _agreementService;
        private readonly IOrganizationRecruiter _recruiter;

        public AgreementController(
            IRecruitingAgreementService agreementService,
            IOrganizationRecruiter recruiter,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _agreementService = agreementService;
            _recruiter = recruiter;
        }


        [HttpGet("active")]
        [ProducesResponseType(typeof(RecruiterRecruitingAgreementOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActiveAgreements([FromRoute] Guid organizationId)
        {
            var agreements = await _agreementService.GetAgreements<RecruiterRecruitingAgreementOutput>(_recruiter);

            return Ok(agreements);
        }
    }
}
