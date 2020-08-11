// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.People.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stripe;

namespace AgencyPro.Services.FinancialAccounts
{
    public partial class FinancialAccountService
    {
        public async Task<bool> RemoveFinancialAccount(IPerson person)
        {
            var entity = await _organizationFinancialAccountRepository.Queryable()
                .Where(x => x.Id == person.Id).FirstAsync();

            entity.Updated = DateTimeOffset.UtcNow;

            var result = await _organizationFinancialAccountRepository.UpdateAsync(entity, true);

            return result > 0;
        }

        public async Task<bool> RemoveFinancialAccount(IAgencyOwner agencyOwner)
        {
            _logger.LogInformation("Remove Financial Account");
            var entity = await _organizationFinancialAccountRepository
                .Queryable()
                .Where(x => x.Id == agencyOwner.OrganizationId)
                .FirstAsync();

            var response = new AccountService().Delete(entity.FinancialAccountId);
            if (response.Deleted == true)
            {
                entity.ObjectState = ObjectState.Deleted;

                var result = await _organizationFinancialAccountRepository.DeleteAsync(entity, true);

                return result;
            }

            return false;
        }
    }
}