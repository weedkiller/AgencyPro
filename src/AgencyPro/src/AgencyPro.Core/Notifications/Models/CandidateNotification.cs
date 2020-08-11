// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Candidates.Models;

namespace AgencyPro.Core.Notifications.Models
{
    public class CandidateNotification : Notification
    {
        public Guid CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}