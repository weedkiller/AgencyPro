// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.Events
{
    public class NotificationEvent : BaseEvent
    {
        public string Username { get; set; }

        public int? UserId { get; set; }

        public string Event { get; set; }
    }
}