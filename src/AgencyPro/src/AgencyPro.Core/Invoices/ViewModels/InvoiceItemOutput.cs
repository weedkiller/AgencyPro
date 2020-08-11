// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Stripe.Model;

namespace AgencyPro.Core.Invoices.ViewModels
{
    public class InvoiceItemOutput : IStripeInvoiceItem
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string CustomerId { get; set; }
        public string Description { get; set; }
        public string InvoiceId { get; set; }
        public int Quantity { get; set; }
        public DateTime? PeriodStart { get; set; }
        public DateTime? PeriodEnd { get; set; }
        public Guid? ContractId { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
    }
}