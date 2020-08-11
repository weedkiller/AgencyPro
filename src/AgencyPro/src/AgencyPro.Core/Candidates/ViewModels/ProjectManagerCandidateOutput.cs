// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Metadata;
using Newtonsoft.Json;

namespace AgencyPro.Core.Candidates.ViewModels
{
    [FlowDirective(FlowRoleToken.ProjectManager, "candidates")]
    public class ProjectManagerCandidateOutput : CandidateOutput
    {
        [JsonIgnore] public override Guid? ProjectManagerOrganizationId { get; set; }
        [JsonIgnore] public override Guid? ProjectManagerId { get; set; }
        [JsonIgnore] public override string ProjectManagerImageUrl { get; set; }
        [JsonIgnore] public override string ProjectManagerName { get; set; }
        [JsonIgnore] public override string ProjectManagerOrganizationImageUrl { get; set; }

        public override Guid TargetOrganizationId => ProviderOrganizationId;
        public override Guid TargetPersonId => ProjectManagerId.Value;

        [JsonIgnore] public override string ProjectManagerOrganizationName { get; set; }
        [JsonIgnore] public override decimal RecruitingAgencyStream { get; set; }
        [JsonIgnore] public override decimal RecruitingAgencyBonus { get; set; }
        [JsonIgnore] public override decimal RecruiterBonus { get; set; }
        [JsonIgnore] public override decimal RecruiterStream { get; set; }
        [JsonIgnore] public override decimal RecruitingStream { get; }
        [JsonIgnore] public override decimal RecruitingBonus { get; }
    }
}