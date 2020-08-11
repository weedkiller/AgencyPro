// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Common;
using AgencyPro.Core.OrganizationPeople.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgencyPro.Core.OrganizationRoles.Services
{
    public interface IOrganizationRoleService<in TInput, TOutput, TPrincipal, in TFilters, in TOrganizationOutput, TCount>
    {
        Task<TPrincipal> GetPrincipal(Guid personId, Guid organizationId);
        Task<TOutput> Get(Guid personId, Guid organizationId);
        Task<T> GetById<T>(Guid personId, Guid organizationId) where T : TOutput;
        Task<T> GetById<T>(TInput input) where T : TOutput;
        Task<List<T>> GetForOrganization<T>(Guid organizationId, TFilters filters) where T : TOutput;
        Task<PackedList<T>> GetForOrganization<T>(Guid organizationId, TFilters filters, CommonFilters pagingFilters) where T : TOutput;
        Task<List<T>> GetForOrganization<T>(Guid organization, Guid[] personIds) where T : TOutput;
        Task<bool> Remove(IAgencyOwner ao, Guid personId, bool commit = true);
        Task<T> Create<T>(TInput model) where T : TOutput;
        Task<T> Create<T>(IAgencyOwner ao, TInput input) where T : TOutput;
        Task<T> Update<T>(IAgencyOwner ao, TInput input) where T : TOutput;
        Task<T> Update<T>(IOrganizationAccountManager am, TInput input) where T : TOutput;
        Task<T> GetOrganization<T>(TPrincipal principal) where T : TOrganizationOutput;
        Task<TCount> GetCounts(TPrincipal principal);

    }
}