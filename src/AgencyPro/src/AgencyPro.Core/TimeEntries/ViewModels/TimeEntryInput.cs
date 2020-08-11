// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.TimeEntries.ViewModels
{
    public class TimeEntryInput
    {
        public virtual Guid ContractId { get; set; }

        public virtual DateTimeOffset StartDate { get; set; }

        public virtual int? MinutesDuration { get; set; }

        public virtual DateTimeOffset EndDate { get; set; }

        public virtual string Notes { get; set; }

        public virtual int TimeType { get; set; }
        public virtual Guid? StoryId { get; set; }
        public virtual bool? CompleteStory { get; set; }


    }
}