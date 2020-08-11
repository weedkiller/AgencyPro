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

namespace AgencyPro.Marketer.API.Controllers.v2
{
    public class AgreementController : OrganizationUserController
    {
        private readonly IMarketingAgreementService _agreementService;
        private readonly IOrganizationMarketer _marketer;

        public AgreementController(
            IMarketingAgreementService agreementService,
            IOrganizationMarketer marketer,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _agreementService = agreementService;
            _marketer = marketer;
        }


        [HttpGet("active")]
        [ProducesResponseType(typeof(MarketerMarketingAgreementOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActiveAgreements([FromRoute] Guid organizationId)
        {
            var agreements = await _agreementService.GetAgreements<MarketerMarketingAgreementOutput>(_marketer);

            return Ok(agreements);
        }
    }
}
