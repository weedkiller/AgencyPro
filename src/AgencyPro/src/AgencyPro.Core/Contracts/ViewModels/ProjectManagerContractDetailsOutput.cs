// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Comments.ViewModels;
using AgencyPro.Core.Stories.ViewModels;
using AgencyPro.Core.TimeEntries.ViewModels;

namespace AgencyPro.Core.Contracts.ViewModels
{
    public class ProjectManagerContractDetailsOutput : ProjectManagerContractOutput
    {
        public ICollection<ProjectManagerTimeEntryOutput> TimeEntries { get; set; }
        public ICollection<ProjectManagerStoryOutput> Stories { get; set; }
        public ICollection<CommentOutput> Comments { get; set; }

    }
}