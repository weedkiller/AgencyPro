// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.FinancialAccounts.Models;
using AgencyPro.Core.FinancialAccounts.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AgencyPro.Core.FinancialAccounts.ViewModels
{
    public class FinancialAccountOutput : IFinancialAccount
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public FinancialAccountStatus Status { get; set; }

        public bool ChargesEnabled { get; set; }

        public bool PayoutsEnabled { get; set; }
        public string AccountId { get; set; }
        
    }
}
