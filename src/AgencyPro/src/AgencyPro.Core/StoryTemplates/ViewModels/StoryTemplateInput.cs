// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.StoryTemplates.ViewModels
{
    public class StoryTemplateInput
    {
        public int? StoryPoints { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public decimal Hours { get; set; }
    }
}