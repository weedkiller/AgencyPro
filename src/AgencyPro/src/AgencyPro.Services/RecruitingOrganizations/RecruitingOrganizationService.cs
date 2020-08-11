// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Agreements.Models;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.RecruitingOrganizations.Models;
using AgencyPro.Core.Organizations.RecruitingOrganizations.Services;
using AgencyPro.Core.Organizations.RecruitingOrganizations.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Services.RecruitingOrganizations
{
    public class RecruitingOrganizationService : Service<RecruitingOrganization>, IRecruitingOrganizationService
    {
        private readonly IRepositoryAsync<RecruitingAgreement> _recruitingAgreement;

        public RecruitingOrganizationService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _recruitingAgreement = UnitOfWork.RepositoryAsync<RecruitingAgreement>();
        }

        public async Task<List<T>> Discover<T>(IProviderAgencyOwner owner) where T : RecruitingOrganizationOutput
        {
            var exclude = await _recruitingAgreement.Queryable().Where(x => x.ProviderOrganizationId == owner.OrganizationId)
                .Select(x => x.RecruitingOrganizationId).ToListAsync();
            exclude.Add(owner.OrganizationId);

            return await Repository.Queryable()
                .Include(x => x.Organization)
                .ThenInclude(x => x.OrganizationFinancialAccount)
                .Where(x => !exclude.Contains(x.Id) && x.Organization.OrganizationFinancialAccount != null && x.Discoverable)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }
    }
}