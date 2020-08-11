// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.BillingCategories.Services;
using AgencyPro.Core.BillingCategories.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class BillingCategoryController : OrganizationUserController
    {
        private readonly IProviderAgencyOwner _agencyOwner;
        private readonly IBillingCategoryService _billingCategoryService;

        public BillingCategoryController(
            IProviderAgencyOwner agencyOwner,
            IBillingCategoryService billingCategoryService,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _agencyOwner = agencyOwner;
            _billingCategoryService = billingCategoryService;
        }


        /// <summary>
        /// Get billing categories
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<BillingCategoryOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBillingCategories(
            [FromRoute] Guid organizationId,
            [FromQuery] Guid? projectId)
        {
            List<BillingCategoryOutput> categories;
            if (projectId.HasValue)
            {
                categories = await _billingCategoryService
                    .GetBillingCategoriesByProject(organizationId, projectId.Value);
            }
            else
            {
                categories = await _billingCategoryService
                    .GetBillingCategoriesByOrganization(organizationId);
            }
            

            return Ok(categories);
        }

        /// <summary>
        /// Adds a billing category to an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="billingCategoryId"></param>
        /// <returns></returns>
        [HttpPut("{billingCategoryId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddBillingCategory(
            [FromRoute]Guid organizationId, [FromRoute]int billingCategoryId)
        {

            var org =
                await _billingCategoryService.AddBillingCategory
                (_agencyOwner,
                    billingCategoryId);

            return Ok(org);
        }

        /// <summary>
        /// Removes a billing category from an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="billingCategoryId"></param>
        /// <returns></returns>
        [HttpDelete("{billingCategoryId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveBillingCategory(
            [FromRoute]Guid organizationId, [FromRoute]int billingCategoryId)
        {
            var org =
                await _billingCategoryService
                    .RemoveBillingCategory
                        (_agencyOwner, billingCategoryId);

            return Ok(org);
        }

    }
}
