// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Stripe.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AgencyPro.Core.BuyerAccounts.Services;
using AgencyPro.Core.Invoices.Services;
using AgencyPro.Core.Subscriptions.Services;

namespace AgencyPro.Admin.API.Controllers.v2
{
    [Route("import")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IBuyerAccountService _buyerAccountService;

        public ImportController(
            ISubscriptionService subscriptionService, 
            IBuyerAccountService buyerAccountService)
        {
            _subscriptionService = subscriptionService;
            _buyerAccountService = buyerAccountService;
        }

        [HttpGet("customers")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> ImportCustomers([FromQuery]int limit = 20)
        {
            var result = await _buyerAccountService.ImportBuyerAccounts(limit);
            return Ok(result);
        }

        [HttpGet("subscriptions")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> ImportSubscriptions([FromQuery]int limit = 20)
        {
            var result = await _subscriptionService.ImportSubscriptions(limit);
            return Ok(result);
        }
        

    }
}