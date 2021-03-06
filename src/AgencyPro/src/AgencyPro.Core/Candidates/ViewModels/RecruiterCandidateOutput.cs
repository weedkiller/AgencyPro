﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Metadata;
using Newtonsoft.Json;

namespace AgencyPro.Core.Candidates.ViewModels
{
    [FlowDirective(FlowRoleToken.Recruiter, "candidates")]
    public class RecruiterCandidateOutput : CandidateOutput
    {
        [JsonIgnore]
        public override Guid RecruiterId { get; set; }

        [JsonIgnore]
        public override Guid RecruiterOrganizationId { get; set; }

        [JsonIgnore]
        public override string RecruiterOrganizationImageUrl { get; set; }

        public override Guid TargetOrganizationId => this.RecruiterOrganizationId;
        public override Guid TargetPersonId => this.RecruiterId;


        [JsonIgnore]
        public override string RecruiterImageUrl { get; set; }

        [JsonIgnore]
        public override string RecruiterName { get; set; }
        [JsonIgnore]
        public override string RecruiterPhoneNumber { get; set; }
        [JsonIgnore]
        public override string RecruiterEmail { get; set; }

        [JsonIgnore]
        public override string RecruiterOrganizationName { get; set; }
        
        [JsonIgnore]
        public override Guid? ProjectManagerId { get; set; }

        [JsonIgnore]
        public override string ProjectManagerImageUrl { get; set; }

        [JsonIgnore]
        public override string ProjectManagerName { get; set; }

        [JsonIgnore]
        public override string ProjectManagerOrganizationImageUrl { get; set; }

        [JsonIgnore]
        public override string ProjectManagerOrganizationName { get; set; }

        [JsonIgnore]
        public override Guid? ProjectManagerOrganizationId { get; set; }

        [JsonIgnore]
        public override decimal RecruitingAgencyStream { get; set; }

        [JsonIgnore]
        public override decimal RecruitingAgencyBonus { get; set; }

        [JsonIgnore]
        public override decimal RecruitingBonus { get; }

        [JsonIgnore]
        public override decimal RecruitingStream { get; }
    }
}