// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel.DataAnnotations;

namespace AgencyPro.Core.Organizations.RecruitingOrganizations.ViewModels
{
    public class RecruitingOrganizationUpgradeInput
    {
        [Range(0, 100)]
        public virtual decimal RecruiterStream { get; set; }

        [Range(0, 100)]
        public virtual decimal RecruiterBonus { get; set; }
        [Range(0, 100)]
        public virtual decimal RecruitingAgencyBonus { get; set; }

        [Range(0, 100)]
        public virtual decimal RecruitingAgencyStream { get; set; }

        public bool Discoverable { get; set; }
    }
}