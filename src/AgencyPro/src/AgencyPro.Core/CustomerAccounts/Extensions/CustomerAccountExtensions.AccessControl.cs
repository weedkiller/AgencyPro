// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using AgencyPro.Core.CustomerAccounts.Models;
using AgencyPro.Core.OrganizationRoles.Services;

namespace AgencyPro.Core.CustomerAccounts.Extensions
{
    public static partial class CustomerAccountExtensions
    {
        public static IQueryable<CustomerAccount> ForAgencyOwner(this IQueryable<CustomerAccount> entities,
            IProviderAgencyOwner ao)
        {
            return entities.Where(x => x.AccountManagerOrganizationId == ao.OrganizationId);
        }

        public static IQueryable<CustomerAccount> ForOrganizationAccountManager(
            this IQueryable<CustomerAccount> entities, IOrganizationAccountManager am)
        {
            return entities.Where(x =>
                x.AccountManagerId == am.AccountManagerId && x.AccountManagerOrganizationId == am.OrganizationId);
        }

        public static IQueryable<CustomerAccount> ForOrganizationCustomer(this IQueryable<CustomerAccount> entities,
            IOrganizationCustomer cu)
        {
            return entities.Where(x => x.CustomerId == cu.CustomerId && x.CustomerOrganizationId == cu.OrganizationId);
        }
    }
}