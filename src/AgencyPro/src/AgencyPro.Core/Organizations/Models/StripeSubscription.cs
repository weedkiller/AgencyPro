// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.Models;
using AgencyPro.Core.Stripe.Model;

namespace AgencyPro.Core.Organizations.Models
{
    public class StripeSubscription : AuditableEntity
    {
        public OrganizationSubscription OrganizationSubscription { get; set; }
        public string Id { get; set; }

        public DateTime? CanceledAt { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndedAt { get; set; }
        public DateTime? TrialEnd { get; set; }
        public DateTime? CurrentPeriodEnd { get; set; }
        public DateTime? CurrentPeriodStart { get; set; }

        public string CustomerId { get; set; }

        public bool CancelAtPeriodEnd { get; set; }

        public ICollection<StripeInvoice> Invoices { get; set; }
        public ICollection<StripeSubscriptionItem> Items { get; set; }
    }
}