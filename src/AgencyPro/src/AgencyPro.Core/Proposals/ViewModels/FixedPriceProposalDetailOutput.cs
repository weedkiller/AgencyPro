// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Contracts.ViewModels;
using AgencyPro.Core.Stories.ViewModels;
using System;
using System.Collections.Generic;

namespace AgencyPro.Core.Proposals.ViewModels
{
    public class FixedPriceProposalDetailOutput : FixedPriceProposalOutput
    {
        public override Guid TargetOrganizationId => this.ProjectManagerOrganizationId;

        public override Guid TargetPersonId => this.ProviderOrganizationOwnerId;

        public ProposalAcceptanceDetailOutput ProposalAcceptance { get; set; }
        public ICollection<ProposedContractOutput> ProposedContracts { get; set; }
        public ICollection<ProposedStoryOutput> ProposedStories { get; set; }
    }
}
