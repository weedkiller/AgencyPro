// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.BillingCategories.Services;
using AgencyPro.Core.BillingCategories.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Person.API.Controllers.v2
{
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("BillingCategory")]
    [Produces("application/json")]
    public class BillingCategoryController : ControllerBase
    {
        private readonly IBillingCategoryService _billingCategoryService;

        public BillingCategoryController(
            IBillingCategoryService billingCategoryService) 
        {
            _billingCategoryService = billingCategoryService;
        }

        /// <summary>
        /// Get all the billing categories for a category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<BillingCategoryOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBillingCategories(
            [FromQuery] int categoryId)
        {
            var categories = await _billingCategoryService
                .GetBillingCategoriesByCategory(categoryId);

            return Ok(categories);
        }
    }
}
