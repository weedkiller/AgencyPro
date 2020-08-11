// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.People.Models;

namespace AgencyPro.Core.Notifications.Models
{
    public class PersonNotification : Notification
    {
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
    }
}