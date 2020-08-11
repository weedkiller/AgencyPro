// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Comments.ViewModels;
using AgencyPro.Core.Contracts.ViewModels;
using AgencyPro.Core.Stories.ViewModels;

namespace AgencyPro.Core.Projects.ViewModels
{
    public class ContractorProjectDetailsOutput : ContractorProjectOutput
    {
        public ICollection<ContractorStoryOutput> Stories { get; set; }
        public ICollection<ContractorContractOutput> Contracts { get; set; }
        public ICollection<CommentOutput> Comments { get; set; }

    }
}