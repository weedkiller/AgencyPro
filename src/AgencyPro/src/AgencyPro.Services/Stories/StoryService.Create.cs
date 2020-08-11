// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Projects.Enums;
using AgencyPro.Core.Stories.Enums;
using AgencyPro.Core.Stories.Events;
using AgencyPro.Core.Stories.Models;
using AgencyPro.Core.Stories.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace AgencyPro.Services.Stories
{
    public partial class StoryService
    {
        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(StoryService)}.{callerName}] - {message}";
        }

        public Task<StoryResult> CreateStory(
            IProviderAgencyOwner agencyOwner,
            CreateStoryInput input
        )
        {
            return CreateStoryInternal(input, agencyOwner.OrganizationId);
        }

        public Task<StoryResult> CreateStory(
            IOrganizationAccountManager organizationAccountManager,
            CreateStoryInput input
        )
        {
            
            return CreateStoryInternal(input, organizationAccountManager.OrganizationId);
        }

        public async Task<StoryResult> CreateStory(
            IOrganizationProjectManager pm,
            CreateStoryInput input
        )
        {
            return await CreateStoryInternal(input, pm.OrganizationId);
        }

        private async Task<StoryResult> CreateStoryInternal(CreateStoryInput input, Guid organizationId)
        {
            _logger.LogInformation(GetLogMessage("Org: {0}"),organizationId);

            var retVal = new StoryResult();

            var project = _projectService.Repository.Queryable()
                .Include(x=>x.Proposal)
                .Where(x => x.Id == input.ProjectId && x.ProjectManagerOrganizationId == organizationId)
                .FirstOrDefaultAsync();


            var existingStoryCount = Repository.Queryable()
                .CountAsync(x => x.ProjectId == input.ProjectId);
            
            await Task.WhenAll(project, existingStoryCount);

            if (project.Result == null)
            {
                retVal.ErrorMessage = "No project found";
                return retVal;
            }

            _logger.LogDebug(GetLogMessage("Project: {0}; Status: {1}"), project.Result.Id, project.Result.Status);

            var story = new Story
            {
                Status = input.ContractorId.HasValue ? StoryStatus.Assigned : StoryStatus.Pending,
                StoryId = project.Result.Abbreviation + "-" + (existingStoryCount.Result + 1),
                ObjectState = ObjectState.Added,
                StoryTemplateId = input.TemplateId,
                StoryPoints = input.StoryPoints,
                ContractorId = input.ContractorId,
                ContractorOrganizationId = input.ContractorId.HasValue ? organizationId : (Guid?) null
            };

            switch (project.Result.Status)
            {
                case ProjectStatus.Active:

                    story.CustomerAcceptanceDate = DateTimeOffset.UtcNow;
                    story.Updated = DateTimeOffset.UtcNow;
                    story.ObjectState = ObjectState.Modified;
                    story.Status = StoryStatus.Approved;
                    story.CustomerApprovedHours = story.StoryPoints * project.Result.Proposal.EstimationBasis;

                    break;
                
            }

            story.InjectFrom(input);

            story.StatusTransitions.Add(new StoryStatusTransition()
            {
                Status = story.Status, 
                ObjectState = ObjectState.Added
            });

            
            var storyRecords = Repository.Insert(story, true);

            _logger.LogDebug(GetLogMessage("{0} records updated in db."), storyRecords);

            if (storyRecords > 0)
            {
                retVal.StoryId = story.Id;
                retVal.Succeeded = true;

                await Task.Run(() =>
                {
                    if (story.Status == StoryStatus.Pending)
                    {
                        RaiseEvent(new StoryCreatedEvent
                        {
                            StoryId = story.Id
                        });
                    }

                    if (story.ContractorId.HasValue)
                    {
                        RaiseEvent(new StoryAssignedEvent()
                        {
                            StoryId = story.Id
                        });
                    }
                    
                    if (story.Status == StoryStatus.Approved)
                    {
                        RaiseEvent(new StoryApprovedEvent()
                        {
                            StoryId = story.Id
                        });
                    }
                });
            }

            return retVal;
        }
    }
}