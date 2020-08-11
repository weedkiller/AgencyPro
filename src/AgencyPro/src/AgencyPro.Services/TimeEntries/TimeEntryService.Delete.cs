// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.TimeEntries.Enums;
using AgencyPro.Core.TimeEntries.Models;
using AgencyPro.Core.TimeEntries.ViewModels;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.TimeEntries
{
    public partial class TimeEntryService
    {
        public Task<TimeEntryResult> DeleteTimeEntry(IOrganizationContractor co, Guid entryId)
        {
            var entry = Repository
                .FirstOrDefault(x => x.Id == entryId &&
                                     x.ContractorId == co.ContractorId &&
                                     x.ProviderOrganizationId == co.OrganizationId);

            return DeleteTimeEntry(entry);
        }

        public Task<TimeEntryResult> DeleteTimeEntry(IOrganizationProjectManager pm, Guid entryId)
        {
            var entry = Repository
                .FirstOrDefault(x => x.Id == entryId &&
                                     x.ProjectManagerId == pm.ProjectManagerId &&
                                     x.ProviderOrganizationId == pm.OrganizationId);

            return DeleteTimeEntry(entry);
        }

        public Task<TimeEntryResult> DeleteTimeEntry(IAgencyOwner ao, Guid entryId)
        {
            var entry = Repository
                .FirstOrDefault(x => x.Id == entryId &&
                                     x.ProviderOrganizationId == ao.OrganizationId);

            return DeleteTimeEntry(entry);
        }

        private async Task<TimeEntryResult> DeleteTimeEntry(TimeEntry entry)
        {
            _logger.LogInformation(GetLogMessage("Deleting time entry: {0}"), entry.Id);

            var retVal = new TimeEntryResult
            {
                TimeEntryId = entry.Id
            };

            if (entry.Status == TimeStatus.Logged)
            {
                entry.IsDeleted = true;
                entry.UpdatedById = _userInfo.UserId;
                entry.Updated = DateTimeOffset.UtcNow;

                var result = await Repository.UpdateAsync(entry, true);

                _logger.LogDebug(GetLogMessage("{0} results updated"), result);
                if (result > 0)
                {
                    retVal.Succeeded = true;
                }

            }
            else
            {
                retVal.ErrorMessage = "You cannot delete a time entry that has been approved or invoiced";
            }


            return retVal;
        }
    }
}