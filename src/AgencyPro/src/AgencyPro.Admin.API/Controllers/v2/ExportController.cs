// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;
using AgencyPro.Core.BuyerAccounts.Services;
using AgencyPro.Core.Plans.Services;
using AgencyPro.Core.Products.Services;
using AgencyPro.Core.Subscriptions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Admin.API.Controllers.v2
{
    [Route("export")]
    [ApiController]
    public class ExportController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IPlanService _planService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IBuyerAccountService _buyerAccountService;

        public ExportController(
            IProductService productService,
            IPlanService planService,
            ISubscriptionService subscriptionService,
            IBuyerAccountService buyerAccountService)
        {

            _productService = productService;
            _planService = planService;
            _subscriptionService = subscriptionService;
            _buyerAccountService = buyerAccountService;
        }

        [HttpGet("customers")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportCustomers()
        {
            var result = await _buyerAccountService.ExportCustomers();
            return Ok(result);
        }

        [HttpGet("subscriptions")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportSubscriptions()
        {
            var result = await _subscriptionService.ExportSubscriptions();
            return Ok(result);
        }

        [HttpGet("plans")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportPlans()
        {
            var result = await _planService.ExportPlans();
            return Ok(result);
        }

        [HttpGet("products")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportProducts()
        {
            var result = await _productService.ExportProducts();
            return Ok(result);
        }
    }
}