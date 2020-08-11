// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.Categories.Models;
using AgencyPro.Core.Models;

namespace AgencyPro.Core.Skills.Models
{
    public class Skill : AuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public int Priority { get; set; }

        public ICollection<OrganizationSkill> OrganizationSkill { get; set; }
        public ICollection<CategorySkill> SkillCategories { get; set; }
        public ICollection<ContractorSkill> ContractorSkills { get; set; }
    }
}