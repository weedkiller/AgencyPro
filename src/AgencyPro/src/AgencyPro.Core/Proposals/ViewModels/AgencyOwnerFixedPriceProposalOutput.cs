// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Metadata;

namespace AgencyPro.Core.Proposals.ViewModels
{
    [FlowDirective(FlowRoleToken.AgencyOwner, "proposals")]
    public class AgencyOwnerFixedPriceProposalOutput : FixedPriceProposalOutput
    {
        public override Guid TargetOrganizationId => this.ProjectManagerOrganizationId;
        public override Guid TargetPersonId => this.ProviderOrganizationOwnerId;
        
    }
}