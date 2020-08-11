// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Cards.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using Stripe;

namespace AgencyPro.Core.Cards.Services
{
    public interface ICardService
    {
        Task<int> AddCustomerCard(string customerId, CardInputModel input);
        Task<int> AddAccountCard(string customerId, CardInputModel input);

        Task<int> PullCard(Card card);
        Task<int> PullCard(IExternalAccount card);
        Task<List<CustomerCardViewModel>> GetCards(IOrganizationCustomer customer);
    }
}