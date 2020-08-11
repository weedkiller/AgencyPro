// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyPro.Services.ProviderOrganizations
{
    public partial class ProviderOrganizationService
    {
        public async Task<List<T>> Discover<T>(IMarketingAgencyOwner owner) where T : MarketingAgencyOwnerProviderOrganizationOutput
        {
            var exclude = await _marketingAgreement.Queryable()
                .Where(x => x.MarketingOrganizationId == owner.OrganizationId)
                .Select(x => x.ProviderOrganizationId).ToListAsync();
            exclude.Add(owner.OrganizationId);

            return await Repository.Queryable()
                .Include(x => x.Organization)
                .ThenInclude(x => x.OrganizationFinancialAccount)
                .Where(x => !exclude.Contains(x.Id) && x.Organization.OrganizationFinancialAccount != null && x.Discoverable)
                .ProjectTo<T>(ProjectionMapping).ToListAsync();
        }

        public async Task<List<T>> Discover<T>(IRecruitingAgencyOwner owner) where T : RecruitingAgencyOwnerProviderOrganizationOutput
        {
            var exclude = await _recruitingAgreement.Queryable()
                .Where(x => x.RecruitingOrganizationId == owner.OrganizationId)
                .Select(x => x.ProviderOrganizationId).ToListAsync();
            exclude.Add(owner.OrganizationId);

            return await Repository.Queryable()
                .Include(x=>x.Organization)
                .ThenInclude(x=>x.OrganizationFinancialAccount)
                .Where(x => !exclude.Contains(x.Id) && x.Organization.OrganizationFinancialAccount != null && x.Discoverable)
                .ProjectTo<T>(ProjectionMapping).ToListAsync();
        }
    }
}