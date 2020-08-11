// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel;

namespace AgencyPro.Core.Constants
{
    public enum AgencyProStream
    {
        [Description("contractor-stream")] ContractorStream,

        [Description("account-manager-stream")]
        AccountManagerStream,

        [Description("project-manager-stream")]
        ProjectManagerStream,

        [Description("marketer-stream")] MarketerStream,

        [Description("recruiter-stream")] RecruiterStream,

        [Description("agency-stream")] AgencyOwnerStream
    }
}