// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Comments.ViewModels;
using AgencyPro.Core.Metadata;
using AgencyPro.Core.Stories.ViewModels;
using AgencyPro.Core.TimeEntries.ViewModels;

namespace AgencyPro.Core.Contracts.ViewModels
{
    [FlowDirective(FlowRoleToken.AccountManager, "contracts")]
    public class AccountManagerContractDetailsOutput : AccountManagerContractOutput
    {
        public ICollection<AccountManagerTimeEntryOutput> TimeEntries { get; set; }
        public ICollection<AccountManagerStoryOutput> Stories { get; set; }
        public ICollection<CommentOutput> Comments { get; set; }
    }
}