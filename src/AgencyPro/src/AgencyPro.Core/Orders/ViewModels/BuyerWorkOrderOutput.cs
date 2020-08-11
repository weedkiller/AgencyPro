// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using Newtonsoft.Json;

namespace AgencyPro.Core.Orders.ViewModels
{
    public class BuyerWorkOrderOutput : WorkOrderOutput
    {
        [JsonProperty("number")]
        public override int BuyerNumber { get; set; }

        [JsonIgnore]
        public override int ProviderNumber { get; set; }

        public override Guid TargetOrganizationId => this.CustomerOrganizationId;
        public override Guid TargetPersonId => this.CustomerId;
    }
}