// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Stripe.Model
{
    public interface IStripeInvoiceItem
    {
        string Id { get; set; }
        decimal Amount { get; set; }
        string CustomerId { get; set; }
        string Description { get; set; }
        string InvoiceId { get; set; }
        int Quantity { get; set; }
        DateTime? PeriodStart { get; set; }
        DateTime? PeriodEnd { get; set; }
        Guid? ContractId { get; set; }
        DateTimeOffset Created { get; set; }
        DateTimeOffset Updated { get; set; }
    }
}