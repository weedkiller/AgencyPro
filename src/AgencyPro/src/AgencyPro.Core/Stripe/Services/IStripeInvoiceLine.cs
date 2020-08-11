// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Stripe.Services
{
    public interface IStripeInvoiceLine
    {
        string Id { get; set; }
        string InvoiceId { get; set; }
        string Description { get; set; }
        bool Discountable { get; set; }
        DateTime? PeriodStart { get; set; }
        DateTime? PeriodEnd { get; set; }
        DateTimeOffset Created { get; set; }
        DateTimeOffset Updated { get; set; }
        decimal Amount { get; set; }
        string Currency { get; set; }
        string InvoiceItemId { get; set; }
        string SubscriptionId { get; set; }
        string Type { get; set; }
    }
}