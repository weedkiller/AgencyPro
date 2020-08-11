// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json;

namespace AgencyPro.Core.Agreements.ViewModels
{
    public class MarketerMarketingAgreementOutput : MarketingAgreementOutput
    {
        [JsonIgnore]
        public override decimal MarketingAgencyBonus { get; set; }

        [JsonIgnore]
        public override decimal MarketingStream { get; set; }
        
        [JsonIgnore]
        public override decimal MarketingAgencyStream { get; set; }

        [JsonIgnore]
        public override bool InitiatedByProvider { get; set; }
    }
}