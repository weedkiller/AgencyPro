// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Candidates.Enums;
using AgencyPro.Core.Data.Infrastructure;

namespace AgencyPro.Core.Candidates.Models
{
    public class CandidateStatusTransition : IObjectState
    {
        public int Id { get; set; }

        public Candidate Candidate { get; set; }
        public Guid CandidateId { get; set; }

        public CandidateStatus Status { get; set; }
        public DateTimeOffset Created { get; set; }
        public ObjectState ObjectState { get; set; }
    }
}