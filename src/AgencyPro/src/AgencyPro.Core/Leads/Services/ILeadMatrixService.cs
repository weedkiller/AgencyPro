// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Leads.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;

namespace AgencyPro.Core.Leads.Services
{
    public interface ILeadMatrixService
    {
        Task<List<T>> GetResults<T>(IOrganizationMarketer ma, LeadMatrixFilters filters) where T : MarketerLeadMatrixOutput;
        Task<List<T>> GetResults<T>(IAgencyOwner ao, LeadMatrixFilters filters) where T : AgencyOwnerLeadMatrixOutput;

    }
}