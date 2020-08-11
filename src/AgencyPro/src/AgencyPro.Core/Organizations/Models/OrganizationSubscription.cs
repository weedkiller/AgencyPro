// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Models;

namespace AgencyPro.Core.Organizations.Models
{
    public class OrganizationSubscription : AuditableEntity
    {
        public Guid Id { get; set; }

        public Organization Organization { get; set; }

        public StripeSubscription StripeSubscription { get; set; }

        public string StripeSubscriptionId { get; set; }
       
    }
}