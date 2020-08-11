// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Orders.Model;

namespace AgencyPro.Core.Notifications.Models
{
    public class WorkOrderNotification : Notification
    {
        public Guid WorkOrderId { get; set; }
        public WorkOrder WorkOrder { get; set; }
    }
}