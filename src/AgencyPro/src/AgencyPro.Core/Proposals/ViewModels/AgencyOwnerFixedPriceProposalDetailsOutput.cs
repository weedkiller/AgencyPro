// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Contracts.ViewModels;
using AgencyPro.Core.Stories.ViewModels;

namespace AgencyPro.Core.Proposals.ViewModels
{
    public class AgencyOwnerFixedPriceProposalDetailsOutput : AgencyOwnerFixedPriceProposalOutput
    {
        public ProposalAcceptanceDetailOutput ProposalAcceptance { get; set; }
        public ICollection<ProposedContractOutput> ProposedContracts { get; set; }
        public ICollection<ProposedStoryOutput> ProposedStories { get; set; }
    }
}