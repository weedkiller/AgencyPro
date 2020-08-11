// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Metadata;
using Newtonsoft.Json;

namespace AgencyPro.Core.TimeEntries.ViewModels
{
    [FlowDirective(FlowRoleToken.AgencyOwner, "time")]
    public class ProviderAgencyOwnerTimeEntryOutput : TimeEntryOutput
    {
        [JsonIgnore]
        public override decimal InstantMarketerStream { get; set; }

        [JsonIgnore]
        public override decimal InstantRecruiterStream { get; set; }

        [JsonIgnore]
        public override decimal TotalMarketerStream { get; set; }

        [JsonIgnore]
        public override decimal TotalRecruiterStream { get; set; }

        [JsonIgnore]
        public override decimal InstantMarketingAgencyStream { get; set; }

        public override Guid TargetOrganizationId => this.ContractorOrganizationId;
        public override Guid TargetPersonId => this.ProviderOrganizationOwnerId;

        [JsonIgnore]
        public override decimal InstantRecruitingAgencyStream { get; set; }
    }
}