// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Enums;
using AgencyPro.Core.Events;

namespace AgencyPro.Core.Leads.Events
{
    public class LeadDeletedEvent : BaseEvent
    {
        // recruiter?
        public Guid LeadId { get; set; }

        public LeadDeletedEvent()
        {
            Action = ModelAction.Delete;
        }
    }
}