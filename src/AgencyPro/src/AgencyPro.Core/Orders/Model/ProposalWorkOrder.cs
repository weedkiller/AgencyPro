// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Models;
using AgencyPro.Core.Proposals.Models;

namespace AgencyPro.Core.Orders.Model
{
    public class ProposalWorkOrder : BaseObjectState
    {
        public Guid WorkOrderId { get; set; }
        public Guid ProposalId { get; set; }

        public FixedPriceProposal Proposal { get; set; }
        public WorkOrder WorkOrder { get; set; }
    }
}