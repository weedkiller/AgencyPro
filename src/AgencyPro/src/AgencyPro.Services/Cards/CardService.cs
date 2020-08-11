// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Cards.Models;
using AgencyPro.Core.Cards.Services;
using AgencyPro.Core.Cards.ViewModels;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;
using Omu.ValueInjecter;
using Stripe;

namespace AgencyPro.Services.Cards
{
    public class CardService : Service<StripeCard>, ICardService
    {
        private readonly global::Stripe.CardService _cardService;
        private readonly ExternalAccountService _externalAccountService;
        private readonly IRepositoryAsync<AccountCard> _accountCards;
        private readonly IRepositoryAsync<CustomerCard> _customerCards;

        public CardService(IServiceProvider serviceProvider, 

            global::Stripe.CardService cardService, ExternalAccountService externalAccountService ) : base(serviceProvider)
        {
            _cardService = cardService;
            _externalAccountService = externalAccountService;
            _accountCards = UnitOfWork.RepositoryAsync<AccountCard>();
            _customerCards = UnitOfWork.RepositoryAsync<CustomerCard>();
        }

        

        public async Task<int> AddCustomerCard(string customerId, CardInputModel input)
        {
            var card = _cardService.Create(customerId, new CardCreateOptions()
            {
                Source = input.ExternalAccount,
                Validate = true
            });

            return await PullCard(card);
        }

        public async Task<int> AddAccountCard(string accountId, CardInputModel input)
        {
            var card = _externalAccountService.Create(accountId, new ExternalAccountCreateOptions()
            {
                ExternalAccount = input.ExternalAccount
            });

            return await PullCard(card);
        }

        public async Task<int> PullCard(Card card)
        {
            var customerCard = await _customerCards.Queryable().Include(x=>x.StripeCard)
                .Where(x => x.Id == card.Id).FirstOrDefaultAsync();

            if (customerCard == null)
            {
                customerCard = new CustomerCard()
                {
                    Id = card.Id,
                    ObjectState = ObjectState.Added,
                    StripeCard = new StripeCard()
                    {
                        Id = card.Id,
                        ObjectState = ObjectState.Added
                    }
                };
            }
            else
            {
                customerCard.StripeCard.ObjectState = ObjectState.Modified;
                customerCard.ObjectState = ObjectState.Modified;
            }


            customerCard.CustomerId = card.CustomerId;
            customerCard.Id = card.Id;
            customerCard.StripeCard.InjectFrom(card);

            return _customerCards.InsertOrUpdateGraph(customerCard, true);
        }

        public async Task<int> PullCard(IExternalAccount card)
        {
            var accountCard = await _accountCards.Queryable()
                .Include(x=>x.StripeCard).Where(x => x.Id == card.Id).FirstOrDefaultAsync();

            if (accountCard == null)
            {
                accountCard = new AccountCard()
                {
                    Id = card.Id,
                    ObjectState = ObjectState.Added,
                    StripeCard = new StripeCard()
                    {
                        Id = card.Id,
                        ObjectState = ObjectState.Added
                    }
                };
            }
            else
            {
                accountCard.ObjectState = ObjectState.Modified;
                accountCard.StripeCard.ObjectState = ObjectState.Modified;
            }


            accountCard.Id = card.Id;
            accountCard.AccountId = card.AccountId;

            accountCard.StripeCard.InjectFrom(card);

            return _accountCards.InsertOrUpdateGraph(accountCard, true);
        }
        
        public async Task<List<CustomerCardViewModel>> GetCards(IOrganizationCustomer customer)
        {
            var cards = await _customerCards.Queryable()
                .Where(x => x.Customer.OrganizationBuyerAccount.Id == customer.OrganizationId)
                .ProjectTo<CustomerCardViewModel>(ProjectionMapping)
                .ToListAsync();

            return cards;
        }
    }
}
