// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Comments.ViewModels;

namespace AgencyPro.Core.Contracts.ViewModels
{
    public class AgencyOwnerMarketingContractDetailsOutput : AgencyOwnerMarketingContractOutput
    {
        public ICollection<CommentOutput> Comments { get; set; }

    }
}