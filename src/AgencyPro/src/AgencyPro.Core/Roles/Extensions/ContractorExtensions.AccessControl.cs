// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Roles.Models;

namespace AgencyPro.Core.Roles.Extensions
{
    public static partial class ContractorExtensions
    {
        public static IQueryable<Contractor> ForAgencyOwner(this IQueryable<Contractor> entities,
            IAgencyOwner ao)
        {
            return entities.Where(x => x.RecruiterOrganizationId == ao.OrganizationId);
        }

        public static IQueryable<Contractor> ForOrganizationRecruiter(this IQueryable<Contractor> entities,
            IOrganizationRecruiter re)
        {
            return entities.Where(x => x.RecruiterOrganizationId == re.OrganizationId && x.RecruiterId == re.RecruiterId);
        }
    }
}