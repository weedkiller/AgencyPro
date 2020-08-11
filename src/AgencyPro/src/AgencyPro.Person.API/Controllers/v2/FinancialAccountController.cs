// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.FinancialAccounts.Services;
using AgencyPro.Core.FinancialAccounts.ViewModels;
using AgencyPro.Core.People.Services;
using AgencyPro.Core.Stripe.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Person.API.Controllers.v2
{
    [Route("FinancialAccount")]
    public class FinancialAccountController : ControllerBase
    {
        private readonly IPerson _person;
        private readonly IFinancialAccountService _accountService;
        private readonly IStripeService _stripeService;

        public FinancialAccountController(IPerson person, IFinancialAccountService accountService, IStripeService stripeService)
        {
            _person = person;
            _accountService = accountService;
            _stripeService = stripeService;
        }

        /// <summary>
        /// Gets the financial accounts of a customer
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<FinancialAccountOutput>),StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFinancialAccount()
        {
            var account = await _accountService.GetFinancialAccount(_person);
            return Ok(account);
        }

        /// <summary>
        ///     Get the stripe auth url for the current customer
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("AuthUrl")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAuthUrl([FromRoute] Guid organizationId)
        {
            return Ok(await _stripeService.GetAuthUrl(_person));
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
            return Ok(await _stripeService.GetStripeUrl(_person));
        }



        /// <summary>
        /// Remove the financial account from the customer
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveFinancialAccount()
        {
            var result = await _accountService.RemoveFinancialAccount(_person);
            return Ok(result);
        }
        
    }
}
