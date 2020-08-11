// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Leads.Models;

namespace AgencyPro.Core.Notifications.Models
{
    public class LeadNotification : Notification
    {
        public Guid LeadId { get; set; }
        public Lead Lead { get; set; }

    }
}