// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationRecruiters
{
    public class OrganizationRecruiterStatistics : OrganizationRecruiterOutput
    {
        public virtual int TotalContracts { get; set; }
        public virtual int TotalContractors { get; set; }
        public virtual int TotalCandidates { get; set; }
    }


}