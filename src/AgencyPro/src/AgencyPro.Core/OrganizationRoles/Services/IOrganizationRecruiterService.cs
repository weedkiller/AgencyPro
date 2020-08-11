// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.OrganizationPeople.Filters;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationRecruiters;
using AgencyPro.Core.Organizations.RecruitingOrganizations.ViewModels;
using AgencyPro.Core.Services;

namespace AgencyPro.Core.OrganizationRoles.Services
{
    public interface IOrganizationRecruiterService
        : IService<OrganizationRecruiter>,
            IOrganizationRoleService<OrganizationRecruiterInput, OrganizationRecruiterOutput,
                IOrganizationRecruiter, RecruiterFilters, RecruiterOrganizationOutput, RecruiterCounts>
    {
      
    }
}