// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Models;
using AgencyPro.Core.Roles.Models;

namespace AgencyPro.Core.Skills.Models
{
    public class ContractorSkill : AuditableEntity
    {
        public Contractor Contractor { get; set; }
        public Skill Skill { get; set; }

        public Guid SkillId { get; set; }
        public Guid ContractorId { get; set; }

        public int SelfAssessment { get; set; }
    }
}