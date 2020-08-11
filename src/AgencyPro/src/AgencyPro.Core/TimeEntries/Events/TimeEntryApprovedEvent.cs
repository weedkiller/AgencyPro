// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.TimeEntries.Events
{
    public class TimeEntryApprovedEvent : TimeEntryEvent
    {
        // co

        public override string ToString()
        {
            return "timeentry.approved";
        }
    }
}