// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Comments.ViewModels;
using AgencyPro.Core.Contracts.ViewModels;
using AgencyPro.Core.Proposals.ViewModels;
using AgencyPro.Core.Stories.ViewModels;
using AgencyPro.Core.TimeEntries.ViewModels;

namespace AgencyPro.Core.Projects.ViewModels
{
    public class CustomerProjectDetailsOutput : CustomerProjectOutput
    {
        public CustomerFixedPriceProposalDetailModel Proposal { get; set; }

        public ICollection<CustomerContractOutput> Contracts { get; set; }
        public ICollection<CustomerStoryOutput> Stories { get; set; }
        public ICollection<CommentOutput> Comments { get; set; }

        public ICollection<CustomerTimeEntryOutput> PendingTimeEntries { get; set; }
    }
}