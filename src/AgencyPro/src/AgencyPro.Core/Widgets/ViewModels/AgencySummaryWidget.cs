// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.CustomerAccounts.Enums;
using AgencyPro.Core.Leads.Enums;
using AgencyPro.Core.People.Enums;
using AgencyPro.Core.Projects.Enums;
using System.Collections.Generic;
using AgencyPro.Core.Candidates.Enums;
using AgencyPro.Core.Contracts.Enums;

namespace AgencyPro.Core.Widgets.ViewModels
{
    public class AgencySummaryWidget
    {
        public Dictionary<PersonStatus, int> People { get; set; }
        public Dictionary<LeadStatus, int> Leads { get; set; }
        public Dictionary<AccountStatus, int> Accounts { get; set; }
        public Dictionary<ProjectStatus, int> Projects { get; set; }
        public Dictionary<ContractStatus, int> Contracts { get; set; }
        public Dictionary<CandidateStatus, int> Candidates { get; set; }
    }
}