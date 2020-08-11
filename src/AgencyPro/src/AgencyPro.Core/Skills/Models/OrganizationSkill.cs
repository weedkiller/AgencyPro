// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Models;
using AgencyPro.Core.Organizations.ProviderOrganizations;
using AgencyPro.Core.Skills.Services;

namespace AgencyPro.Core.Skills.Models
{
    public class OrganizationSkill : AuditableEntity, IOrganizationSkill
    {
        public ProviderOrganization Organization { get; set; }
        public Skill Skill { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid SkillId { get; set; }
        public int Priority { get; set; }
    }
}