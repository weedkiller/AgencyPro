// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.Orders.Model
{
    public enum OrderStatus
    {
        Draft = 0,
        Sent = 1,
        AwaitingProposal = 2,
        Rejected = 3
    }
}