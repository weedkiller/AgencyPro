// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using AgencyPro.Core.Candidates.Models;
using AgencyPro.Core.OrganizationRoles.Services;

namespace AgencyPro.Core.Candidates.Extensions
{
    public static partial class CandidateExtensions
    {
        public static IQueryable<Candidate> ForOrganizationProjectManager(this IQueryable<Candidate> entities,
            IOrganizationProjectManager pm)
        {
            return entities.Where(x =>
                x.ProjectManagerId == pm.ProjectManagerId && x.ProjectManagerOrganizationId == pm.OrganizationId);
        }

        public static IQueryable<Candidate> ForOrganizationRecruiter(this IQueryable<Candidate> entities,
            IOrganizationRecruiter re)
        {
            return entities.Where(
                x => x.RecruiterId == re.RecruiterId && x.RecruiterOrganizationId == re.OrganizationId);
        }

        public static IQueryable<Candidate> ForAgencyOwner(this IQueryable<Candidate> entities, IProviderAgencyOwner ao)
        {
            return entities.Where(x => x.ProviderOrganizationId == ao.OrganizationId);
        }
    }
}