// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Organizations.RecruitingOrganizations.Services
{
    public interface IRecruitingOrganization
    {
        decimal RecruiterStream { get; set; }
        decimal RecruitingAgencyStream { get; set; }
        Guid DefaultRecruiterId { get; set; }
    }
}