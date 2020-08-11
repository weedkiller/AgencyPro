// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Comments.ViewModels;
using AgencyPro.Core.Metadata;

namespace AgencyPro.Core.Leads.ViewModels
{
    [FlowDirective(FlowRoleToken.AccountManager, "leads")]
    public class AccountManagerLeadDetailsOutput : AccountManagerLeadOutput
    {
        public ICollection<CommentOutput> Comments { get; set; }

    }
}