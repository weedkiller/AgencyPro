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
using AgencyPro.Core.Orders.Services;
using AgencyPro.Core.Orders.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Stripe.Services;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Customer.API.Controllers.v2
{
    public class CustomerAccountController : OrganizationUserController
    {
        private readonly ICustomerAccountService _accountService;
        private readonly IOrganizationCustomer _customer;
        private readonly IWorkOrderService _workOrderService;
        private readonly ICommentService _commentService;
        private readonly IStripeService _stripeService;


        public CustomerAccountController(ICustomerAccountService accountService,
            IOrganizationCustomer customer,
            IWorkOrderService workOrderService,
            ICommentService commentService,
            IStripeService stripeService,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _customer = customer;
            _workOrderService = workOrderService;
            _commentService = commentService;
            _stripeService = stripeService;
            _accountService = accountService;
        }



        /// <summary>
        ///     Get all accounts for the current customers
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<CustomerCustomerAccountOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccounts(
            [FromRoute] Guid organizationId,
            [FromQuery] CommonFilters filters
        )
        {
            var accounts = await _accountService
                .GetAccounts<CustomerCustomerAccountOutput>(_customer, filters);
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
        [ProducesResponseType(typeof(CustomerCustomerAccountDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccount(
            [FromRoute] Guid organizationId,
            [FromRoute] int accountId
        )
        {
            var account = await _accountService.GetAccount<CustomerCustomerAccountDetailsOutput>(_customer, accountId);

            return Ok(account);
        }

        /// <summary>
        /// Create account at a provider organization 
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(CustomerCustomerAccountDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> JoinProviderOrganization(
            [FromRoute] Guid organizationId,
            [FromBody] JoinAsCustomerInput input)
        {
            var result = await _accountService.Create(_customer, input);

            if (!string.IsNullOrWhiteSpace(input.WorkOrderDescription))
            {
                await _workOrderService.CreateWorkOrder<BuyerWorkOrderOutput>(_customer, new WorkOrderInput()
                {
                    AccountManagerId = result.AccountManagerId.Value,
                    AccountManagerOrganizationId = result.AccountManagerOrganizationId.Value,
                    Description = input.WorkOrderDescription,
                    IsDraft = false
                });
            }

            return Ok(result);
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
            var result = await _commentService.CreateAccountComment(_customer, number, input);
            return Ok(result);
        }
    }

   
}