// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Models;
using AgencyPro.Core.Notifications.Services;

namespace AgencyPro.Core.Notifications.Models
{
    public abstract class Notification : AuditableEntity, INotification
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public NotificationType Type { get; set; }
        public Guid UserId { get; set; }
        public bool RequiresAcknowledgement { get;set; }
        public bool? Acknowledged { get; set; }
    }
}