// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.Models;
using AgencyPro.Core.Transactions.Models;

namespace AgencyPro.Core.Stripe.Model
{
    public class StripePayout : AuditableEntity
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset ArrivalDate { get; set; }
        public bool Automatic { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<StripeBalanceTransaction> BalanceTransactions { get; set; }
    }
}