// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using Newtonsoft.Json;

namespace AgencyPro.Core.Leads.ViewModels
{
    public class MarketerLeadMatrixOutput : LeadMatrixOutput
    {
        [JsonIgnore]
        public override Guid MarketerId { get; set; }

        [JsonIgnore]
        public override Guid MarketerOrganizationId { get; set; }
    }
}