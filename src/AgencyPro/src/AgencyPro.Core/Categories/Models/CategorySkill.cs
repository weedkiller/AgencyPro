// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Skills.Models;

namespace AgencyPro.Core.Categories.Models
{
    public class CategorySkill
    {
        public Guid SkillId { get; set; }
        public Skill Skill { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}