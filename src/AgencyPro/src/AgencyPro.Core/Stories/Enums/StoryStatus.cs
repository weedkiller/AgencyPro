// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.Stories.Enums
{
    public enum StoryStatus
    {
        Pending = 0,
        Approved = 1,
        Assigned = 2,
        InProgress = 4,
        Completed = 8,
        Archived = 16,
        Rejected = 32
    }
}