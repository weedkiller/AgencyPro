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
using AgencyPro.Core.Organizations.ProviderOrganizations;
using AgencyPro.Core.Organizations.ProviderOrganizations.Services;
using AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Services.ProviderOrganizations
{
    public partial class ProviderOrganizationService : Service<ProviderOrganization>, IProviderOrganizationService
    {
        private readonly IRepositoryAsync<MarketingAgreement> _marketingAgreement;
        private readonly IRepositoryAsync<RecruitingAgreement> _recruitingAgreement;

        public ProviderOrganizationService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _marketingAgreement = UnitOfWork.RepositoryAsync<MarketingAgreement>();
            _recruitingAgreement = UnitOfWork.RepositoryAsync<RecruitingAgreement>();
        }

        public async Task<List<MarketerProviderOrganizationOutput>> GetProviderOrganizations(IOrganizationMarketer marketer)
        {
            var result = await _marketingAgreement.Queryable()
                .Where(x => x.MarketingOrganizationId == marketer.OrganizationId && x.Status == AgreementStatus.Approved)
                .Select(x => x.ProviderOrganization)
                .ProjectTo<MarketerProviderOrganizationOutput>(ProjectionMapping)
                .ToListAsync();

            return result;
        }

        public async Task<List<RecruiterProviderOrganizationOutput>> GetProviderOrganizations(IOrganizationRecruiter recruiter)
        {
            var result = await _recruitingAgreement.Queryable()
                .Where(x => x.RecruitingOrganizationId == recruiter.OrganizationId && x.Status == AgreementStatus.Approved)
                .Select(x => x.ProviderOrganization)
                .ProjectTo<RecruiterProviderOrganizationOutput>(ProjectionMapping)
                .ToListAsync();

            return result;
        }
    }
}