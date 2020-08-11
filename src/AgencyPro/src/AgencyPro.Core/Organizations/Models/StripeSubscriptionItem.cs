// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Models;

namespace AgencyPro.Core.Organizations.Models
{
    public class StripeSubscriptionItem : AuditableEntity
    {
        public string Id { get; set; }
        public StripeSubscription Subscription { get; set; }
        public string SubscriptionId { get; set; }

        public string PlanId { get; set; }
        public bool IsDeleted { get; set; }
        public long Quantity { get; set; }
    }
}