// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.Models;
using AgencyPro.Core.TimeEntries.Models;

namespace AgencyPro.Core.BillingCategories.Models
{
    public class BillingCategory : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsStoryBucket { get; set; }
        public bool IsPrivate { get; set; }
        public Guid? OrganizationId { get; set; }

        public ICollection<ProjectBillingCategory> ProjectBillingCategories { get; set; }
        public ICollection<TimeEntry> TimeEntries { get; set; }
        public ICollection<CategoryBillingCategory> CategoryBillingCategories { get; set; }
        public ICollection<OrganizationBillingCategory> OrganizationBillingCategories { get; set; }
    }
}