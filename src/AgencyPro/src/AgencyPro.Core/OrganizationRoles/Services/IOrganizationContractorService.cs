// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationPeople.Filters;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors;
using AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels;
using AgencyPro.Core.Services;

namespace AgencyPro.Core.OrganizationRoles.Services
{
    public interface IOrganizationContractorService : IService<OrganizationContractor>,
        IOrganizationRoleService<OrganizationContractorInput,
            OrganizationContractorOutput, IOrganizationContractor, ContractorFilters, ContractorOrganizationOutput, ContractorCounts>
    {
        Task<List<T>> GetFeaturedContractors<T>(Guid organizationId)
            where T : OrganizationContractorOutput;
        
        Task<T> UpdateRecruiter<T>(IAgencyOwner agencyOwner, Guid contractorId, UpdateContractorRecruiterInput input) where T : AgencyOwnerOrganizationContractorOutput;
    }
}