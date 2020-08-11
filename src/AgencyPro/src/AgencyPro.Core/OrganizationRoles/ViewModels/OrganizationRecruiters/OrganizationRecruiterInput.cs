// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.OrganizationRoles.Services;

namespace AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationRecruiters
{
    public class OrganizationRecruiterInput : IOrganizationRecruiter
    {
        public virtual decimal RecruiterStream { get; set; }

        public virtual decimal RecruiterBonus { get; set; }

        public Guid RecruiterId { get; set; }

        public Guid OrganizationId { get; set; }
    }
}