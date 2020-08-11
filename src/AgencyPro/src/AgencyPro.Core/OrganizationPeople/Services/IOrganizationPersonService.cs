// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Common;
using AgencyPro.Core.OrganizationPeople.Models;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Services;

namespace AgencyPro.Core.OrganizationPeople.Services
{
    public interface IOrganizationPersonService : IService<OrganizationPerson>
    {
        Task<T> GetOrganizationPerson<T>(Guid personId, Guid organizationId) where T : OrganizationPersonOutput;
        Task<OrganizationPersonOutput> Get(Guid personId, Guid organizationId);
        Task<IOrganizationPerson> GetPrincipal(Guid personId, Guid organizationId);

        Task<PackedList<T>> GetPeople<T>(IAgencyOwner ao, OrganizationPeopleFilters filters)
            where T : AgencyOwnerOrganizationPersonOutput;

        Task<PackedList<T>> GetPeople<T>(IOrganizationAccountManager am, OrganizationPeopleFilters filters)
            where T : AccountManagerOrganizationPersonOutput;

        Task<OrganizationPersonResult> Remove(IAgencyOwner input, Guid personId);

        Task<OrganizationPersonResult> Create(IAgencyOwner ao, CreateOrganizationPersonInput input);
        Task<OrganizationPersonResult> Create(IOrganizationAccountManager am, CreateOrganizationPersonInput input);

        Task<OrganizationPersonResult> Create(OrganizationPersonInput input, Guid organizationId);
        Task<OrganizationPersonResult> Create(CreateOrganizationPersonInput input, Guid organizationId, Guid? affiliateOrganizationId, bool checkValidation = true);

        Task<OrganizationPersonResult> AddExistingPerson(IAgencyOwner agencyOwner, AddExistingPersonInput input);

        Task<OrganizationPersonResult> HideOrganization(IOrganizationPerson person);
        Task<OrganizationPersonResult> ShowOrganization(IOrganizationPerson person);

        Task<OrganizationPersonOutput> GetPersonByCode(string code);
    }
}