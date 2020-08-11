// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Models;
using AgencyPro.Core.Projects.Models;

namespace AgencyPro.Core.BillingCategories.Models
{
    public class ProjectBillingCategory : BaseObjectState
    {
        public Guid ProjectId { get; set; }
        public int BillingCategoryId { get; set; }

        public Project Project { get; set; }
        public BillingCategory BillingCategory { get; set; }
    }
}