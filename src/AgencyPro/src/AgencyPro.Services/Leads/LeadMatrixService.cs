// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Leads.Services;
using AgencyPro.Core.Leads.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;

namespace AgencyPro.Services.Leads
{
    public class LeadMatrixService : ILeadMatrixService
    {
        public async Task<List<T>> GetResults<T>(IOrganizationMarketer ma, LeadMatrixFilters filters) where T : MarketerLeadMatrixOutput
        {
           return await Task.FromResult(new List<T>());
        }

        public Task<List<T>> GetResults<T>(IAgencyOwner ao, LeadMatrixFilters filters) where T : AgencyOwnerLeadMatrixOutput
        {
            return Task.FromResult(new List<T>());
        }
    }
}