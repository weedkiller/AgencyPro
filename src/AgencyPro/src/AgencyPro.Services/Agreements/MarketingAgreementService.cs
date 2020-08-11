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
using AgencyPro.Core.Organizations.MarketingOrganizations.Models;
using AgencyPro.Core.Organizations.ProviderOrganizations;
using AgencyPro.Services.Agreements.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Agreements
{
    public partial class MarketingAgreementService : Service<MarketingAgreement>, IMarketingAgreementService
    {
        private readonly ILogger<MarketingAgreementService> _logger;
        private readonly IRepositoryAsync<ProviderOrganization> _providerOrganization;
        private readonly IRepositoryAsync<MarketingOrganization> _marketingOrganizations;

        public MarketingAgreementService(
            MultiMarketingAgreementEventHandler marketingEvents,
            IServiceProvider serviceProvider, 
            ILogger<MarketingAgreementService> logger) : base(serviceProvider)
        {
            _logger = logger;

            _providerOrganization = UnitOfWork.RepositoryAsync<ProviderOrganization>();
            _marketingOrganizations = UnitOfWork.RepositoryAsync<MarketingOrganization>();
            AddEventHandler(marketingEvents);
        }

        public Task<List<T>> GetAgreements<T>(IProviderAgencyOwner providerAgencyOwner) where T : MarketingAgreementOutput
        {
            return Repository.Queryable().Where(x => x.ProviderOrganizationId == providerAgencyOwner.OrganizationId)
                .Where(x => x.Status != AgreementStatus.Rejected)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<T> GetAgreement<T>(IProviderAgencyOwner providerAgencyOwner, Guid marketingOrganizationId) where T : MarketingAgreementOutput
        {
            return Repository
                .Queryable()
                .Where(x => x.ProviderOrganizationId == providerAgencyOwner.OrganizationId && x.MarketingOrganizationId == marketingOrganizationId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<List<T>> GetAgreements<T>(IMarketingAgencyOwner marketingAgencyOwner) where T : MarketingAgreementOutput
        {
            return Repository.Queryable().Where(x => x.MarketingOrganizationId == marketingAgencyOwner.OrganizationId)
                .Where(x=>x.Status != AgreementStatus.Rejected)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<T> GetAgreement<T>(IMarketingAgencyOwner marketingAgencyOwner, Guid providerOrganizationId) where T : MarketingAgreementOutput
        {
            return Repository.Queryable().Where(x => x.ProviderOrganizationId == providerOrganizationId && x.MarketingOrganizationId == marketingAgencyOwner.OrganizationId)
                .Where(x => x.Status != AgreementStatus.Rejected)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<List<T>> GetAgreements<T>(IOrganizationMarketer marketer) where T : MarketingAgreementOutput
        {
            return Repository.Queryable().Where(x => x.MarketingOrganizationId == marketer.OrganizationId)
                .Where(x => x.Status == AgreementStatus.Approved)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }
    }
}
