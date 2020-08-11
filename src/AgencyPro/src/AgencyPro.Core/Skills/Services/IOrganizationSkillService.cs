// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Skills.ViewModels;

namespace AgencyPro.Core.Skills.Services
{
    public interface IOrganizationSkillService
    {
        Task<OrganizationSkillOutput> Add(IAgencyOwner agencyOwner, Guid skillId, OrganizationSkillInput input);
        Task<OrganizationSkillOutput> Update(IAgencyOwner agencyOwner, Guid skillId, OrganizationSkillInput input);

        Task<bool> Remove(IAgencyOwner agencyOwner, Guid skillId);

        Task<List<OrganizationSkillOutput>> GetSkills(Guid organizationId);
    }
}