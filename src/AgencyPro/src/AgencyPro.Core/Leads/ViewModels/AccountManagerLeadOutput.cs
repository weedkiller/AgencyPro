// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Metadata;
using Newtonsoft.Json;

namespace AgencyPro.Core.Leads.ViewModels
{
    [FlowDirective(FlowRoleToken.AccountManager, "leads")]
    public class AccountManagerLeadOutput : LeadOutput
    {
        public override Guid TargetOrganizationId => ProviderOrganizationId;
        public override Guid TargetPersonId => AccountManagerId.Value;

        [JsonIgnore] public override decimal MarketerStream { get; set; }
        [JsonIgnore] public override decimal MarketerBonus { get; set; }
        [JsonIgnore] public override decimal MarketingAgencyBonus { get; set; }
        [JsonIgnore] public override decimal MarketingAgencyStream { get; set; }
    }
}