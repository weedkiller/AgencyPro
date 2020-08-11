// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json;

namespace AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationRecruiters
{
    public class ContractorOrganizationRecruiterOutput
        : OrganizationRecruiterOutput
    {
        [JsonIgnore] public override decimal RecruiterStream { get; set; }
        [JsonIgnore] public override decimal RecruiterBonus { get; set; }
    }
}