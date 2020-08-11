// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using AgencyPro.Core.OrganizationRoles.Models;

namespace AgencyPro.Data.Repositories
{
    public static class OrganizationContractorRepository
    {
        public static IQueryable<OrganizationContractor> GetForOrganization(
            this IQueryable<OrganizationContractor> repo, Guid organizationId)
        {
            return repo.Where(x => x.OrganizationId == organizationId);
        }

        public static IQueryable<OrganizationContractor> GetById(
            this IQueryable<OrganizationContractor> repo, Guid organizationId, Guid contractorId)
        {
            return repo.Where(x => x.OrganizationId == organizationId && x.ContractorId == contractorId);
        }
    }
}