// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Categories.Models;
using AgencyPro.Core.Models;

namespace AgencyPro.Core.BillingCategories.Models
{
    public class CategoryBillingCategory : BaseObjectState
    {
        public int CategoryId { get; set; }
        public int BillingCategoryId { get; set; }

        public Category Category { get; set; }
        public BillingCategory BillingCategory { get; set; }

    }
}