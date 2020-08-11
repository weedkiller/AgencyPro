// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.BuyerAccounts.Models;
using AgencyPro.Core.Charges.Models;
using AgencyPro.Core.Invoices.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.PaymentIntents.Models;
using AgencyPro.Core.PayoutIntents.Models;
using AgencyPro.Core.Stripe.Services;

namespace AgencyPro.Core.Stripe.Model
{
    public sealed class StripeInvoice : AuditableEntity, IInvoice
    {
        public StripeInvoice()
        {
            this.Items = new List<StripeInvoiceItem>();
            this.Lines = new List<StripeInvoiceLine>();
            this.InvoiceTransfers = new List<InvoiceTransfer>();
            this.IndividualPayoutIntents = new List<IndividualPayoutIntent>();
            this.OrganizationPayoutIntents = new List<OrganizationPayoutIntent>();
        }



        public string Id { get; set; }
        public ProjectInvoice ProjectInvoice { get; set; }

        public string SubscriptionId { get; set; }
        public StripeSubscription SubscriptionInvoice { get; set; }

        public decimal AmountPaid { get; set; }
        public decimal AmountRemaining { get; set; }
        public decimal AmountDue { get; set; }
        public decimal AttemptCount { get; set; }
        public bool Attempted { get; set; }
        public bool AutomaticCollection { get; set; }
        public string BillingReason { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public decimal EndingBalance { get; set; }
        public string HostedInvoiceUrl { get; set; }
        public string InvoicePdf { get; set; }
        public ICollection<StripeInvoiceItem> Items { get; set; }
        public bool IsDeleted { get; set; }

        public string StripePaymentIntentId { get; set; }
        public StripePaymentIntent PaymentIntent { get; set; }
       
        public string CustomerId { get; set; }
        public BuyerAccount BuyerAccount { get; set; }

        public ICollection<StripeInvoiceLine> Lines { get; set; }
        public ICollection<InvoiceTransfer> InvoiceTransfers { get; set; }
        public ICollection<StripeCharge> Charges { get; set; }
        public ICollection<IndividualPayoutIntent> IndividualPayoutIntents { get; set; }
        public ICollection<OrganizationPayoutIntent> OrganizationPayoutIntents { get; set; }

        public string Status { get; set; }
        public string Number { get; set; }

        public decimal Total { get; set; }
        public decimal Subtotal { get; set; }
    }
}