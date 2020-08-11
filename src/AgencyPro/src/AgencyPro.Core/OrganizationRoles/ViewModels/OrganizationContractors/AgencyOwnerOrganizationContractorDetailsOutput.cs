// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Contracts.ViewModels;
using System.Collections.Generic;
using AgencyPro.Core.Skills.ViewModels;
using AgencyPro.Core.Stories.ViewModels;

namespace AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors
{
    public class AgencyOwnerOrganizationContractorDetailsOutput
        : OrganizationContractorStatistics
    {
        public virtual IList<ContractorSkillOutput> Skills { get; set; }
        public virtual IList<AgencyOwnerProviderContractOutput> Contracts { get; set; }
        public virtual IList<AgencyOwnerStoryOutput> Stories { get; set; }
    }
}