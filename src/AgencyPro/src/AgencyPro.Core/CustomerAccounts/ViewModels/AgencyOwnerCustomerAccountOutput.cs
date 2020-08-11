// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json;

namespace AgencyPro.Core.CustomerAccounts.ViewModels
{
    public class AgencyOwnerCustomerAccountOutput : CustomerAccountOutput
    {
        [JsonIgnore]
        public override int BuyerNumber { get; set; }
    }
}