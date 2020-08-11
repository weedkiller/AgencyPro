// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Comments.Services;
using AgencyPro.Core.Comments.ViewModels;
using AgencyPro.Core.CustomerAccounts.Services;
using AgencyPro.Core.CustomerAccounts.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.Common;

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class AccountController : OrganizationUserController
    {
        private readonly ICustomerAccountService _accountService;
        private readonly ICommentService _commentService;
        private readonly IProviderAgencyOwner _agencyOwner;

        public AccountController(
            ICommentService commentService,
            ICustomerAccountService accountService,
            IProviderAgencyOwner agencyOwner,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _commentService = commentService;
            _agencyOwner = agencyOwner;
            _accountService = accountService;
        }

        /// <summary>
        ///     Get all accounts for the current account manager
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<AgencyOwnerCustomerAccountOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccounts(
            [FromRoute] Guid organizationId, [FromQuery] CommonFilters filters
        )
        {
            var accounts = await _accountService
                .GetAccounts<AgencyOwnerCustomerAccountOutput>(_agencyOwner, filters);
            AddPagination(filters, accounts.Total);
            return Ok(accounts.Data);
        }

        /// <summary>
        ///     Get acct for the current account manager
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpGet("{accountId}")]
        [ProducesResponseType(typeof(AgencyOwnerCustomerAccountDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccount(
            [FromRoute] Guid organizationId,
            [FromRoute] int accountId
        )
        {
            var account = await _accountService
                .GetAccount<AgencyOwnerCustomerAccountDetailsOutput>(_agencyOwner, accountId);

            return Ok(account);
        }

        /// <summary>
        ///     Create and return a new account
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("")]
        
        [ProducesResponseType(typeof(CustomerAccountResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAccount(
            [FromRoute] Guid organizationId,
            [FromBody] NewCustomerAccountInput model)
        {

            var account = await _accountService
                .Create(_agencyOwner, model);

            return Ok(account);
        }


        /// <summary>
        /// Creates an internal account (buyer account for the current organization)
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="accountManagerId"></param>
        /// <returns></returns>
        [HttpPost("{accountManagerId}/internal")]
        [ProducesResponseType(typeof(CustomerAccountResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateInternalAccount(
            [FromRoute] Guid organizationId, [FromRoute]Guid accountManagerId)
        {
            var account = await _accountService
                .CreateInternalAccount(new OrganizationAccountManager()
                {
                    AccountManagerId = accountManagerId,
                    OrganizationId = organizationId
                });

            return Ok(account);
        }

        /// <summary>
        ///     Updates an account and returns it
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="accountId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{accountId}")]
        
        [ProducesResponseType(typeof(CustomerAccountResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAccount(
            [FromRoute] Guid organizationId,
            [FromRoute] int accountId,
            [FromBody] CustomerAccountInput model)
        {
            var account = await _accountService
                .Update(_agencyOwner, accountId, model);
            return Ok(account);
        }

        /// <summary>
        ///     Link an existing account
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("link")]
        
        [ProducesResponseType(typeof(CustomerAccountResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> LinkAccount(
            [FromRoute] Guid organizationId,
            [FromBody] LinkCustomerWithCompanyInput input)
        {
            var account = await _accountService.LinkOrganizationCustomer(_agencyOwner, input);
            return Ok(account);
        }

        /// <summary>
        ///     Deletes an account
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpDelete("{accountId}")]
        [ProducesResponseType(typeof(CustomerAccountResult), 200)]
        public async Task<IActionResult> DeleteAccount(
            [FromRoute] Guid organizationId,
            [FromRoute] int accountId)
        {
            var result = await _accountService
                .DeleteAccount(_agencyOwner, accountId);
            return Ok(result);
        }

        /// <summary>
        /// deactivates an account
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        [HttpPatch("{number}/deactivate")]
        [ProducesResponseType(typeof(CustomerAccountResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Deactivate([FromRoute] Guid organizationId, [FromRoute] int number)
        {
            var account = await _accountService
                .Deactivate(_agencyOwner, number);

            return Ok(account);
        }


        /// <summary>
        /// Add comment to a customer account
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="accountId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{accountId}/comments")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateComment(
            [FromRoute] Guid organizationId,
            [FromRoute] int accountId,
            [FromBody]CommentInput input
        )
        {
            var result = await _commentService.CreateAccountComment(_agencyOwner, accountId, input);
            return Ok(result);
        }
    }
}