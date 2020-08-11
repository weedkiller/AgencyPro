// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.BuyerAccounts.Services;
using Newtonsoft.Json;

namespace AgencyPro.Core.BuyerAccounts.ViewModels
{
    public class BuyerAccountOutput : IBuyerAccount
    {
        public decimal Balance { get; set; }
        public bool Delinquent { get; set; }

        [JsonIgnore]
        public string Id { get; set; }

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
    }
}
