﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Metadata;
using Newtonsoft.Json;

namespace AgencyPro.Core.Projects.ViewModels
{
    [FlowDirective(FlowRoleToken.ProjectManager, "projects")]
    public class ProjectManagerProjectOutput : ProjectOutput
    {

        [JsonIgnore] public override decimal CustomerAverageRate { get; }
        [JsonIgnore] public override decimal WeightedContractorAverage { get; }
        public override Guid TargetOrganizationId => this.ProjectManagerOrganizationId;
        public override Guid TargetPersonId => this.ProjectManagerId;

        [JsonIgnore] public override decimal WeightedAccountManagerAverage { get; }

        [JsonIgnore] public override decimal WeightedAgencyAverage { get; }
        [JsonIgnore] public override decimal WeightedMarketingAgencyAverage { get; }
        [JsonIgnore] public override decimal WeightedRecruitingAgencyAverage { get; }

        [JsonIgnore] public override decimal WeightedMarketerAverage { get; }

        [JsonIgnore] public override decimal WeightedRecruiterAverage { get; }

        [JsonIgnore] public override decimal WeightedProjectManagerAverage { get; }

        [JsonIgnore] public override decimal WeightedSystemAverage { get; }
        
        [JsonIgnore] public override decimal MaxContractorStream { get; set; }

        [JsonIgnore] public override decimal MaxMarketerStream { get; set; }

        [JsonIgnore] public override decimal MaxRecruiterStream { get; set; }

        [JsonIgnore] public override decimal MaxSystemStream { get; set; }

        [JsonIgnore] public override decimal MaxAgencyStream { get; set; }
        [JsonIgnore] public override decimal MaxRecruitingAgencyStream { get; set; }
        [JsonIgnore] public override decimal MaxMarketingAgencyStream { get; set; }

        [JsonIgnore] public override decimal MaxAccountManagerStream { get; set; }
    }
}