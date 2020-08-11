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
    public static class OrganizationMarketerRepository
    {
        public static IQueryable<OrganizationMarketer> GetForOrganization(
            this IQueryable<OrganizationMarketer> repo, Guid organizationId)
        {
            return repo.Where(x => x.OrganizationId == organizationId);
        }

        public static IQueryable<OrganizationMarketer> GetById(
            this IQueryable<OrganizationMarketer> repo, Guid organizationId, Guid marketerId)
        {
            return repo.Where(x => x.OrganizationId == organizationId && x.MarketerId == marketerId);
        }

        public static async Task<OrganizationMarketer> GetMarketerOrDefault(this IRepositoryAsync<OrganizationMarketer> repo, Guid? organizationId, Guid? marketerId, string referralCode)
        {
            OrganizationMarketer ma = null;

            if (!string.IsNullOrWhiteSpace(referralCode))
            {
                // no recruiter referral codes yet
            }

            if (organizationId.HasValue && marketerId.HasValue)
            {
                ma = await repo.Queryable().Where(x => x.OrganizationId == organizationId && x.MarketerId == marketerId)
                    .FirstOrDefaultAsync();
            }

            if (ma == null)
            {
                if (organizationId.HasValue)
                {
                    ma = await repo.Queryable()
                        .Include(x=>x.OrganizationDefaults)
                        .Where(x => x.OrganizationId == organizationId.Value && x.OrganizationDefaults.Any())
                        .FirstOrDefaultAsync();
                }

                if (ma == null)
                {
                    ma = await repo.Queryable().Where(x => x.IsSystemDefault)
                        .FirstAsync();
                }
            }

            return ma;
        }
    }
}