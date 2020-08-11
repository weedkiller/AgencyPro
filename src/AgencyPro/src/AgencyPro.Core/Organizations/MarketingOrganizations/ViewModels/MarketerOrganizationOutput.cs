// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json;

namespace AgencyPro.Core.Organizations.MarketingOrganizations.ViewModels
{
    public class MarketerOrganizationOutput : MarketingOrganizationOutput
    {
        [JsonIgnore]
        public override decimal ServiceFeePerLead { get; set; }
    }
}