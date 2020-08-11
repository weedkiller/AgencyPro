// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.BillingCategories.Services;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.MarketingOrganizations.ViewModels;
using AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels;
using AgencyPro.Core.Organizations.RecruitingOrganizations.ViewModels;
using AgencyPro.Core.Organizations.Services;
using AgencyPro.Core.Organizations.ViewModels;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public partial class OrganizationController : OrganizationUserController
    {
        private readonly Lazy<IProviderAgencyOwner> _providerAgencyOwner;
        private readonly Lazy<IMarketingAgencyOwner> _marketingAgencyOwner;
        private readonly Lazy<IRecruitingAgencyOwner> _recruitingAgencyOwner;
        private readonly IOrganizationService _organizationService;
        private readonly IBillingCategoryService _billingCategoryService;
        private readonly IAgencyOwner _agencyOwner;
        private readonly ILogger<OrganizationController> _logger;

        public OrganizationController(
            IOrganizationService organizationService,
            IBillingCategoryService billingCategoryService,
            IAgencyOwner agencyOwner,
            ILogger<OrganizationController> logger,
            Lazy<IProviderAgencyOwner> providerAgencyOwner,
            Lazy<IMarketingAgencyOwner> marketingAgencyOwner,
            Lazy<IRecruitingAgencyOwner> recruitingAgencyOwner,
            IServiceProvider provider) : base(provider)
        {
            _organizationService = organizationService;
            _billingCategoryService = billingCategoryService;
            _agencyOwner = agencyOwner;
            _logger = logger;
            _providerAgencyOwner = providerAgencyOwner;
            _marketingAgencyOwner = marketingAgencyOwner;
            _recruitingAgencyOwner = recruitingAgencyOwner;
        }

        [HttpGet("counts")]
        [ProducesResponseType(typeof(AgencyOwnerCounts), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCounts([FromRoute] Guid organizationId)
        {


            var counts = await _organizationService.GetCounts(_agencyOwner);

            return Ok(counts);
        }

        /// <summary>
        ///     Gets the organization information from the perspective of currently logged-in account manager
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(AgencyOwnerOrganizationDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] Guid organizationId)
        {
            
            var a = await  _organizationService.GetOrganization<AgencyOwnerOrganizationDetailsOutput>(_agencyOwner);

            if (_agencyOwner.IsMarketingOwner)
            {
                var ma = _marketingAgencyOwner.Value;
                a.MarketingOrganizationDetails = await _organizationService.GetMarketingDetails(ma);
            }

            if (_agencyOwner.IsRecruitingOwner)
            {
                var re = _recruitingAgencyOwner.Value;
                a.RecruitingOrganizationDetails = await _organizationService.GetRecruitingDetails(re);
            }

            if (_agencyOwner.IsProviderOwner)
            {
                var pr = _providerAgencyOwner.Value;
                a.ProviderOrganizationDetails = await _organizationService.GetProviderDetails(pr);
            }

            return Ok(a);
        }

        /// <summary>
        /// Adds a billing category to an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="billingCategoryId"></param>
        /// <returns></returns>
        [HttpPut("BillingCategory/{billingCategoryId}")]
        [ProducesResponseType(typeof(AgencyOwnerProviderOrganizationDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddBillingCategory(
            [FromRoute]Guid organizationId, [FromRoute]int billingCategoryId)
        {
            var org =
                await _billingCategoryService.AddBillingCategory
                (_providerAgencyOwner.Value,
                    billingCategoryId);

            return await Get(organizationId);
        }

        /// <summary>
        /// Removes a billing category from an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="billingCategoryId"></param>
        /// <returns></returns>
        [HttpDelete("BillingCategory/{billingCategoryId}")]
        [ProducesResponseType(typeof(AgencyOwnerProviderOrganizationDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveBillingCategory(
            [FromRoute]Guid organizationId, [FromRoute]int billingCategoryId)
        {
            var org =
                await _billingCategoryService
                    .RemoveBillingCategory
                        (_providerAgencyOwner.Value, billingCategoryId);

            return await Get(organizationId);
        }

        /// <summary>
        /// Update organization settings
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPatch("")]
        [ProducesResponseType(typeof(AgencyOwnerOrganizationDetailsOutput), StatusCodes.Status200OK)]
        
        public async Task<IActionResult> Update([FromRoute] Guid organizationId,
            [FromBody] OrganizationUpdateInput input)
        {
            var result = await _organizationService
                .UpdateOrganization(_agencyOwner, input);

            if (result.Succeeded)
            {
                return await Get(organizationId);
            }

            return Ok(result);
        }

        /// <summary>
        /// update provider settings
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPatch("provider")]
        [ProducesResponseType(typeof(AgencyOwnerOrganizationDetailsOutput), StatusCodes.Status200OK)]
        
        public async Task<IActionResult> Update([FromRoute] Guid organizationId,
            [FromBody] ProviderOrganizationInput input)
        {
            var result = await _organizationService
                .UpdateOrganization(_providerAgencyOwner.Value, input);

            if (result.Succeeded)
            {
                return await Get(organizationId);
            }

            return Ok(result);
        }

        /// <summary>
        /// update recruiting settings
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPatch("recruiting")]
        [ProducesResponseType(typeof(AgencyOwnerOrganizationDetailsOutput), StatusCodes.Status200OK)]
        
        public async Task<IActionResult> Update([FromRoute] Guid organizationId,
            [FromBody] RecruitingOrganizationInput input)
        {
            var result = await _organizationService
                .UpdateOrganization(_recruitingAgencyOwner.Value, input);

            if (result.Succeeded)
            {
                return await Get(organizationId);
            }

            return Ok(result);
        }

        /// <summary>
        /// update marketing settings
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPatch("marketing")]
        [ProducesResponseType(typeof(AgencyOwnerOrganizationDetailsOutput), StatusCodes.Status200OK)]
        
        public async Task<IActionResult> Update([FromRoute] Guid organizationId,
            [FromBody] MarketingOrganizationInput input)
        {
            var result = await _organizationService
                .UpdateOrganization(_marketingAgencyOwner.Value, input);

            if (result.Succeeded)
            {
                return await Get(organizationId);
            }

            return Ok(result);
        }


        /// <summary>
        ///     Updates an organization logo (200x200px is best)
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="logo">The logo to upload</param>
        /// <returns></returns>
        [HttpPatch("logo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateOrganizationPic([FromRoute] Guid organizationId,
            [FromForm] IFormFile logo)
        {
            var result = await _organizationService.UpdateOrganizationPic(_agencyOwner, logo);

            if (result.Succeeded)
            {
                return Accepted();
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return new UnprocessableEntityObjectResult(ModelState);
        }

    }
}