// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Skills.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Services.OrganizationSkills
{
    public partial class OrganizationSkillService
    {
        public async Task<OrganizationSkillOutput> Update(IAgencyOwner agencyOwner, Guid skillId,
            OrganizationSkillInput input)
        {
            var skill = await Repository.Queryable()
                .Where(x => x.OrganizationId == agencyOwner.OrganizationId && x.SkillId == skillId)
                .FirstAsync();

            skill.Priority = input.Priority;
            skill.Updated = DateTimeOffset.UtcNow;

            var result = await Repository.UpdateAsync(skill, true);

            return await Repository.Queryable()
                .Where(x => x.OrganizationId == agencyOwner.OrganizationId && x.SkillId == skillId)
                .ProjectTo<OrganizationSkillOutput>(ProjectionMapping)
                .FirstAsync();
        }
    }
}