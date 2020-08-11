// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel;

namespace AgencyPro.Core.TimeEntries.Enums
{
    public enum TimeType
    {
        [Description("Tasks")] Consulting = 1,
        [Description("Meetings")] Meetings = 2,
        [Description("Research")] Research = 3,
        [Description("Training")] Training = 4
    }
}