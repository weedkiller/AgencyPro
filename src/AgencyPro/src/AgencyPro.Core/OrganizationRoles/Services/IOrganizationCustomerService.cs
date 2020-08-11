// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationPeople.Filters;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationCustomers;
using AgencyPro.Core.Organizations.ViewModels;
using AgencyPro.Core.Services;

namespace AgencyPro.Core.OrganizationRoles.Services
{
    public interface IOrganizationCustomerService : IService<OrganizationCustomer>,
        IOrganizationRoleService<OrganizationCustomerInput,
            OrganizationCustomerOutput, IOrganizationCustomer, CustomerFilters, CustomerOrganizationOutput, CustomerCounts>
    {
        Task<IAgencyOwner> GetAgencyOwner(Guid personId, Guid organizationId);

        Task<List<T>> GetCustomers<T>(IAgencyOwner agencyOwner) where T : OrganizationCustomerOutput;

        Task<T> GetCustomerForProject<T>(Guid projectId)
            where T : OrganizationCustomerOutput;

        Task<List<T>> GetCustomers<T>(IOrganizationAccountManager organizationAccountManager)
            where T: OrganizationCustomerOutput;

        Task<List<T>> GetCustomers<T>(IOrganizationProjectManager projectManager)
            where T : OrganizationCustomerOutput;
        
    }
}