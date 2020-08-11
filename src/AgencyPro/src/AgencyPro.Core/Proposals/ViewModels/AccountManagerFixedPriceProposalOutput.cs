// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Metadata;

namespace AgencyPro.Core.Proposals.ViewModels
{
    [FlowDirective(FlowRoleToken.AccountManager, "proposals")]
    public class AccountManagerFixedPriceProposalOutput : FixedPriceProposalOutput
    {
        public override Guid TargetOrganizationId => this.AccountManagerOrganizationId;
        public override Guid TargetPersonId => this.AccountManagerId;

    }
}