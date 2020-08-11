// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationPeople.Filters;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationMarketers;
using AgencyPro.Core.Organizations.MarketingOrganizations.ViewModels;
using AgencyPro.Core.Services;

namespace AgencyPro.Core.OrganizationRoles.Services
{
    public interface IOrganizationMarketerService : IService<OrganizationMarketer>,
        IOrganizationRoleService<OrganizationMarketerInput, 
            OrganizationMarketerOutput, IOrganizationMarketer, MarketerFilters, MarketerOrganizationOutput, MarketerCounts>
    {
       
        Task<T> GetMarketerForProject<T>(Guid inputProjectId)   
            where T : OrganizationMarketerOutput;
        

        Task<T> GetMarketerOrDefault<T>(Guid? organizationId, Guid? marketerId, string referralCode)
            where T : OrganizationMarketerOutput;
    }
}