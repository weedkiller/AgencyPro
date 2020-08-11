// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AgencyPro.Core.FinancialAccounts.Services;
using AgencyPro.Core.FinancialAccounts.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Stripe.Services;
using Microsoft.AspNetCore.Http;
using Stripe;

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class FinancialAccountController : OrganizationUserController
    {
        private readonly IStripeService _stripeService;
        private readonly IAgencyOwner _agencyOwner;
        private readonly IFinancialAccountService _financialAccountService;

        public FinancialAccountController(IServiceProvider serviceProvider,
                        IStripeService stripeService,
            IAgencyOwner agencyOwner, IFinancialAccountService financialAccount) : base(serviceProvider)
        {
            _stripeService = stripeService;
            _agencyOwner = agencyOwner;
            _financialAccountService = financialAccount;
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
            return Ok(await _stripeService.GetStripeUrl(_agencyOwner));
        }

        /// <summary>
        /// gets the stripe auth url to connect express account
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("AuthUrl")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAuthUrl([FromRoute] Guid organizationId)
        {
            return Ok(await _stripeService.GetAuthUrl(_agencyOwner));
        }

        /// <summary>
        /// gets the financial account of the current user if there is one
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(FinancialAccountDetails), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFinancialAccount([FromRoute] Guid organizationId)
        {
            var account = await _financialAccountService
                .GetFinancialAccount(_agencyOwner);

            if (account == null)
                return NoContent();

            var service = new BalanceService();
            var balance = service.Get(new RequestOptions()
            {
                StripeAccount = account.AccountId
            });

            return Ok(new
            {
                account,
                balance
            });
        }


    }
}