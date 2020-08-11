// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Stories.Enums;
using AgencyPro.Core.Stories.Models;
using AgencyPro.Core.TimeEntries.Enums;
using AgencyPro.Core.TimeEntries.Models;
using AgencyPro.Core.TimeEntries.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace AgencyPro.Services.TimeEntries
{
    public partial class TimeEntryService
    {
        public async Task<TimeEntryResult> UpdateTimeEntry(IOrganizationContractor co, Guid entryId, TimeEntryInput input)
        {
            _logger.LogInformation(GetLogMessage("Entry: {0}"), entryId);

            var entry = await Repository
                .FirstOrDefaultAsync(x => x.Id == entryId);

            var retVal = new TimeEntryResult {TimeEntryId = entryId};

            if (entry.Status == TimeStatus.Logged|| entry.Status == TimeStatus.Rejected)
            {
                if (input.MinutesDuration.HasValue && input.MinutesDuration.Value > 0)
                    input.EndDate = input.StartDate.AddMinutes(input.MinutesDuration.Value);

                if (entry.Status == TimeStatus.Rejected)
                {
                    entry.Status = TimeStatus.Logged;
                    entry.StatusTransitions.Add(new TimeEntryStatusTransition()
                    {
                        Status = TimeStatus.Logged,
                        ObjectState = ObjectState.Added
                    });
                }


                entry.InjectFrom(input);

                entry.ObjectState = ObjectState.Modified;
                entry.Updated = DateTimeOffset.UtcNow;

                var storyId = input.StoryId;
                var isStoryComplete = input.CompleteStory;

                if (!input.StoryId.HasValue) return await UpdateTimeEntry(entry);

                var story = await _storyService.Repository
                    .Queryable()
                    .FirstOrDefaultAsync(x => x.Id == storyId.Value);

                if (isStoryComplete.HasValue && isStoryComplete is true)
                {
                    story.Status = StoryStatus.Completed;
                    story.StatusTransitions.Add(new StoryStatusTransition()
                    {
                        Status = StoryStatus.Completed
                    });
                    //else if (isStoryComplete is false) story.Status = StoryStatus.InProgress;
                }

                story.ObjectState = ObjectState.Modified;
                story.Updated = DateTimeOffset.UtcNow;

                var result = await _storyService.Repository.UpdateAsync(story, true);

                return await UpdateTimeEntry(entry);
            }

            return retVal;
        }

        public async Task<TimeEntryResult> UpdateTimeEntry(IOrganizationProjectManager co, Guid entryId, TimeEntryInput input)
        {
            _logger.LogInformation(GetLogMessage("Entry: {0}"), entryId);
            var retVal = new TimeEntryResult()
            {
                TimeEntryId = entryId
            };

            var entry = await Repository.FirstOrDefaultAsync(x => x.Id == entryId);
            if (entry.Status == TimeStatus.Logged)
            {
                if (input.MinutesDuration.HasValue && input.MinutesDuration.Value > 0)
                    input.EndDate = input.StartDate.AddMinutes(input.MinutesDuration.Value);
               
                entry.InjectFrom(input) ;

                retVal = await UpdateTimeEntry(entry);
            }
            else
            {
                retVal.ErrorMessage = "You can only update time entry in the logged state";
            }

            return retVal;
        }

        public async Task<TimeEntryResult> UpdateTimeEntry(IAgencyOwner ao, Guid entryId, TimeEntryInput input)
        {
            _logger.LogInformation(GetLogMessage("Entry: {0}"), entryId);

            var entry = await Repository.FirstOrDefaultAsync(x => x.Id == entryId);
            if (input.MinutesDuration.HasValue && input.MinutesDuration.Value > 0)
                input.EndDate = input.StartDate.AddMinutes(input.MinutesDuration.Value);

            entry.InjectFrom(input);

            return await UpdateTimeEntry(entry);
        }

        private async Task<TimeEntryResult> UpdateTimeEntry(TimeEntry entry)
        {
            _logger.LogInformation(GetLogMessage("Entry: {0}"), entry.Id);

            var retVal = new TimeEntryResult();

            entry.Updated = DateTimeOffset.UtcNow;
            entry.UpdatedById = _userInfo.UserId;

            var result = await Repository.UpdateAsync(entry, true);
            _logger.LogDebug(GetLogMessage("{0} results updated"), result);

            if (result > 0)
            {
                retVal.Succeeded = true;
                retVal.TimeEntryId = entry.Id;
            }

            return retVal;
        }
    }
}