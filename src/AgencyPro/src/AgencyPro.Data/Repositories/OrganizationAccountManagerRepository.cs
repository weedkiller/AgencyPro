// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using AgencyPro.Core.OrganizationRoles.Models;

namespace AgencyPro.Data.Repositories
{
    public static class OrganizationAccountManagerRepository
    {
        public static IQueryable<OrganizationAccountManager> GetForOrganization(
            this IQueryable<OrganizationAccountManager> repo, Guid organizationId)
        {
            return repo.Where(x => x.OrganizationId == organizationId);
        }

        public static IQueryable<OrganizationAccountManager> GetById(
            this IQueryable<OrganizationAccountManager> repo, Guid organizationId, Guid accountManagerId)
        {
            return repo.Where(x => x.OrganizationId == organizationId && x.AccountManagerId == accountManagerId);
        }
    }
}