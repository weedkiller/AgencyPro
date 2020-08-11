// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Metadata;
using Newtonsoft.Json;

namespace AgencyPro.Core.Leads.ViewModels
{
    [FlowDirective(FlowRoleToken.Marketer, "leads")]
    public class MarketerLeadOutput : LeadOutput
    {
        [JsonIgnore] public override Guid? AccountManagerId { get; set; }
        [JsonIgnore] public override string AccountManagerName { get; set; }
        [JsonIgnore] public override Guid? AccountManagerOrganizationId { get; set; }
        [JsonIgnore] public override string AccountManagerImageUrl { get; set; }
        [JsonIgnore] public override string AccountManagerOrganizationImageUrl { get; set; }
        [JsonIgnore] public override string AccountManagerOrganizationName { get; set; }


        public override Guid TargetOrganizationId => MarketerOrganizationId;

        public override Guid TargetPersonId => MarketerId;

        [JsonIgnore] public override string MarketerName { get; set; }

        [JsonIgnore] public override string MarketerOrganizationName { get; set; }

        [JsonIgnore] public override string MarketerImageUrl { get; set; }
        [JsonIgnore] public override string MarketerPhoneNumber { get; set; }
        [JsonIgnore] public override string MarketerEmail { get; set; }

        [JsonIgnore] public override Guid MarketerOrganizationId { get; set; }

        [JsonIgnore] public override string MarketerOrganizationImageUrl { get; set; }

        [JsonIgnore]
        public override Guid MarketerId { get; set; }

        [JsonIgnore] public override decimal MarketingStream { get; }
        [JsonIgnore]
        public override decimal MarketingBonus { get; }
        [JsonIgnore] public override decimal MarketingAgencyBonus { get; set; }
        [JsonIgnore] public override decimal MarketingAgencyStream { get; set; }

    }
}