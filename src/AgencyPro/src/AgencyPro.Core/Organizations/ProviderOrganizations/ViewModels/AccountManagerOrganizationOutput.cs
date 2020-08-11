// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json;

namespace AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels
{
    public class AccountManagerOrganizationOutput : ProviderOrganizationOutput
    {
        [JsonIgnore] public override decimal ProjectManagerStream { get; set; }
        
        [JsonIgnore] public override decimal AgencyStream { get; set; }

        public override string AccountManagerInformation { get; set; }
        [JsonIgnore] public override string ContractorInformation { get; set; }
        [JsonIgnore] public override int FutureDaysAllowed { get; set; }
        public override string ProviderInformation { get; set; }
        [JsonIgnore] public override string ProjectManagerInformation { get; set; }
        [JsonIgnore] public override int EstimationBasis { get; set; }
        [JsonIgnore] public override int PreviousDaysAllowed { get; set; }
        [JsonIgnore] public override decimal SystemStream { get; set; }

        [JsonIgnore]
        public override decimal ContractorStream { get; set; }

    }
}