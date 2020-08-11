// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.OrganizationRoles.Services;
using System;
using System.Threading.Tasks;

namespace AgencyPro.Services.OrganizationSkills
{
    public partial class OrganizationSkillService
    {
        public async Task<bool> Remove(IAgencyOwner agencyOwner, Guid skillId)
        {
            return await Repository
                .DeleteAsync(x => x.OrganizationId == agencyOwner.OrganizationId && x.SkillId == skillId, true);

        }
    }
}