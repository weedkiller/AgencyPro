// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Notifications.Services;

namespace AgencyPro.Core.Notifications.ViewModels
{
    public class NotificationOutput : NotificationInput, INotification
    {
        public int Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public int Flags { get; set; }
    }
}