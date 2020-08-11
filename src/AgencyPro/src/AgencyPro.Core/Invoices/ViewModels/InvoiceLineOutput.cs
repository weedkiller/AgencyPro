// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Stripe.Services;

namespace AgencyPro.Core.Invoices.ViewModels
{
    public class InvoiceLineOutput : IStripeInvoiceLine
    {
        public string Id { get; set; }
        public string InvoiceId { get; set; }
        public string Description { get; set; }
        public bool Discountable { get; set; }
        public DateTime? PeriodStart { get; set; }
        public DateTime? PeriodEnd { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string InvoiceItemId { get; set; }
        public string SubscriptionId { get; set; }
        public string Type { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
    }
}