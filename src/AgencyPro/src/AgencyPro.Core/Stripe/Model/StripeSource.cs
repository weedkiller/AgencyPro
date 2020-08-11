// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.BuyerAccounts.Models;
using AgencyPro.Core.Models;

namespace AgencyPro.Core.Stripe.Model
{
    public class StripeSource : AuditableEntity
    {
        public string Id { get; set; }
        public string ClientSecret { get; set; }
        public string Flow { get; set; }

        public string CustomerId { get; set; }
        public BuyerAccount Customer { get; set; }

        public string Type { get; set; }
        public string Status { get; set; }
        public bool IsDeleted { get; set; }
        public long? Amount { get; set; }
    }
}