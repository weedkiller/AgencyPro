﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json;

namespace AgencyPro.Core.Organizations.RecruitingOrganizations.ViewModels
{
    public class RecruiterOrganizationOutput : RecruitingOrganizationOutput
    {
       
        [JsonIgnore]
        public override decimal RecruitingAgencyStream { get; set; }
    }
}