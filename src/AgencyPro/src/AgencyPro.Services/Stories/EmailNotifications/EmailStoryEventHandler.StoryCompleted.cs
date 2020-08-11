// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
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

        private void StoryCompletedSendContractorEmail(Guid storyId)
        {
            _logger.LogInformation(GetLogMessage("Story: {0}"), storyId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var story = context.Stories
                    .Where(x => x.Id == storyId)
                    .ProjectTo<ContractorStoryEmail>(ProjectionMapping)
                    .First();


                story.Initialize(Settings);

                Send(TemplateTypes.ContractorStoryCompleted, story,
                    $@"[{story.ProjectManagerOrganizationName}] A story has been completed");
            }
            
        }

        private void StoryCompletedSendProjectManagerEmail(Guid storyId)
        {
            _logger.LogInformation(GetLogMessage("Story: {0}"), storyId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var story = context.Stories
                    .Where(x => x.Id == storyId)
                    .ProjectTo<ProjectManagerStoryEmail>(ProjectionMapping)
                    .First();


                story.Initialize(Settings);

                Send(TemplateTypes.ProjectManagerStoryCompleted, story,
                    $@"[{story.ProjectManagerOrganizationName}] A story has been completed");
            }
           

        }

        private void StoryCompletedSendAccountManagerEmail(Guid storyId)
        {
            _logger.LogInformation(GetLogMessage("Story: {0}"), storyId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var story = context.Stories
                    .Where(x => x.Id == storyId)
                    .ProjectTo<AccountManagerStoryEmail>(ProjectionMapping)
                    .First();


                story.Initialize(Settings);

                Send(TemplateTypes.AccountManagerStoryCompleted, story,
                    $@"[{story.ProjectManagerOrganizationName}] A story has been completed");
            }
        

        }

        private void StoryCompletedSendAgencyOwnerEmail(Guid storyId)
        {
            _logger.LogInformation(GetLogMessage("Story: {0}"), storyId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var story = context.Stories
                    .Where(x => x.Id == storyId)
                    .ProjectTo<AgencyOwnerStoryEmail>(ProjectionMapping)
                    .First();


                story.Initialize(Settings);

                Send(TemplateTypes.AgencyOwnerStoryCompleted, story,
                    $@"[{story.ProjectManagerOrganizationName}] A story has been completed");
            }
            

        }

        public void Handle(StoryCompletedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Story Completed Event Triggered"));

            StoryCompletedSendContractorEmail(evt.StoryId);
            StoryCompletedSendAccountManagerEmail(evt.StoryId);
            StoryCompletedSendProjectManagerEmail(evt.StoryId);
            StoryCompletedSendAgencyOwnerEmail(evt.StoryId);
        }
    }
}