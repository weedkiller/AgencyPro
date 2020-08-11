// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Messaging.PushNotification
{
    public class ParseResponse
    {
        public string ObjectId { get; set; }

        public string Code { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public string DeviceType { get; set; }

        public string DeviceToken { get; set; }

        public string Error { get; set; }
    }
}