// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Agreements.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;

namespace AgencyPro.Core.Agreements.Services
{
    public interface IMarketingAgreementService
    {
        Task<List<T>> GetAgreements<T>(IProviderAgencyOwner providerAgencyOwner) 
            where T : MarketingAgreementOutput; 

        Task<T> GetAgreement<T>(IProviderAgencyOwner providerAgencyOwner, Guid marketingOrganizationId) 
            where T : MarketingAgreementOutput;

        Task<List<T>> GetAgreements<T>(IMarketingAgencyOwner marketingAgencyOwner) where T : MarketingAgreementOutput;
        Task<T> GetAgreement<T>(IMarketingAgencyOwner marketingAgencyOwner, Guid providerOrganizationId) where T : MarketingAgreementOutput;
        Task<List<T>> GetAgreements<T>(IOrganizationMarketer marketer) where T : MarketingAgreementOutput;

        Task<AgreementResult> CreateAgreement(IMarketingAgencyOwner marketingAgencyOwner, 
            Guid providerOrganizationId);

        Task<AgreementResult> AcceptAgreement(IProviderAgencyOwner providerAgencyOwner,
            Guid marketingOrganizationId);
        
        Task<AgreementResult> AcceptAgreement(IMarketingAgencyOwner marketingAgencyOwner,
            Guid providerOrganizationId);

        Task<AgreementResult> CreateAgreement(IProviderAgencyOwner providerAgencyOwner, Guid marketingOrganizationId);
    }
}