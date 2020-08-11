// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel;

namespace AgencyPro.Core.Enums
{
    public enum DateRange
    {
        [Description("Custom")] Custom = 0,

        [Description("Last 24 Hours")] Last24Hours = 1,

        [Description("Today")] Today = 2,

        [Description("Yesterday")] Yesterday = 3,

        [Description("Last Week")] LastWeek = 4,

        [Description("This Week")] ThisWeek = 5,

        [Description("Last Month")] LastMonth = 6,

        [Description("All Times")] All = 7
    }
}