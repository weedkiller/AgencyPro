// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.ComponentModel;

namespace AgencyPro.Core.Organizations.Models
{
    [Flags]
    public enum OrganizationType : int
    {
        [Description("Buyer")]
        Buyer = 0,

        [Description("Marketing")]
        Marketing = 1,

        [Description("Recruiting")]
        Recruiting = 2,

        [Description("Provider")]
        Provider = 4,

        [Description("Staffing")]
        StaffingAgency = Marketing | Recruiting,

        [Description("Full-Service")]
        FullServiceAgency = StaffingAgency | Provider
    }
    
}