// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Events;

namespace AgencyPro.Core.Candidates.Events
{
    public abstract class CandidateEvent : BaseEvent
    {
        public Guid CandidateId { get; set; }
    }
}