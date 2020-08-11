// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Enums;

namespace AgencyPro.Core.Candidates.Events
{
    public class CandidatePromotedEvent : CandidateEvent
    {
        // recruiter
        // agency owner
        public CandidatePromotedEvent()
        {
            this.Action = ModelAction.Approve;
        }
    }
}