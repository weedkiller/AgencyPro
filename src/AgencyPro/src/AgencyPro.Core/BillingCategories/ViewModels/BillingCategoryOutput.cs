// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.BillingCategories.ViewModels
{
    public class BillingCategoryOutput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsStoryBucket { get; set; }
        public bool IsPrivate { get; set; }
    }
}
