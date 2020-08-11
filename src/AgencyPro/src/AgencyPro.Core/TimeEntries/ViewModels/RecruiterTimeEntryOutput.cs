﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Metadata;
using Newtonsoft.Json;

namespace AgencyPro.Core.TimeEntries.ViewModels
{
    [FlowDirective(FlowRoleToken.Recruiter, "time")]
    public class RecruiterTimeEntryOutput : TimeEntryOutput
    {
        [JsonIgnore]
        public override decimal InstantAgencyStream { get; set; }
        [JsonIgnore]
        public override decimal TotalAgencyStream { get; set; }

        [JsonIgnore]
        public override decimal InstantSystemStream { get; set; }
        [JsonIgnore]
        public override decimal TotalSystemStream { get; set; }

        [JsonIgnore]
        public override decimal InstantContractorStream { get; set; }
        [JsonIgnore]
        public override decimal TotalContractorStream { get; set; }

        [JsonIgnore]
        public override decimal InstantMarketerStream { get; set; }
        [JsonIgnore]
        public override decimal TotalMarketerStream { get; set; }

        [JsonIgnore]
        public override decimal InstantProjectManagerStream { get; set; }
        [JsonIgnore]
        public override decimal TotalProjectManagerStream { get; set; }

        [JsonIgnore]
        public override decimal InstantAccountManagerStream { get; set; }
        [JsonIgnore]
        public override decimal TotalAccountManagerStream { get; set; }

        [JsonIgnore]
        public override decimal TotalCustomerAmount { get; set; }

        public override Guid TargetOrganizationId => this.RecruitingOrganizationId;
        public override Guid TargetPersonId => this.RecruiterId;
    }
}