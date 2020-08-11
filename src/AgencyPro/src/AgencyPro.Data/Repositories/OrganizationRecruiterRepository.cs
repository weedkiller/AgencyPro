// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.OrganizationRoles.Models;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Data.Repositories
{
    public static class OrganizationRecruiterRepository
    {
        public static IQueryable<OrganizationRecruiter> GetForOrganization(
            this IQueryable<OrganizationRecruiter> repo, Guid organizationId)
        {
            return repo.Where(x => x.OrganizationId == organizationId);
        }

        public static IQueryable<OrganizationRecruiter> GetById(
            this IQueryable<OrganizationRecruiter> repo, Guid organizationId, Guid recruiterId)
        {
            return repo.Where(x => x.OrganizationId == organizationId && x.RecruiterId == recruiterId);
        }

        public static async Task<OrganizationRecruiter> GetRecruiterOrDefault(this IRepositoryAsync<OrganizationRecruiter> organizationRecruiterRepository,
           Guid? organizationId, Guid? recruiterId, string referralCode)
        {
            OrganizationRecruiter re = null;
           
            if (organizationId.HasValue && recruiterId.HasValue)
            {
                re = await organizationRecruiterRepository
                    .FirstOrDefaultAsync(x => x.RecruiterId == recruiterId && x.OrganizationId == organizationId);
            }

            if (re == null)
            {
                if (organizationId.HasValue)
                {
                    re = await organizationRecruiterRepository.Queryable()
                             .Where(x => x.OrganizationId == organizationId.Value && x.RecruitingOrganizationDefaults.Any())
                             .FirstOrDefaultAsync() ?? await organizationRecruiterRepository.Queryable().Where(x => x.IsSystemDefault)
                             .FirstAsync();
                }
                else
                {
                    re = await organizationRecruiterRepository.Queryable().Where(x => x.IsSystemDefault)
                        .FirstAsync();
                }
            }

            return re;
        }
    }
}
