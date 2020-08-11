// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Stories.Events;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Stories.EmailNotifications
{
    public partial class StoryEventHandlers
    {

        public void Handle(StoryApprovedEvent evt)
        {
           _logger.LogInformation(GetLogMessage("Story approved event triggered"));
        }
    }
}