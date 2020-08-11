// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.TimeEntries.Enums;
using AgencyPro.Core.TimeEntries.Extensions;
using AgencyPro.Core.TimeEntries.Models;
using AgencyPro.Core.TimeEntries.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.TimeEntries
{
    public partial class TimeEntryService
    {
        public Task<TimeEntryResult> Reject(List<TimeEntry> entries)
        {
            var retVal = new TimeEntryResult
            {
                TimeEntryIds = entries.Select(x=>x.Id).ToArray()
            };
            
            foreach (var e in entries)
            {
                _logger.LogDebug(GetLogMessage("rejecting entry: {0}"), e.Id);

                e.Status = TimeStatus.Rejected;
                e.Updated = DateTimeOffset.UtcNow;
                e.UpdatedById = _userInfo.UserId;

                e.StatusTransitions.Add(new TimeEntryStatusTransition()
                {
                    Status = TimeStatus.Rejected,
                    ObjectState = ObjectState.Added
                });

                Repository.Update(e);
            }

            var result = Repository.Commit();

            _logger.LogDebug(GetLogMessage("{0} records updated in db"), result);

            if (result > 0)
            {
                retVal.Succeeded = true;
            }

            return Task.FromResult(retVal);
        }

        public async Task<TimeEntryResult> Reject(IAgencyOwner ao, Guid[] entries)
        {

            _logger.LogInformation(GetLogMessage("AO: {0}; Rejecting {1} entries"), ao.OrganizationId, entries.Length);

            var entriesToReject = await Repository.Queryable().ForAgencyOwner(ao)
                .Where(x => entries.Contains(x.Id)).ToListAsync();

            return await Reject(entriesToReject);
        }

        public async Task<TimeEntryResult> Reject(IOrganizationProjectManager pm, Guid[] entries)
        {
            _logger.LogInformation(GetLogMessage("PM: {0}; Rejecting {1} entries"), pm.OrganizationId, entries.Length);

            var entriesToReject = await Repository
                .Queryable()
                .ForOrganizationProjectManager(pm)
                .Where(x => entries.Contains(x.Id))
                .ToListAsync();

            return await Reject(entriesToReject);
        }
    }
}