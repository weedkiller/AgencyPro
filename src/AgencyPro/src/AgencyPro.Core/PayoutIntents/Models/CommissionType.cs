﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel;

namespace AgencyPro.Core.PayoutIntents.Models
{
    public enum CommissionType
    {
        [Description("Contractor Stream")]
        ContractorStream=1,

        [Description("Project Manager Stream")]
        ProjectManagerStream = 2,

        [Description("Account Manager Stream")]
        AccountManagerStream =4,

        [Description("Provider Agency Stream")]
        ProviderAgencyStream = 8,

        [Description("Marketing Agency Stream")]
        MarketingAgencyStream = 16,

        [Description("Recruiting Agency Stream")]
        RecruitingAgencyStream = 32,

        [Description("Recruiter Stream")]
        RecruiterStream = 64,

        [Description("Marketer Stream")]
        MarketerStream = 128
    }
}