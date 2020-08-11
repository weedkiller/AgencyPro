// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Skills.Services;

namespace AgencyPro.Core.Skills.ViewModels
{
    public class OrganizationSkillOutput : OrganizationSkillInput, IOrganizationSkill
    {
        public virtual string SkillName { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid SkillId { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
    }
}