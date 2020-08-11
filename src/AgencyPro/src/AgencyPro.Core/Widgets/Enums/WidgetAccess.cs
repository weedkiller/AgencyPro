// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Widgets.Enums
{
    [Flags]
    public enum WidgetAccess
    {
        None = 0,
        Recruiter = 1,
        Marketer = 2,
        Affiliates = 3,
        AgencyOwner = 4,
        AccountManager = 8,
        ProjectManager = 16,
        Contractor = 32,
        Providers = Contractor + ProjectManager + AccountManager + AgencyOwner + Affiliates,
        Customer = 64,
        CustomerAndAgencyOwner = 68,
        System = 128,
        Person = 256
    }
}