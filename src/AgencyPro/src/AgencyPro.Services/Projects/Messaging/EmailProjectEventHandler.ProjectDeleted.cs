// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Projects.Events;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Projects.Messaging
{
    public partial class MultiProjectEventHandler
    {
        public void Handle(ProjectDeletedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Project Deleted Event Triggered"));
        }
    }
}