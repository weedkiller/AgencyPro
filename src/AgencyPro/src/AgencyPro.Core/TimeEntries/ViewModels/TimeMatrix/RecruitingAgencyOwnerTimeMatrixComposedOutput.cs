// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationRecruiters;

namespace AgencyPro.Core.TimeEntries.ViewModels.TimeMatrix
{
    public class RecruitingAgencyOwnerTimeMatrixComposedOutput
    {
        public ICollection<RecruitingAgencyOwnerTimeMatrixOutput> Matrix { get; set; }
        public ICollection<AgencyOwnerOrganizationRecruiterOutput> Recruiters { get; set; }
    }
}