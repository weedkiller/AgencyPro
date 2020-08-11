// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Stories.Models;

namespace AgencyPro.Core.Notifications.Models
{
    public class StoryNotification : Notification
    {
        public Guid StoryId { get; set; }
        public Story Story { get; set; }
    }
}