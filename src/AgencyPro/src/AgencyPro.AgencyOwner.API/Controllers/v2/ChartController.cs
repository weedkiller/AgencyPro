// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.TimeEntries.Services;
using AgencyPro.Core.TimeEntries.ViewModels;
using AgencyPro.Core.TimeEntries.ViewModels.TimeMatrix;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class ChartController : OrganizationUserController
    {
        private readonly ITimeMatrixService _matrixService;
        private readonly Lazy<IMarketingAgencyOwner> _marketingAgencyOwner;
        private readonly Lazy<IRecruitingAgencyOwner> _recruitingAgencyOwner;
        private readonly Lazy<IProviderAgencyOwner> _providerAgencyOwner;
        private readonly IAgencyOwner _agencyOwner;

        public ChartController(
            ITimeMatrixService matrixService, 
            Lazy<IMarketingAgencyOwner> marketingAgencyOwner,
            Lazy<IRecruitingAgencyOwner> recruitingAgencyOwner,
            Lazy<IProviderAgencyOwner> providerAgencyOwner,
            IAgencyOwner agencyOwner, 
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _matrixService = matrixService;
            _marketingAgencyOwner = marketingAgencyOwner;
            _recruitingAgencyOwner = recruitingAgencyOwner;
            _providerAgencyOwner = providerAgencyOwner;
            _agencyOwner = agencyOwner;
        }

        /// <summary>
        ///     Gets a searchable matrix of time segments
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("summary")]
        [ProducesResponseType(typeof(ProviderAgencyOwnerTimeMatrixComposedOutput), 200)]
        public async Task<IActionResult> Get([FromRoute] Guid organizationId, 
            [FromQuery] TimeMatrixFilters filters)
        {
            var result = await _matrixService
                .GetProviderAgencyComposedOutput(_providerAgencyOwner.Value, filters);

            return Ok(result);
        }

        [HttpGet("marketing-summary")]
        [ProducesResponseType(typeof(MarketingAgencyOwnerTimeMatrixComposedOutput), 200)]
        public async Task<IActionResult> GetMarketingSummary([FromRoute] Guid organizationId,
            [FromQuery] TimeMatrixFilters filters)
        {
            var result = await _matrixService
                .GetMarketingAgencyComposedOutput(_marketingAgencyOwner.Value, filters);

            return Ok(result);
        }

        [HttpGet("recruiting-summary")]
        [ProducesResponseType(typeof(RecruitingAgencyOwnerTimeMatrixComposedOutput), 200)]
        public async Task<IActionResult> GetRecruitingSummary([FromRoute] Guid organizationId,
            [FromQuery] TimeMatrixFilters filters)
        {
            var result = await _matrixService
                .GetRecruitingAgencyComposedOutput(_recruitingAgencyOwner.Value, filters);

            return Ok(result);
        }
    }
}
