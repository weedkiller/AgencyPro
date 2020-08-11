// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels;

namespace AgencyPro.Core.Organizations.ProviderOrganizations.Services
{
    public interface IProviderOrganizationService
    {
        Task<List<MarketerProviderOrganizationOutput>> GetProviderOrganizations(IOrganizationMarketer marketer);
        Task<List<RecruiterProviderOrganizationOutput>> GetProviderOrganizations(IOrganizationRecruiter recruiter);

        Task<List<T>> Discover<T>(IMarketingAgencyOwner principal) where T : MarketingAgencyOwnerProviderOrganizationOutput;
        Task<List<T>> Discover<T>(IRecruitingAgencyOwner principal) where T : RecruitingAgencyOwnerProviderOrganizationOutput;
    }
}