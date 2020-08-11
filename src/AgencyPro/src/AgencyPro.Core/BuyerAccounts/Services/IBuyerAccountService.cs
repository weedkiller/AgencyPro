// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.BuyerAccounts.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using Stripe;

namespace AgencyPro.Core.BuyerAccounts.Services
{
    public interface IBuyerAccountService
    {
        Task<BuyerAccountOutput> GetBuyerAccount(IOrganizationCustomer customer);

        Task<int> PullCustomer(Customer customer);


        Task<int> PushCustomer(Guid organizationId, Guid customerId);

        Task<int> ImportBuyerAccounts(int limit);
        Task<int> ExportCustomers();
        Task<int> ExportCustomers(Guid organizationId);
        Task<string> GetAuthUrl(IOrganizationCustomer customer);
        Task<string> GetStripeUrl(IOrganizationCustomer customer, bool isRecursive = false);


    }
}