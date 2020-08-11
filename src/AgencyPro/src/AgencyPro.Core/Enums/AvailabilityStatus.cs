// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel;

namespace AgencyPro.Core.Enums
{
    public enum AvailabilityStatus
    {
        [Description("Undefined")] Undefined = 0,
        [Description("Online")] Online = 1,
        [Description("Away")] Away = 2,
        [Description("Do not Disturb")] DoNotDisturb = 3,
        [Description("Invisible")] Invisible = 4,
        [Description("Offline")] Offline = 5
    }
}