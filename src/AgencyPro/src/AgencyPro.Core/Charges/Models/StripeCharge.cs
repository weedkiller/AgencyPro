// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.BuyerAccounts.Models;
using AgencyPro.Core.FinancialAccounts.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.PaymentIntents.Models;
using AgencyPro.Core.Retainers.Models;
using AgencyPro.Core.Stripe.Model;

namespace AgencyPro.Core.Charges.Models
{
    public class StripeCharge : AuditableEntity, IStripeCharge
    {
        public string Id { get; set; }
        public bool Disputed { get; set; }
        public bool Paid { get; set; }

        public string InvoiceId { get; set; }
        public StripeInvoice Invoice { get; set; }



        public string OrderId { get; set; }
        public string ReceiptEmail { get; set; }
        public string ReceiptUrl { get; set; }
        public bool IsDeleted { get; set; }

       public FinancialAccount Destination { get; set; }
       public string DestinationId { get; set; }
       public string Description { get; set; }

       public string BalanceTransactionId { get; set; }
       public bool? Captured { get; set; }
       public string CustomerId { get; set; }
       public BuyerAccount Customer { get; set; }

       public string OnBehalfOfId { get; set; }
       public bool Refunded { get; set; }
       public string StatementDescriptorSuffix { get; set; }
       public string StatementDescriptor { get; set; }

       public string PaymentIntentId { get; set; }
       public StripePaymentIntent PaymentIntent { get; set; }
       public string OutcomeType { get; set; }
       public string OutcomeSellerMessage { get; set; }
       public long OutcomeRiskScore { get; set; }
       public string OutcomeRiskLevel { get; set; }
       public string OutcomeReason { get; set; }
       public string OutcomeNetworkStatus { get; set; }
       public string ReceiptNumber { get; set; }
       public decimal Amount { get; set; }

       public ProjectRetainerIntent RetainerIntent { get; set; }
       public Guid? ProjectId { get; set; }
    }
}