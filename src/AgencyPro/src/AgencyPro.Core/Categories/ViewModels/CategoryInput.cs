﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.Categories.ViewModels
{
    public class CategoryInput
    {
        public virtual string Name { get; set; }
        public virtual string ContractorTitle { get; set; }
        public virtual string ContractorTitlePlural { get; set; }
        public virtual string AccountManagerTitle { get; set; }
        public virtual string AccountManagerTitlePlural { get; set; }
        public virtual string ProjectManagerTitle { get; set; }
        public virtual string ProjectManagerTitlePlural { get; set; }
        public virtual string RecruiterTitle { get; set; }
        public virtual string MarketerTitle { get; set; }
        public virtual string RecruiterTitlePlural { get; set; }
        public virtual string MarketerTitlePlural { get; set; }
        public virtual string CustomerTitle { get; set; }
        public virtual string CustomerTitlePlural { get; set; }
        public virtual string StoryTitle { get; set; }
        public virtual string StoryTitlePlural { get; set; }
    }
}