// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;
using AgencyPro.Core.FinancialAccounts.ViewModels;
using AgencyPro.Core.OrganizationPeople.Services;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.People.Services;
using AgencyPro.Core.Roles.Services;

namespace AgencyPro.Core.FinancialAccounts.Services
{
    public interface IFinancialAccountService
    {
        Task<FinancialAccountDetails> GetFinancialAccount(IPerson person);
        Task<FinancialAccountDetails> GetFinancialAccount(IAgencyOwner customer);

        Task<FinancialAccountResult> AccountCreatedOrUpdated(global::Stripe.Account account);

        Task<bool> RemoveFinancialAccount(IPerson person);
        Task<bool> RemoveFinancialAccount(IAgencyOwner agencyOwner);
    }
}
