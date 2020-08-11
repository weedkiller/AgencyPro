// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Stories.ViewModels
{
    public class ProposedStoryOutput
    {
        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual int? StoryPoints { get; set; }
    }
}