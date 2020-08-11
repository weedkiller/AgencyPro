// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.StoryTemplates.Models
{
    public interface IStoryTemplate
    {
        Guid Id { get; set; }
        int? StoryPoints { get; set; }
        string Description { get; set; }
        string Title { get; set; }
        decimal Hours { get; set; }
    }
}