// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationPeople.Filters;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationAccountManagers;
using AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels;
using AgencyPro.Core.Services;

namespace AgencyPro.Core.OrganizationRoles.Services
{
    public interface IOrganizationAccountManagerService : IService<OrganizationAccountManager>,
        IOrganizationRoleService<OrganizationAccountManagerInput, OrganizationAccountManagerOutput,
            IOrganizationAccountManager, AccountManagerFilters, AccountManagerOrganizationOutput, AccountManagerCounts>
    {
     
        Task<T> GetAccountManagerForProject<T>(Guid projectId) 
            where T : OrganizationAccountManagerOutput;

        Task<T> GetAccountManagerOrDefault<T>(Guid organizationId, Guid? accountManager)
            where T : OrganizationAccountManagerOutput;
    }
}