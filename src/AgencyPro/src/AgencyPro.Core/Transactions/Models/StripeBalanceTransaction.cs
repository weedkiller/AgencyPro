// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Models;
using AgencyPro.Core.Stripe.Model;

namespace AgencyPro.Core.Transactions.Models
{
    public class StripeBalanceTransaction : AuditableEntity
    {
        public string Id { get; set; }
        public string TransactionType { get; set; }
        public string Status { get; set; }

        public decimal Gross { get; set; }
        public decimal Net { get; set; }
        public decimal Fee { get; set; }
      
        public string Description { get; set; }
        public DateTime AvailableOn { get; set; }
        public bool IsDeleted { get; set; }

        public string PayoutId { get; set; }
        public StripePayout Payout { get; set; }
    }
}