// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.TimeEntries.ViewModels
{
    public class DayTimeTrackingModel
    {
        public Guid ContractId { get; set; }
        public Guid? StoryId { get; set; }
        public int TimeType { get; set; }
        public DateTime StartDateTime { get; set; }

        public string Notes { get; set; }
        public bool? CompleteStory { get; set; }
    }
}