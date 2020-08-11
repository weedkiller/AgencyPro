// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationPeople.Filters;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationProjectManagers;
using AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels;
using AgencyPro.Core.Services;

namespace AgencyPro.Core.OrganizationRoles.Services
{
    public interface IOrganizationProjectManagerService : IService<OrganizationProjectManager>,
        IOrganizationRoleService<OrganizationProjectManagerInput, OrganizationProjectManagerOutput,
            IOrganizationProjectManager, ProjectManagerFilters, ProjectManagerOrganizationOutput, ProjectManagerCounts>
    {
        Task<T> GetProjectManagerForProject<T>(Guid projectId) where T : OrganizationProjectManagerOutput;
    }
}