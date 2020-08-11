// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Metadata;
using Newtonsoft.Json;

namespace AgencyPro.Core.Candidates.ViewModels
{
    [FlowDirective(FlowRoleToken.RecruitingAgencyOwner, "candidates")]
    public class RecruitingAgencyCandidateOutput : CandidateOutput
    {
        [JsonIgnore]
        public override Guid? ProjectManagerId { get; set; }

        [JsonIgnore]
        public override string ProjectManagerImageUrl { get; set; }

        [JsonIgnore]
        public override string ProjectManagerName { get; set; }

        [JsonIgnore]
        public override string ProjectManagerOrganizationImageUrl { get; set; }

        public override Guid TargetOrganizationId => this.RecruiterOrganizationId;
        public override Guid TargetPersonId => this.RecruitingOrganizationOwnerId;

        [JsonIgnore]
        public override string ProjectManagerOrganizationName { get; set; }

        [JsonIgnore]
        public override Guid? ProjectManagerOrganizationId { get; set; }

    }
}