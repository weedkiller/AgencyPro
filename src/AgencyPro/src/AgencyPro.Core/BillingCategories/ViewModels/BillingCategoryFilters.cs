// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.BillingCategories.ViewModels
{
    public class BillingCategoryFilters
    {
        public Guid? StoryId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? ContractId { get; set; }
    }
}
