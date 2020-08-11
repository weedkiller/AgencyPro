// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Agreements.Models;
using AgencyPro.Core.Agreements.Services;
using AgencyPro.Core.Agreements.ViewModels;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.ProviderOrganizations;
using AgencyPro.Core.Organizations.RecruitingOrganizations.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Agreements
{
    public partial class RecruitingAgreementService : Service<RecruitingAgreement>, IRecruitingAgreementService
    {
        private readonly ILogger<RecruitingAgreementService> _logger;
        private readonly IRepositoryAsync<RecruitingOrganization> _recruitingOrganizations;
        private readonly IRepositoryAsync<ProviderOrganization> _providerOrganizations;

        public RecruitingAgreementService(IServiceProvider serviceProvider, ILogger<RecruitingAgreementService> logger) : base(serviceProvider)
        {
            _logger = logger;
            _recruitingOrganizations = UnitOfWork.RepositoryAsync<RecruitingOrganization>();
            _providerOrganizations = UnitOfWork.RepositoryAsync<ProviderOrganization>();
        }

        public Task<List<T>> GetAgreements<T>(IProviderAgencyOwner principal) where T : RecruitingAgreementOutput
        {
            return Repository.Queryable().Where(x => x.ProviderOrganizationId == principal.OrganizationId)
                .Where(x => x.Status != AgreementStatus.Rejected)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<T> GetAgreement<T>(IProviderAgencyOwner principal, Guid recruitingAgencyId) where T : RecruitingAgreementOutput
        {
            return Repository.Queryable().Where(x => x.ProviderOrganizationId == principal.OrganizationId && x.RecruitingOrganizationId == recruitingAgencyId)
                .Where(x => x.Status != AgreementStatus.Rejected)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<List<T>> GetAgreements<T>(IRecruitingAgencyOwner principal) where T : RecruitingAgreementOutput
        {
            return Repository.Queryable().Where(x => x.RecruitingOrganizationId == principal.OrganizationId)
                .Where(x => x.Status != AgreementStatus.Rejected)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public  Task<T> GetAgreement<T>(IRecruitingAgencyOwner principal, Guid providerAgencyId) where T : RecruitingAgreementOutput
        {
            return Repository.Queryable().Where(x => x.RecruitingOrganizationId == principal.OrganizationId && x.ProviderOrganizationId == providerAgencyId)
                .Where(x => x.Status != AgreementStatus.Rejected)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<List<T>> GetAgreements<T>(IOrganizationRecruiter principal) where T : RecruitingAgreementOutput
        {
            return Repository.Queryable().Where(x => x.RecruitingOrganizationId == principal.OrganizationId)
                .Where(x => x.Status == AgreementStatus.Approved)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }
    }
}