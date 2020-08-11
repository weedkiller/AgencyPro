// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.TimeEntries.Enums;
using AgencyPro.Core.TimeEntries.Events;
using AgencyPro.Core.TimeEntries.Extensions;
using AgencyPro.Core.TimeEntries.Models;
using AgencyPro.Core.TimeEntries.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.TimeEntries
{
    public partial class TimeEntryService
    {
        private async Task<TimeEntryResult> Approve(List<TimeEntry> timeEntries)
        {
            _logger.LogInformation(GetLogMessage("Approving {0} entries"), timeEntries.Count);

            var retVal = new TimeEntryResult();

            foreach (var e in timeEntries)
            {
                e.Status = TimeStatus.Approved;
                e.Updated = DateTimeOffset.UtcNow;
                e.UpdatedById = _userInfo.UserId;
                e.ObjectState = ObjectState.Modified;
                e.StatusTransitions.Add(new TimeEntryStatusTransition()
                {
                    Status = TimeStatus.Approved,
                    ObjectState = ObjectState.Added
                });

                Repository.Update(e);

                
            }

            var result = Repository.Commit();
            
            _logger.LogDebug(GetLogMessage("{0} records updated"), result);

            if (result > 0)
            {
                retVal.Succeeded = true;
                retVal.TimeEntryIds = timeEntries.Select(x => x.Id).ToArray();

                foreach (var e in timeEntries)
                {
                    await Task.Run(() =>
                    {
                        RaiseEvent(new TimeEntryApprovedEvent()
                        {
                            TimeEntryId = e.Id
                        });
                    });
                }
            }

            return retVal;
        }

        public async Task<TimeEntryResult> Approve(IOrganizationProjectManager pm, Guid[] entries)
        {
            var entriesToApprove = await Repository.Queryable()
                .ForOrganizationProjectManager(pm)
                .Where(x => entries.Contains(x.Id)).ToListAsync();

            return await Approve(entriesToApprove);
        }

        public async Task<TimeEntryResult> Approve(IAgencyOwner ao, Guid[] entries)
        {
            var entriesToApprove = await Repository.Queryable()
                .ForAgencyOwner(ao)
                .Where(x => entries.Contains(x.Id)).ToListAsync();

            return await Approve(entriesToApprove);
        }

        public async Task<TimeEntryResult> SaveAndApprove(IAgencyOwner ao, Guid entryId, TimeEntryInput input)
        {
            var entry = await UpdateTimeEntry(ao, entryId, input);
            
            return await Approve(ao, new[] { entryId });
        }

        public async Task<TimeEntryResult> SaveAndApprove(IOrganizationProjectManager pm, Guid entryId, TimeEntryInput input)
        {
            var entry = await UpdateTimeEntry(pm, entryId, input);
            var entry2 = await Approve(pm, new[] { entryId });

            return entry2;
        }
    }
}