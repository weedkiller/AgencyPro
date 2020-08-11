// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel;

namespace AgencyPro.Core.UserAccount.Enums
{
    public enum AgencyProRole
    {
        [Description("user")] User = 0,
        [Description("admin")] Admin = 1,
    }
}