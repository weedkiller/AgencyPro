// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Stories.Events;
using AgencyPro.Core.Stories.Extensions;
using AgencyPro.Core.Stories.Models;
using AgencyPro.Core.Stories.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Projects.Enums;
using AgencyPro.Core.Stories.Enums;

namespace AgencyPro.Services.Stories
{
    public partial class StoryService
    {
        private async Task<StoryResult> AssignStory(Story story, AssignStoryInput input)
        {
            _logger.LogInformation(GetLogMessage("Story: {0}"), story.Id);

            var result = new StoryResult {StoryId = story.Id};
            
            var originalContractorId = story.ContractorId;

            story.ContractorId = input.ContractorId;
            story.AssignedDateTime = DateTimeOffset.UtcNow;
           
            story.Updated = DateTimeOffset.UtcNow;
            story.ObjectState = ObjectState.Modified;
            if (input.ContractorId.HasValue)
            {
                _logger.LogDebug(GetLogMessage("assign"));

                story.Status = StoryStatus.Assigned;
               
            }
            else
            {
                _logger.LogDebug(GetLogMessage("unassign"));


                switch (story.Project.Status)
                {
                    case ProjectStatus.Pending:
                        story.Status = StoryStatus.Pending;
                        break;
                    case ProjectStatus.Active:
                        story.Status = StoryStatus.Approved;
                        break;
                    case ProjectStatus.Paused:
                        story.Status = StoryStatus.Pending;
                        break;
                    case ProjectStatus.Ended:
                        story.Status = StoryStatus.Archived;
                        break;
                    case ProjectStatus.Inactive:
                        story.Status = StoryStatus.Pending;
                        break;
                }
            }
            story.StatusTransitions.Add(new StoryStatusTransition()
            {
                Status = story.Status,
                ObjectState = ObjectState.Added
            });


            var records = await Repository.UpdateAsync(story, true);
            _logger.LogDebug(GetLogMessage("{0} Records Updated"), records);

            if (records > 0)
            {
                result.Succeeded = true;

                if (originalContractorId != story.ContractorId && story.ContractorId.HasValue)
                {
                    await Task.Run(() => RaiseEvent(new StoryAssignedEvent()
                    {
                        StoryId = story.Id
                    }));
                }
            }

            return result;
        }

        public async Task<StoryResult> AssignStory(IOrganizationProjectManager pm, Guid storyId, AssignStoryInput input) 
        {
            _logger.LogInformation(GetLogMessage("Project Manager {0}; Assigning Story: {1}"), pm.OrganizationId, storyId);

            var story = await Repository
                .Queryable()
                .Include(x=>x.Project)
                .ForOrganizationProjectManager(pm)
                .Where(x => x.Id == storyId)
                .FirstOrDefaultAsync();
            story.ContractorOrganizationId = input.ContractorId != null ? pm.OrganizationId : (Guid?)null;


            return await AssignStory(story, input);
        }

        public async Task<StoryResult> AssignStory(IProviderAgencyOwner ao, Guid storyId, AssignStoryInput input)
        {
            _logger.LogInformation(GetLogMessage("PAO {0}; Assigning Story: {1}"), ao.OrganizationId, storyId);

            var story = await Repository
                .Queryable()
                .Include(x=>x.Project)
                .ForAgencyOwner(ao)
                .Where(x => x.Id == storyId)
                .FirstOrDefaultAsync();

            story.ContractorOrganizationId = input.ContractorId != null ? ao.OrganizationId : (Guid?)null;


            return await AssignStory(story, input);

        }
    }
}