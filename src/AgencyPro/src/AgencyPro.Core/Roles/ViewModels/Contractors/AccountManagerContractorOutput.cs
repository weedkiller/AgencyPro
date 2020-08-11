// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json;

namespace AgencyPro.Core.Roles.ViewModels.Contractors
{
    public sealed class AccountManagerContractorOutput : ContractorOutput
    {
        [JsonIgnore] public override decimal RecruiterStream { get; set; }
    }
}