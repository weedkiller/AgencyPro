// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Contracts.ViewModels;
using AgencyPro.Core.Skills.ViewModels;
using AgencyPro.Core.Stories.ViewModels;

namespace AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors
{
    public class AccountManagerOrganizationContractorDetailsOutput
        :OrganizationContractorStatistics
    {
        public virtual IList<ContractorSkillOutput> Skills { get; set; }
        public virtual IList<AccountManagerContractOutput> Contracts { get; set; }
        public virtual IList<AccountManagerStoryOutput> Stories { get; set; }
    }
}