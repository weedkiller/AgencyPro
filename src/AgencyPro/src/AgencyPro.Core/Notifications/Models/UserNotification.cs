// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.UserAccount.Models;

namespace AgencyPro.Core.Notifications.Models
{
    public class UserNotification : Notification
    {
        public ApplicationUser User { get; set; }
        public Guid PersonId { get; set; }
    }
}