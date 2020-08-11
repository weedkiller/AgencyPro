// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.TimeEntries.ViewModels;

namespace AgencyPro.Services.TimeEntries
{
    public partial class TimeEntryService
    {
        public Task<List<TimeEntryOutput>> GetTimeEntriesForDashboard(IOrganizationAccountManager am
        )
        {
            throw new NotImplementedException();
        }

        public Task<List<TimeEntryOutput>> GetTimeEntriesForDashboard(IOrganizationProjectManager co)
        {
            throw new NotImplementedException();
        }

        public Task<List<TimeEntryOutput>> GetTimeEntriesForDashboard(IOrganizationContractor co)
        {
            throw new NotImplementedException();
        }

        public Task<List<TimeEntryOutput>> GetTimeEntriesForDashboard(IOrganizationRecruiter re)
        {
            throw new NotImplementedException();
        }

        public Task<List<TimeEntryOutput>> GetTimeEntriesForDashboard(IOrganizationMarketer ma)
        {
            throw new NotImplementedException();
        }

        public Task<List<TimeEntryOutput>> GetTimeEntriesForDashboard(IOrganizationCustomer cu)
        {
            throw new NotImplementedException();
        }

        public Task<List<TimeEntryOutput>> GetAgencyTimeEntriesForDashboard(IAgencyOwner ao)
        {
            throw new NotImplementedException();
        }
    }
}