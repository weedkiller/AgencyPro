// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Common;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Services;
using AgencyPro.Core.TimeEntries.Models;
using AgencyPro.Core.TimeEntries.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgencyPro.Core.TimeEntries.Services
{
    public interface ITimeEntryService : IService<TimeEntry>
    {
        Task<TimeEntryResult> TrackTime(IOrganizationContractor contractor, TimeTrackingModel model);

        Task<TimeEntryResult> TrackDay(IOrganizationContractor contractor, DayTimeTrackingModel model);
        Task<TimeEntryResult> TrackHalfDay(IOrganizationContractor contractor, DayTimeTrackingModel model);


        Task<TimeEntryResult> Approve(IOrganizationProjectManager pm, Guid[] entries);
        Task<TimeEntryResult> Approve(IAgencyOwner ao, Guid[] entries);
        Task<TimeEntryResult> SaveAndApprove(IAgencyOwner ao, Guid entryId, TimeEntryInput input);
        Task<TimeEntryResult> SaveAndApprove(IOrganizationProjectManager pm, Guid entryId, TimeEntryInput input);

        Task<TimeEntryResult> Reject(IOrganizationProjectManager pm, Guid[] entries);
        Task<TimeEntryResult> Reject(IAgencyOwner ao, Guid[] entries);

        Task<PackedList<T>> GetTimeEntries<T>(IOrganizationContractor co, TimeMatrixFilters filters
        ) where T : ContractorTimeEntryOutput;

        Task<PackedList<T>> GetTimeEntries<T>(IOrganizationProjectManager co, TimeMatrixFilters filters
        ) where T : ProjectManagerTimeEntryOutput;

        Task<PackedList<T>> GetTimeEntries<T>(IOrganizationAccountManager am, TimeMatrixFilters filters
        ) where T : AccountManagerTimeEntryOutput;

        Task<List<T>> GetTimeEntries<T>(IOrganizationCustomer cu, TimeMatrixFilters filters
        ) where T : CustomerTimeEntryOutput;

        Task<PackedList<T>> GetTimeEntries<T>(IAgencyOwner ao, TimeMatrixFilters filters
        ) where T : ProviderAgencyOwnerTimeEntryOutput;

        Task<List<T>> GetTimeEntries<T>(IOrganizationMarketer ma, TimeMatrixFilters filters
        ) where T : MarketerTimeEntryOutput;

        Task<List<T>> GetTimeEntries<T>(IOrganizationRecruiter re, TimeMatrixFilters filters
        ) where T : RecruiterTimeEntryOutput;


        Task<T> GetTimeEntry<T>(IOrganizationContractor contractor, Guid entryId) where T : ContractorTimeEntryOutput;
        Task<T> GetTimeEntry<T>(IOrganizationCustomer customer, Guid entryId) where T : CustomerTimeEntryOutput;
        Task<T> GetTimeEntry<T>(IOrganizationAccountManager am, Guid entryId) where T : AccountManagerTimeEntryOutput;
        Task<T> GetTimeEntry<T>(IAgencyOwner ao, Guid entryId) where T : ProviderAgencyOwnerTimeEntryOutput;
        Task<T> GetTimeEntry<T>(IOrganizationProjectManager pm, Guid entryId) where T : ProjectManagerTimeEntryOutput;
        Task<T> GetTimeEntry<T>(IOrganizationRecruiter re, Guid entryId) where T : RecruiterTimeEntryOutput;
        Task<T> GetTimeEntry<T>(IOrganizationMarketer ma, Guid entryId) where T : MarketerTimeEntryOutput;

        Task<TimeEntryResult> UpdateTimeEntry(IOrganizationContractor co, Guid entryId, TimeEntryInput input);
        Task<TimeEntryResult> UpdateTimeEntry(IOrganizationProjectManager co, Guid entryId, TimeEntryInput input);
        Task<TimeEntryResult> UpdateTimeEntry(IAgencyOwner ao, Guid entryId, TimeEntryInput input);

        Task<TimeEntryResult> DeleteTimeEntry(IOrganizationContractor co, Guid entryId);
        Task<TimeEntryResult> DeleteTimeEntry(IOrganizationProjectManager pm, Guid entryId);
        Task<TimeEntryResult> DeleteTimeEntry(IAgencyOwner ao, Guid entryId);
    }
}