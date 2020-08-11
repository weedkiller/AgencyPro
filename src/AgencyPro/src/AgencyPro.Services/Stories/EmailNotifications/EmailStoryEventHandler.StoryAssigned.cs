// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.EmailSending.Services;
using AgencyPro.Core.Stories.Emails;
using AgencyPro.Core.Stories.Events;
using AgencyPro.Core.Templates.Models;
using AgencyPro.Data.EFCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Stories.EmailNotifications
{
    public partial class StoryEventHandlers
    {
        private void StoryAssignedSendAccountManagerEmail(Guid storyId)
        {
            _logger.LogInformation(GetLogMessage("Story: {0}"), storyId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var story = context.Stories
                    .Where(x => x.Id == storyId)
                    .ProjectTo<AccountManagerStoryEmail>(ProjectionMapping)
                    .First();


                story.Initialize(Settings);

                Send(TemplateTypes.AccountManagerStoryAssigned, story,
                    $@"[{story.ProjectManagerOrganizationName}] A story has been assigned");
            }
           
        }

        private void StoryAssignedSendProjectManagerEmail(Guid storyId)
        {
            _logger.LogInformation(GetLogMessage("Story: {0}"), storyId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var story = context.Stories
                    .Where(x => x.Id == storyId)
                    .ProjectTo<ProjectManagerStoryEmail>(ProjectionMapping)
                    .First();


                story.Initialize(Settings);

                Send(TemplateTypes.ProjectManagerStoryAssigned, story,
                    $@"[{story.ProjectManagerOrganizationName}] A story has been assigned");
            }

            
        }

        private void StoryAssignedSendContractorEmail(Guid storyId)
        {
            _logger.LogInformation(GetLogMessage("Story: {0}"), storyId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var story = context.Stories
                    .Where(x => x.Id == storyId)
                    .ProjectTo<ContractorStoryEmail>(ProjectionMapping)
                    .First();


                story.Initialize(Settings);

                Send(TemplateTypes.ContractorStoryAssigned, story,
                    $@"[{story.ProjectManagerOrganizationName}] A story has been assigned");
            }


           
        }

        private void StoryAssignedSendAgencyOwnerEmail(Guid storyId)
        {
            _logger.LogInformation(GetLogMessage("Story: {0}"), storyId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var story = context.Stories
                    .Where(x => x.Id == storyId)
                    .ProjectTo<AgencyOwnerStoryEmail>(ProjectionMapping)
                    .First();


                story.Initialize(Settings);

                Send(TemplateTypes.AgencyOwnerStoryAssigned, story,
                    $@"[{story.ProjectManagerOrganizationName}] A story has been assigned");
            }


            
        }

        public void Handle(StoryAssignedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Story Assigned Event Triggered"));
            
            Parallel.Invoke(new List<Action>
            {
                () => StoryAssignedSendAccountManagerEmail(evt.StoryId),
                () => StoryAssignedSendProjectManagerEmail(evt.StoryId),
                () => StoryAssignedSendContractorEmail(evt.StoryId),
                () => StoryAssignedSendAgencyOwnerEmail(evt.StoryId)
            }.ToArray());
        }
    }
}