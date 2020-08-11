// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.Notifications.Models
{
    public enum NotificationType : byte
    {
        User = 0,
        System = 1,
        Lead = 2,
        Candidate = 3,
        Contract = 4,
        Invoice = 5,
        Person = 6,
        Project = 7,
        Proposal = 8,
        Story = 9,
        TimeEntry = 10,
        WorkOrder = 11
    }
}