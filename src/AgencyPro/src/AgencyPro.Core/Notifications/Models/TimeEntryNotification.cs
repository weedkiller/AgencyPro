// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.TimeEntries.Models;

namespace AgencyPro.Core.Notifications.Models
{
    public class TimeEntryNotification : Notification
    {
        public Guid TimeEntryId { get; set; }
        public TimeEntry TimeEntry { get; set; }
    }
}