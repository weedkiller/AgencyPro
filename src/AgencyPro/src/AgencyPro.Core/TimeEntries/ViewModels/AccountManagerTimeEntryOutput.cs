// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Metadata;

namespace AgencyPro.Core.TimeEntries.ViewModels
{
    [FlowDirective(FlowRoleToken.AccountManager, "time")]
    public class AccountManagerTimeEntryOutput
        : TimeEntryOutput
    {
        public override Guid TargetOrganizationId => this.ContractorOrganizationId;
        public override Guid TargetPersonId => this.AccountManagerId;
    }
}