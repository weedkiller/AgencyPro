// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Models;
using AgencyPro.Core.Organizations.Models;

namespace AgencyPro.Core.BillingCategories.Models
{
    
    public class OrganizationBillingCategory : AuditableEntity
    {
        public Guid OrganizationId { get; set; }
        public int BillingCategoryId { get; set; }

        public Organization Organization { get; set; }
        public BillingCategory BillingCategory { get; set; }
    }
}
