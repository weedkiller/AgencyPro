// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Proposals.Enums;

namespace AgencyPro.Core.Proposals.Models
{
    public class ProposalStatusTransition : IObjectState
    {
        public ProposalStatusTransition()
        {
            
        }

        public int Id { get; set; }
        public Guid ProposalId { get; set; }
        public ProposalStatus Status { get; set; }
        public DateTimeOffset Created { get; set; }
        public ObjectState ObjectState { get; set; }
    }
}