// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.Messaging.PushNotification
{
    public class PushNotificationTemplate : MessageTemplate
    {
        public string Sound { get; set; }

        public bool Vibrate { get; set; }

        public bool Silent { get; set; }
    }
}