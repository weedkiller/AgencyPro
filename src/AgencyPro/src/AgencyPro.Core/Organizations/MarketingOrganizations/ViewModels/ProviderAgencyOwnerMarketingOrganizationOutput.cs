// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using Newtonsoft.Json;

namespace AgencyPro.Core.Organizations.MarketingOrganizations.ViewModels
{
    public class ProviderAgencyOwnerMarketingOrganizationOutput : MarketingOrganizationOutput
    {
        [JsonIgnore]
        public override Guid DefaultMarketerId { get; set; }

        [JsonIgnore]
        public override decimal ServiceFeePerLead { get; set; }

        [JsonIgnore]
        public override decimal MarketerStream { get; set; }

        [JsonIgnore]
        public override decimal MarketingAgencyStream { get; set; }

        public decimal MarketingStream => MarketerStream + MarketingAgencyStream;

        [JsonIgnore]
        public override decimal MarketingAgencyBonus { get; set; }

        [JsonIgnore]
        public override decimal MarketerBonus { get; set; }
        
        public decimal MarketingBonus => MarketerBonus + MarketingAgencyBonus + ServiceFeePerLead;
    }
}