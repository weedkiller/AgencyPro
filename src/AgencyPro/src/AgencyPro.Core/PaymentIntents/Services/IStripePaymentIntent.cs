// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.PaymentIntents.Services
{
    public interface IStripePaymentIntent
    {
        string Id { get; set; }
        decimal? Amount { get; set; }
        decimal? AmountCapturable { get; set; }
        decimal? AmountReceived { get; set; }
        DateTime? CancelledAt { get; set; }
        string CaptureMethod { get; set; }
        string InvoiceId { get; set; }
        string ConfirmationMethod { get; set; }
        string CustomerId { get; set; }
        string TransferGroup { get; set; }
        string Description { get; set; }
        DateTimeOffset Created { get; set; }
        DateTimeOffset Updated { get; set; }
    }
}