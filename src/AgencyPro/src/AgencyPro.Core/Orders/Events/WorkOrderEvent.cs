// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Events;

namespace AgencyPro.Core.Orders.Events
{
    public abstract class WorkOrderEvent  :BaseEvent
    {
        public Guid WorkOrderId { get; set; }
    }
}
