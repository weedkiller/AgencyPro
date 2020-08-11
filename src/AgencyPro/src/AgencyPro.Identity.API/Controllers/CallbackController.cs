// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Roles.Services;
using AgencyPro.Identity.API.Controllers.Home;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AgencyPro.Core.FinancialAccounts.Services;
using AgencyPro.Core.Stripe.Services;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Identity.API.Controllers
{
    [Authorize]
    [Route("callback")]
    public class CallbackController : Controller
    {
        private readonly ILogger<CallbackController> _logger;
        private readonly ICustomer _customer;
        private readonly IFinancialAccountService _financialAccountService;
        private readonly IStripeService _stripeService;
        private readonly AccountService _accountService;

        public CallbackController(
            ILogger<CallbackController> logger,
            ICustomer customer,
            IFinancialAccountService financialAccountService,
            IStripeService stripeService,
            AccountService accountService)
        {
            _logger = logger;
            _customer = customer;
            _financialAccountService = financialAccountService;
            _stripeService = stripeService;
            _accountService = accountService;
        }


        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(CallbackController)}.{callerName}] - {message}";
        }

        [HttpGet("stripe")]
        public async Task<IActionResult> Stripe([FromQuery]string code, [FromQuery] string state, [FromQuery] string scope, [FromQuery]string error, [FromQuery]string errorDescription)
        {
            _logger.LogInformation(GetLogMessage("State: {0}"), state);

            if (!string.IsNullOrWhiteSpace(error))
            {
                _logger.LogWarning(GetLogMessage(error));

                return View("Error", new ErrorViewModel()
                {
                    Error = new ErrorMessage()
                    {
                        ErrorDescription = errorDescription
                    }
                });
            }
               


            var service = new OAuthTokenService();
            var response = service.Create(new OAuthTokenCreateOptions()
            {
                GrantType = "authorization_code",
                Code = code,
            });

            if (Guid.TryParse(state, out var organizationId))
            {
                _logger.LogDebug(GetLogMessage("Organization: {0}"), organizationId);

                var account = await _accountService.UpdateAsync(response.StripeUserId, new AccountUpdateOptions()
                {
                    Metadata = new Dictionary<string, string>()
                    {
                        {"org-id", organizationId.ToString()},
                        {"person-id", _customer.Id.ToString()}
                    }
                });

                var result = await _financialAccountService.AccountCreatedOrUpdated(account);

                _logger.LogDebug(GetLogMessage("Result: {@result}"), result);

                if (result.Succeeded)
                {
                    _logger.LogDebug(GetLogMessage("Succeeded"));
                }
                else
                {
                    _logger.LogDebug(GetLogMessage("Failed"));

                }



            }
            else
            {
                _logger.LogDebug(GetLogMessage("Person: {0}"), _customer.Id);

                var account = await _accountService.UpdateAsync(response.StripeUserId, new AccountUpdateOptions()
                {
                    Metadata = new Dictionary<string, string>()
                    {
                        {"person-id", _customer.Id.ToString()}
                    }
                });

                var result = await _financialAccountService.AccountCreatedOrUpdated(account);
                _logger.LogDebug(GetLogMessage("Result: {@result}"), result);

                if (result.Succeeded)
                {
                    _logger.LogDebug(GetLogMessage("Succeeded"));
                }
                else
                {
                    _logger.LogDebug(GetLogMessage("Succeeded"));

                }


            }
            return Content("Your accounts was successfully linked, you may now close this browser");
        }

    }
}