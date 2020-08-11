// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json;

namespace AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors
{
    public sealed class ContractorOrganizationContractorOutput
        : OrganizationContractorOutput
    {
        [JsonIgnore] public override bool IsFeatured { get; set; }
    }
}