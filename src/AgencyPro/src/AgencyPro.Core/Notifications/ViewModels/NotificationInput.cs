// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Enums;
using AgencyPro.Core.UserAccount.Enums;

namespace AgencyPro.Core.Notifications.ViewModels
{
    public class NotificationInput
    {
        public Guid? OrganizationId { get; set; }
        public string Message { get; set; }
        public Guid UserId { get; set; }
        public Guid? SubjectId { get; set; }
        public string ExtraData { get; set; }
        public NotificationSubjectType SubjectType { get; set; }
        public AgencyProRole? Role { get; set; }
    }
}