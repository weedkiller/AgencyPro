// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors
{
    public class UpdateContractorRecruiterInput
    {
        public Guid RecruiterId { get; set; }
        public bool UpdateAllContracts { get; set; }
    }
}