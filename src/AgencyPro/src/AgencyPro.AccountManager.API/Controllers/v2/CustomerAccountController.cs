// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Comments.Services;
using AgencyPro.Core.Comments.ViewModels;
using AgencyPro.Core.Common;
using AgencyPro.Core.CustomerAccounts.Services;
using AgencyPro.Core.CustomerAccounts.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.AccountManager.API.Controllers.v2
{
    public class CustomerAccountController : OrganizationUserController
    {
        private readonly IOrganizationAccountManager _accountManager;
        private readonly ICustomerAccountService _accountService;
        private readonly ICommentService _commentService;

        public CustomerAccountController(
            ICustomerAccountService accountService,
            ICommentService commentService,
            IOrganizationAccountManager accountManager,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _commentService = commentService;
            _accountService = accountService;
            _accountManager = accountManager;
        }

        /// <summary>
        ///     Get all accounts for the current account manager
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<AccountManagerCustomerAccountOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(
            [FromRoute] Guid organizationId,
            [FromQuery] CommonFilters filters)
        {
            var accounts = await _accountService
                .GetAccounts<AccountManagerCustomerAccountOutput>(_accountManager, filters);
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
        [ProducesResponseType(typeof(AccountManagerCustomerAccountDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(
            [FromRoute] Guid organizationId,
            [FromRoute] int accountId)
        {
            var acct = await _accountService.GetAccount<AccountManagerCustomerAccountDetailsOutput>(
                _accountManager, accountId);

            return Ok(acct);
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
                .Create(_accountManager, model);

            if (model.SendEmail)
            {

            }

            return Ok(account);
        }

        /// <summary>
        /// Creates an internal account (buyer account for the current organization)
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpPost("internal")]
        [ProducesResponseType(typeof(CustomerAccountResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateInternalAccount(
            [FromRoute] Guid organizationId)
        {
            var account = await _accountService
                .CreateInternalAccount(_accountManager);

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
            var account = await _accountService.LinkOrganizationCustomer(_accountManager, input);
            return Ok(account);
        }

        /// <summary>
        /// deactivates an account
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        [HttpPatch("{number}/deactivate")]
        [ProducesResponseType(typeof(AccountManagerCustomerAccountDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Deactivate([FromRoute] Guid organizationId, [FromRoute] int number)
        {
            var account = await _accountService
                .Deactivate(_accountManager, number);

            return Ok(account);

        }

        /// <summary>
        /// Create a comment for the account
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="number"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{number}/comments")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateComment(
            [FromRoute] Guid organizationId,
            [FromRoute] int number,
            [FromBody]CommentInput input)
        {
            var result = await _commentService.CreateAccountComment(_accountManager, number, input);
            return Ok(result);
        }
    }
}