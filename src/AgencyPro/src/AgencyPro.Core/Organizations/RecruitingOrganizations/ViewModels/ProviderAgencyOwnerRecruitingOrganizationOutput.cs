// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using Newtonsoft.Json;

namespace AgencyPro.Core.Organizations.RecruitingOrganizations.ViewModels
{
    public class ProviderAgencyOwnerRecruitingOrganizationOutput : RecruitingOrganizationOutput
    {
        [JsonIgnore]
        public override Guid DefaultRecruiterId { get; set; }

        [JsonIgnore]
        public override decimal RecruiterStream { get; set; }

        [JsonIgnore]
        public override decimal RecruitingAgencyStream { get; set; }

        public decimal RecruitingStream => RecruiterStream + RecruitingAgencyStream;

    }
}