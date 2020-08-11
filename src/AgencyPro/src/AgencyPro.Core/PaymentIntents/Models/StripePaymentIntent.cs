// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.Charges.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.PaymentIntents.Services;
using AgencyPro.Core.Stripe.Model;

namespace AgencyPro.Core.PaymentIntents.Models
{
    public class StripePaymentIntent : AuditableEntity, IStripePaymentIntent
    {
        public string Id { get; set; }
        public decimal? Amount { get; set; }
        public decimal? AmountCapturable { get; set; }
        public decimal? AmountReceived { get; set; }
        public DateTime? CancelledAt { get; set; }
        public string CaptureMethod { get; set; }

        public ICollection<StripeCharge> Charges { get; set; }

        public string InvoiceId { get; set; }
        public StripeInvoice StripeInvoice { get; set; }

        public string ConfirmationMethod { get; set; }
        public string CustomerId { get; set; }
        public string TransferGroup { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}