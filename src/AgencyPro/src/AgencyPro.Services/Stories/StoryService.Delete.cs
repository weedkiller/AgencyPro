// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Stories.Events;
using AgencyPro.Core.Stories.ViewModels;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Stories
{
    public partial class StoryService
    {
        public Task<StoryResult> DeleteStory(IOrganizationProjectManager pm, Guid storyId
        )
        {
            _logger.LogInformation(GetLogMessage("Story: {0}"), storyId);

            return DeleteStoryInternal(storyId);
        }

        public Task<StoryResult> DeleteStory(IOrganizationContractor co, Guid storyId
        )
        {
            _logger.LogInformation(GetLogMessage("Story: {0}"), storyId);

            return DeleteStoryInternal(storyId);
        }

        public Task<StoryResult> DeleteStory(IProviderAgencyOwner agencyOwner, Guid storyId)
        {
            _logger.LogInformation(GetLogMessage("Story: {0}"), storyId);

            return DeleteStoryInternal(storyId);
        }

        private async Task<StoryResult> DeleteStoryInternal(Guid storyId)
        {
            _logger.LogInformation(GetLogMessage("Story: {0}"), storyId);
            var retVal = new StoryResult {StoryId = storyId};

            var entity = await Repository.FirstOrDefaultAsync(x => x.Id == storyId);
            entity.IsDeleted = true;
            entity.Updated = DateTimeOffset.UtcNow;

            var result = await Repository.UpdateAsync(entity, true);

            _logger.LogDebug(GetLogMessage("{0} results updated"), result);

            if (result > 0)
            {
                retVal.Succeeded = true;
                await Task.Run(() =>
                {
                    RaiseEvent(new StoryDeletedEvent
                    {
                        StoryId = storyId
                    });
                });
            }

            return retVal;
        }
    }
}