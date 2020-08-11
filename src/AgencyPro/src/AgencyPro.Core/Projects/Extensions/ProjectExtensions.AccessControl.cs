// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Projects.Models;

namespace AgencyPro.Core.Projects.Extensions
{
    public static partial class ProjectExtensions
    {
        public static IQueryable<Project> ForAgencyOwner(this IQueryable<Project> entities,
            IProviderAgencyOwner ao)
        {
            return entities.Where(x => x.AccountManagerOrganizationId == ao.OrganizationId);
        }

        public static IQueryable<Project> ForOrganizationAccountManager(this IQueryable<Project> entities,
            IOrganizationAccountManager am)
        {
            return entities.Where(x =>
                x.AccountManagerId == am.AccountManagerId && x.AccountManagerOrganizationId == am.OrganizationId);
        }

        public static IQueryable<Project> ForOrganizationProjectManager(this IQueryable<Project> entities,
            IOrganizationProjectManager pm)
        {
            return entities.Where(x =>
                x.ProjectManagerId == pm.ProjectManagerId && x.ProjectManagerOrganizationId == pm.OrganizationId);
        }

        public static IQueryable<Project> ForOrganizationContractor(this IQueryable<Project> entities,
            IOrganizationContractor co)
        {
            return entities
                .Where(s => s.Contracts.Any(q =>
                    q.ContractorOrganizationId == co.OrganizationId && q.ContractorId == co.ContractorId));
        }

        public static IQueryable<Project> ForOrganizationCustomer(this IQueryable<Project> entities,
            IOrganizationCustomer cu)
        {
            return entities.Where(x => x.CustomerId == cu.CustomerId && x.CustomerOrganizationId == cu.OrganizationId);
        }
    }
}