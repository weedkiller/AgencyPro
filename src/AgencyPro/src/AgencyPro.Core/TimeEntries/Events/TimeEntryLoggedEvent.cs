// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.TimeEntries.Events
{
    public class TimeEntryLoggedEvent : TimeEntryEvent
    {
        // pm 

        public override string ToString()
        {
            return "timeentry.logged";
        }
    }
}