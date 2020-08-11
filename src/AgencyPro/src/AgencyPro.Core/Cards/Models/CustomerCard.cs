// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.BuyerAccounts.Models;
using AgencyPro.Core.Models;

namespace AgencyPro.Core.Cards.Models
{
    public class CustomerCard : AuditableEntity
    {
        public string Id { get; set; }

        public string CustomerId { get; set; }
        public BuyerAccount Customer { get; set; }

        public StripeCard StripeCard { get; set; }
        public bool IsDeleted { get; set; }
    }
}