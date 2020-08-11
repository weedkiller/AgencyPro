// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Organizations.ViewModels;
using AgencyPro.Core.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Organizations.MarketingOrganizations.ViewModels;
using AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels;
using AgencyPro.Core.Organizations.RecruitingOrganizations.ViewModels;
using AgencyPro.Core.Roles.Services;

namespace AgencyPro.Core.Organizations.Services
{
    public interface IOrganizationService : IService<Organization>
    {
        Task<OrganizationResult> UpgradeOrganization(IOrganizationCustomer customer, OrganizationUpgradeInput input);

        Task<OrganizationResult> UpgradeOrganization(IAgencyOwner agencyOwner,
            MarketingOrganizationUpgradeInput input);
        Task<OrganizationResult> UpgradeOrganization(IAgencyOwner agencyOwner,
            ProviderOrganizationUpgradeInput input);
        Task<OrganizationResult> UpgradeOrganization(IAgencyOwner agencyOwner,
            RecruitingOrganizationUpgradeInput input);

        Task<OrganizationResult> UpdateOrganization(IAgencyOwner agencyOwner, OrganizationUpdateInput input);
        Task<OrganizationResult> UpdateOrganization(IMarketingAgencyOwner agencyOwner, MarketingOrganizationInput input);
        Task<OrganizationResult> UpdateOrganization(IProviderAgencyOwner agencyOwner, ProviderOrganizationInput input);
        Task<OrganizationResult> UpdateOrganization(IRecruitingAgencyOwner agencyOwner, RecruitingOrganizationInput input);

        //Task<T> UpdateMarketingOrganization<T>(IAgencyOwner agencyOwner, MarketingOrganizationUpdateInput input) where T : MarketingAgencyOwnerOrganizationOutput;
        //Task<T> UpdateProviderOrganization<T>(IAgencyOwner agencyOwner, ProviderOrganizationUpdateInput input) where T : ProviderAgencyOwnerOrganizationOutput;
        Task<OrganizationResult> UpdateBuyerOrganization(IOrganizationCustomer cu, OrganizationUpdateInput input);

        Task<OrganizationResult> UpdateOrganizationPic(IAgencyOwner agencyOwner, IFormFile image);
        Task<OrganizationResult> UpdateOrganizationPic(IOrganizationCustomer customerOrg, IFormFile image);

        Task<OrganizationResult> CreateOrganization(IAgencyOwner ao, OrganizationCreateInput input, Guid customerId);
        Task<OrganizationResult> CreateOrganization(ICustomer cu, OrganizationCreateInput input);
        Task<OrganizationResult> CreateOrganization(IOrganizationAccountManager am, OrganizationCreateInput input, Guid customerId);
        
        Task<OrganizationOutput> Get(Guid organizationId);
        Task<T> GetOrganization<T>(Guid organizationId) where T : OrganizationOutput;
        Task DeleteOrganization(Guid organizationId);

        Task<T> GetOrganization<T>(IOrganizationAccountManager am) where T : AccountManagerOrganizationOutput;
        Task<T> GetOrganization<T>(IOrganizationProjectManager pm) where T : ProjectManagerOrganizationOutput;
        Task<T> GetOrganization<T>(IOrganizationCustomer cu) where T : CustomerOrganizationOutput;
        Task<T> GetOrganization<T>(IOrganizationContractor co) where T : ContractorOrganizationOutput;
        Task<T> GetOrganization<T>(IOrganizationMarketer ma) where T : MarketerOrganizationOutput;
        Task<T> GetOrganization<T>(IOrganizationRecruiter re) where T : RecruiterOrganizationOutput;
        Task<T> GetOrganization<T>(IAgencyOwner ao) where T : OrganizationOutput;

        Task<T> GetProviderOrganization<T>(IOrganizationCustomer cu, Guid organizationId) where T : CustomerProviderOrganizationOutput;
        Task<List<T>> GetProviderOrganizations<T>(IOrganizationCustomer cu, OrganizationFilters filters) where T : CustomerProviderOrganizationOutput;
        Task<CustomerProviderOrganizationSummary> GetProviderOrganizationSummary(IOrganizationCustomer cu, OrganizationFilters filters);

        Task<List<T>> GetOrganizations<T>(Guid personId) where T : OrganizationOutput;
        Task<List<T>> GetOrganizations<T>(IMarketer marketer) where T : MarketerOrganizationOutput;
        Task<List<T>> GetOrganizations<T>(IRecruiter marketer) where T : RecruiterOrganizationOutput;

        Task<List<AffiliationOutput>> GetAffiliationsForPerson(Guid userUserId);


        Task<AgencyOwnerCounts> GetCounts(IAgencyOwner agencyOwner);
        Task<AgencyOwnerMarketingOrganizationDetailsOutput> GetMarketingDetails(IMarketingAgencyOwner agencyOwner);
        Task<AgencyOwnerRecruitingOrganizationDetailsOutput> GetRecruitingDetails(IRecruitingAgencyOwner agencyOwner);
        Task<AgencyOwnerProviderOrganizationDetailsOutput> GetProviderDetails(IProviderAgencyOwner agencyOwner);
    }
}