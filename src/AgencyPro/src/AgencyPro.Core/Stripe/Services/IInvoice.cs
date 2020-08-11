// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Stripe.Services
{
    public interface IInvoice
    {
        string Id { get; set; }
        decimal AmountPaid { get; set; }
        decimal AmountRemaining { get; set; }
        decimal AmountDue { get; set; }
        decimal AttemptCount { get; set; }
        bool Attempted { get; set; }
        bool AutomaticCollection { get; set; }
        string BillingReason { get; set; }
        DateTimeOffset? DueDate { get; set; }
        decimal EndingBalance { get; set; }
        string HostedInvoiceUrl { get; set; }
        string InvoicePdf { get; set; }
        string Status { get; set; }
        string Number { get; set; }
        decimal Total { get; set; }
        decimal Subtotal { get; set; }
    }
}