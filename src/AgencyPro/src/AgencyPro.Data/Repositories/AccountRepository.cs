// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using AgencyPro.Core.CustomerAccounts.Extensions;
using AgencyPro.Core.CustomerAccounts.Filters;
using AgencyPro.Core.CustomerAccounts.Models;
using AgencyPro.Core.Data.Repositories;

namespace AgencyPro.Data.Repositories
{
    public static class AccountRepository
    {
        public static CustomerAccount GetById(this IRepositoryAsync<CustomerAccount> repo, Guid organizationId, int number)
        {
            return repo.Queryable()
                .ApplyWhereFilters(new CustomerAccountFilters()
            {
                AccountManagerOrganizationId = organizationId,
                Number = number,
            }).First();
        }
    }
}