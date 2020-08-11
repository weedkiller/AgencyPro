// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.TimeEntries.Enums
{
    public enum TimeStatus : int
    {
        None = 0,
        Logged = 1,
        Approved = 2,
        InvoiceSent = 4,
        Rejected = 8,
        PendingPayout = 16,

        Uncollectible = 32,
        Voided = 33,
        Dispersed = 64,
    }
}