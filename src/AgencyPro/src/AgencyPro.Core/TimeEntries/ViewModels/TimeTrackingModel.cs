// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.ComponentModel.DataAnnotations;

namespace AgencyPro.Core.TimeEntries.ViewModels
{
    public class TimeTrackingModel
    {
        public Guid ContractId { get; set; }
        public Guid? StoryId { get; set; }

        public bool? CompleteStory { get; set; }

        public DateTime StartDateTime { get; set; }

        [Range(0, 3600)] public int Duration { get; set; }

        //[JsonConverter(typeof(StringEnumConverter))]
        public int TimeType { get; set; }

        public string Notes { get; set; }
    }
}