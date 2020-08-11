// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel;

namespace AgencyPro.Core.Metadata
{

    public enum FlowRoleToken
    {
        [Description("")]
        None,

        [Description("agency-owner")]
        AgencyOwner,

        [Description("account-manager")]
        AccountManager,

        [Description("project-manager")]
        ProjectManager,

        [Description("contractor")]
        Contractor,

        [Description("customer")]
        Customer,

        [Description("recruiter")]
        Recruiter,

        [Description("marketer")]
        Marketer,

        [Description("marketing-agency-owner")]
        MarketingAgencyOwner,

        [Description("recruiting-agency-owner")]
        RecruitingAgencyOwner,
    }
}