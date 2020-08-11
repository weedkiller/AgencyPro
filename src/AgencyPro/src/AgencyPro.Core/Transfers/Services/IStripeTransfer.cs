// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.Transfers.Services
{
    public interface IStripeTransfer
    {
        string Id { get; set; }
        decimal Amount { get; set; }
        decimal AmountReversed { get; set; }
        string Description { get; set; }
        string DestinationId { get; set; }
        string DestinationPaymentId { get; set; }
    }
}