// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Metadata;
using Newtonsoft.Json;

namespace AgencyPro.Core.Proposals.ViewModels
{
    [FlowDirective(FlowRoleToken.Customer, "proposals")]
    public class CustomerFixedPriceProposalOutput : FixedPriceProposalOutput
    {
        [JsonIgnore]
        public override decimal OtherPercentBasis { get; set; }

        public override Guid TargetOrganizationId => this.CustomerOrganizationId;
        public override Guid TargetPersonId => this.CustomerId;
    }
}