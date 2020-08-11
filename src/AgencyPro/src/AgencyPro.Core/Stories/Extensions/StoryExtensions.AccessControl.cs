// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Stories.Models;

namespace AgencyPro.Core.Stories.Extensions
{
    public static partial class StoryExtensions
    {
        public static IQueryable<Story> ForAgencyOwner(this IQueryable<Story> entities,
            IProviderAgencyOwner ao)
        {
            return entities
                .Where(x => x.Project.AccountManagerOrganizationId == ao.OrganizationId);
        }

        public static IQueryable<Story> ForOrganizationAccountManager(this IQueryable<Story> entities,
            IOrganizationAccountManager am)
        {
            return entities.Where(x =>
                x.Project.AccountManagerId == am.AccountManagerId &&
                x.Project.AccountManagerOrganizationId == am.OrganizationId);
        }

        public static IQueryable<Story> ForOrganizationProjectManager(this IQueryable<Story> entities,
            IOrganizationProjectManager pm)
        {
            return entities.Where(x =>
                x.Project.ProjectManagerId == pm.ProjectManagerId &&
                x.Project.ProjectManagerOrganizationId == pm.OrganizationId);
        }

        public static IQueryable<Story> ForOrganizationContractor(this IQueryable<Story> entities,
            IOrganizationContractor co)
        {
            return entities.Where(x =>
                x.ContractorId == co.ContractorId && x.ContractorOrganizationId == co.OrganizationId);
        }

        public static IQueryable<Story> ForOrganizationCustomer(this IQueryable<Story> entities,
            IOrganizationCustomer cu)
        {
            return entities.Where(x =>
                x.Project.CustomerId == cu.CustomerId && x.Project.CustomerOrganizationId == cu.OrganizationId);
        }
    }
}