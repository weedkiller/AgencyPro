// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.BuyerAccounts.Services;
using AgencyPro.Core.Cards.Services;
using AgencyPro.Core.Cards.ViewModels;
using AgencyPro.Core.FinancialAccounts.Services;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Customer.API.Controllers.v2
{
    public class CardController : OrganizationUserController
    {
        private readonly ICardService _cardService;
        private readonly IOrganizationCustomer _customer;
        private readonly IFinancialAccountService _service;
        private readonly IBuyerAccountService _buyerService;

        public CardController(
            ICardService cardService,
            IOrganizationCustomer customer,
            IFinancialAccountService service,
            IBuyerAccountService buyerService,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _buyerService = buyerService;
            _service = service;
            _cardService = cardService;
            _customer = customer;
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateCard([FromRoute] Guid organizationId, [FromBody]CardInputModel input)
        {
            var account = await _buyerService.GetBuyerAccount(_customer);
            if (account == null)
                return BadRequest("buyer account is not setup in stripe");

            var results = await _cardService.AddCustomerCard(account.Id, input);

            return Ok(results);
        }

        /// <summary>
        /// gets the cards for the organization customer account
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<CustomerCardViewModel>),StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCards([FromRoute] Guid organizationId)
        {
            var cards = await _cardService.GetCards(_customer);
            return Ok(cards);
        }
    }
}
