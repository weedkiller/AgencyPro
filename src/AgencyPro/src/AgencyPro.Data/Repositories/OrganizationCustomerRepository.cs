// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using AgencyPro.Core.OrganizationRoles.Models;

namespace AgencyPro.Data.Repositories
{
    public static class OrganizationCustomerRepository
    {
        public static IQueryable<OrganizationCustomer> GetForOrganization(
            this IQueryable<OrganizationCustomer> repo, Guid organizationId)
        {
            return repo.Where(x => x.OrganizationId == organizationId);
        }

        public static IQueryable<OrganizationCustomer> GetById(
            this IQueryable<OrganizationCustomer> repo, Guid organizationId, Guid customerId)
        {
            return repo.Where(x => x.OrganizationId == organizationId && x.CustomerId == customerId);
        }
    }
}