// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json;

namespace AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels
{
    public class ProjectManagerOrganizationOutput : ProviderOrganizationOutput
    {
        [JsonIgnore] public override decimal AccountManagerStream { get; set; }
        
        [JsonIgnore] public override decimal AgencyStream { get; set; }

        public override int PreviousDaysAllowed { get; set; }
        [JsonIgnore] public override decimal SystemStream { get; set; }

        [JsonIgnore] public override string AccountManagerInformation { get; set; }
        public override string ContractorInformation { get; set; }
        public override int FutureDaysAllowed { get; set; }
        public override string ProviderInformation { get; set; }
        public override string ProjectManagerInformation { get; set; }
    }
}