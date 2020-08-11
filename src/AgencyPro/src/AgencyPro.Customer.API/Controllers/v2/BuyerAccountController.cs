// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AgencyPro.Core.BuyerAccounts.Services;
using AgencyPro.Core.BuyerAccounts.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.AspNetCore.Http;

namespace AgencyPro.Customer.API.Controllers.v2
{
    public class BuyerAccountController : OrganizationUserController
    {
        private readonly IBuyerAccountService _buyerService;
        private readonly IOrganizationCustomer _customer;

        public BuyerAccountController(
            IServiceProvider serviceProvider,
            IBuyerAccountService buyerService,
            IOrganizationCustomer customer) : base(serviceProvider)
        {
            _buyerService = buyerService;
            _customer = customer;
        }

        /// <summary>
        /// gets the stripe url to login to express dashboard
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("StripeUrl")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStripeUrl([FromRoute] Guid organizationId)
        {
            return Ok(await _buyerService.GetStripeUrl(_customer));
        }

        /// <summary>
        /// Get buyer accounts for an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(BuyerAccountOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBuyerAccount([FromRoute] Guid organizationId)
        {
            var account = await _buyerService.GetBuyerAccount(_customer);
            return Ok(account);
        }
        

        /// <summary>
        ///     Get the stripe auth url for the current customer
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("AuthUrl")][Obsolete]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAuthUrl([FromRoute] Guid organizationId)
        {
            return Ok(await _buyerService.GetAuthUrl(_customer));
        }

        
    }
}