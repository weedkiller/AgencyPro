// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Skills.Services
{
    public interface IOrganizationSkill
    {
        Guid OrganizationId { get; set; }
        Guid SkillId { get; set; }
        DateTimeOffset Created { get; set; }
        DateTimeOffset Updated { get; set; }
        int Priority { get; set; }
    }
}