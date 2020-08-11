// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using AgencyPro.Core.OrganizationRoles.Models;

namespace AgencyPro.Data.Repositories
{
    public static class OrganizationProjectManagerRepository
    {
        public static IQueryable<OrganizationProjectManager> GetForOrganization(
            this IQueryable<OrganizationProjectManager> repo, Guid organizationId)
        {
            return repo.Where(x => x.OrganizationId == organizationId);
        }

        public static IQueryable<OrganizationProjectManager> GetById(
            this IQueryable<OrganizationProjectManager> repo, Guid organizationId, Guid projectManagerId)
        {
            return repo.Where(x => x.OrganizationId == organizationId && x.ProjectManagerId == projectManagerId);
        }
    }
}