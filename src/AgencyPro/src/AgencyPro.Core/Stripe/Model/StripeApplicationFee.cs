// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Models;

namespace AgencyPro.Core.Stripe.Model
{
    public class StripeApplicationFee : AuditableEntity
    {
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}