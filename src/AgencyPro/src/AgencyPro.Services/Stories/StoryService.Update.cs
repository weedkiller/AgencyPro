// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Stories.Events;
using AgencyPro.Core.Stories.ViewModels;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;
using System;
using System.Threading.Tasks;

namespace AgencyPro.Services.Stories
{
    public partial class StoryService
    {
        public Task<StoryResult> UpdateStory(IOrganizationAccountManager am,
            Guid storyId,
            UpdateStoryInput model)
        {
            return UpdateStoryInternal(storyId, model);
        }

        public Task<StoryResult> UpdateStory(IProviderAgencyOwner agencyOwner,
            Guid storyId,
            UpdateStoryInput model)
        {
            return UpdateStoryInternal(storyId, model);
        }

        public Task<StoryResult> UpdateStory(IOrganizationContractor co,
            Guid storyId,
            UpdateStoryInput model)
        {
            return UpdateStoryInternal(storyId, model);
        }

        public Task<StoryResult> UpdateStory(IOrganizationProjectManager pm,
            Guid storyId,
            UpdateStoryInput model)
        {
            return UpdateStoryInternal(storyId, model);
        }

        private async Task<StoryResult> UpdateStoryInternal(Guid storyId, UpdateStoryInput input)
        {
            var retVal = new StoryResult {StoryId = storyId};

            var entity = await Repository
                .FirstOrDefaultAsync(x => x.Id == storyId);

            entity.InjectFrom(input);

            entity.Updated = DateTimeOffset.UtcNow;
            entity.ObjectState = ObjectState.Modified;

            var records = await Repository.UpdateAsync(entity, true);

            _logger.LogDebug(GetLogMessage("{0} Records updated"), records);


            if (records > 0)
            {
                retVal.Succeeded = true;
                await Task.Run(() =>
                {
                    RaiseEvent(new StoryUpdatedEvent
                    {
                        StoryId = storyId
                    });
                });
            }

            return retVal;
        }
    }
}