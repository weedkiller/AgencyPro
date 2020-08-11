// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Comments.Models;
using AgencyPro.Core.Comments.ViewModels;
using AgencyPro.Core.CustomerAccounts.Extensions;
using AgencyPro.Core.CustomerAccounts.Models;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Services.Comments
{
    public partial class CommentService
    {
        private async Task<bool> CreateAccountComment(CustomerAccount account, CommentInput input, Guid organizationId)
        {
            var comment = new Comment
            {
                AccountManagerId = account.AccountManagerId,
                AccountManagerOrganizationId = account.AccountManagerOrganizationId,
                CustomerId = account.CustomerId,
                CustomerOrganizationId = account.CustomerOrganizationId,
                OrganizationId = organizationId
            };

            return await CreateComment(comment, input);
        }

        public async Task<bool> CreateAccountComment(IProviderAgencyOwner agencyOwner, int accountId, CommentInput input)
        {
            var account = await _accountRepository
                .Queryable()
                .ForAgencyOwner(agencyOwner)
                .Where(x => x.Number == accountId)
                .FirstAsync();

            return await CreateAccountComment(account, input, agencyOwner.OrganizationId);
        }

        public async Task<bool> CreateAccountComment(IOrganizationAccountManager accountManager, int accountId,
            CommentInput input)
        {
            var account = await _accountRepository.Queryable().ForOrganizationAccountManager(accountManager)
                .Where(x => x.Number == accountId)
                .FirstAsync();

            return await CreateAccountComment(account, input, accountManager.OrganizationId);
        }

        public async Task<bool> CreateAccountComment(IOrganizationCustomer customer, int accountId, CommentInput input)
        {
            var account = await _accountRepository.Queryable().ForOrganizationCustomer(customer)
                .Where(x => x.BuyerNumber == accountId)
                .FirstAsync();

            return await CreateAccountComment(account, input, customer.OrganizationId);
        }
    }
}