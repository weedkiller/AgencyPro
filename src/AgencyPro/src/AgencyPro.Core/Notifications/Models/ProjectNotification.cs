// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Projects.Models;

namespace AgencyPro.Core.Notifications.Models
{
    public class ProjectNotification : Notification
    {
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
    }
}