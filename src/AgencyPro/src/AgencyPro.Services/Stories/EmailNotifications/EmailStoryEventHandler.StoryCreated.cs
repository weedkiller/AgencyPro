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
        private void StoryCreatedSendProjectManagerEmail(Guid storyId)
        {
            _logger.LogInformation(GetLogMessage("Story: {0}"), storyId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var story = context.Stories
                    .Where(x => x.Id == storyId)
                    .ProjectTo<ProjectManagerStoryEmail>(ProjectionMapping)
                    .First();


                story.Initialize(Settings);

                Send(TemplateTypes.ProjectManagerStoryCreated, story,
                    $@"[{story.ProjectManagerOrganizationName}] A story has been created");
            }
         
        }

        private void StoryCreatedSendAccountManagerEmail(Guid storyId)
        {
            _logger.LogInformation(GetLogMessage("Story: {0}"), storyId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var story = context.Stories
                    .Where(x => x.Id == storyId)
                    .ProjectTo<AccountManagerStoryEmail>(ProjectionMapping)
                    .First();


                story.Initialize(Settings);

                Send(TemplateTypes.AccountManagerStoryCreated, story,
                    $@"[{story.ProjectManagerOrganizationName}] A story has been created");
            }
           
        }

        private void StoryCreatedSendAgencyOwnerEmail(Guid storyId)
        {
            _logger.LogInformation(GetLogMessage("Story: {0}"), storyId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var story = context.Stories
                    .Where(x => x.Id == storyId)
                    .ProjectTo<AgencyOwnerStoryEmail>(ProjectionMapping)
                    .First();

                story.Initialize(Settings);

                Send(TemplateTypes.AgencyOwnerStoryCreated, story,
                    $@"[{story.ProjectManagerOrganizationName}] A story has been created");
            }
           
        }


        public void Handle(StoryCreatedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Story Created Event Triggered"));
            
            Parallel.Invoke(new List<Action>
            {
                () => StoryCreatedSendProjectManagerEmail(evt.StoryId),
                () => StoryCreatedSendAccountManagerEmail(evt.StoryId),
                () => StoryCreatedSendAgencyOwnerEmail(evt.StoryId)
            }.ToArray());

        }
    }
}