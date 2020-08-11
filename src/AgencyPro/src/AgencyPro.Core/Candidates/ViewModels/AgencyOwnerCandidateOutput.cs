// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Metadata;
using Newtonsoft.Json;

namespace AgencyPro.Core.Candidates.ViewModels
{
    [FlowDirective(FlowRoleToken.AgencyOwner, "candidates")]
    public class AgencyOwnerCandidateOutput : CandidateOutput
    {
        [JsonIgnore]
        public override decimal RecruiterStream { get; set; }

        [JsonIgnore]
        public override decimal RecruiterBonus { get; set; }

        [JsonIgnore]
        public override decimal RecruitingAgencyBonus { get; set; }

        public override Guid TargetOrganizationId => this.ProviderOrganizationId;
        public override Guid TargetPersonId => this.ProviderOrganizationOwnerId;

        [JsonIgnore]
        public override decimal RecruitingAgencyStream { get; set; }
    }
}