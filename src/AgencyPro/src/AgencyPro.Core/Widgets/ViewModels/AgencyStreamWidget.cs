// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.TimeEntries.Extensions;
using System;

namespace AgencyPro.Core.Widgets.ViewModels
{
    public class AgencyStreamWidget
    {
        public DateTime[] Dates { get; set; }
        public decimal[] Hours { get; set; }
        public StreamBreakdown[] Streams { get; set; }
    }
}