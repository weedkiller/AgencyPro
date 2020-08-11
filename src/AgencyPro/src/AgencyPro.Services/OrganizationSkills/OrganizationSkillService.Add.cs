// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Skills.Models;
using AgencyPro.Core.Skills.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace AgencyPro.Services.OrganizationSkills
{
    public partial class OrganizationSkillService
    {
        
        public async Task<OrganizationSkillOutput> Add(IAgencyOwner agencyOwner, Guid skillId, OrganizationSkillInput input)
        {
            _logger.LogInformation(GetLogMessage("Skill: {0}"), skillId);

            var x1 = await Repository.Queryable()
                .Where(x => x.OrganizationId == agencyOwner.OrganizationId && x.SkillId == skillId)
                .FirstOrDefaultAsync();

            if (x1 == null)
            {
                var entity = new OrganizationSkill()
                {
                    Created = DateTimeOffset.UtcNow,
                    ObjectState = ObjectState.Added,
                    OrganizationId = agencyOwner.OrganizationId,
                    SkillId = skillId
                }.InjectFrom(input) as OrganizationSkill;

                var result = await Repository.InsertAsync(entity, true);
                if (result > 0)
                {

                }
            }

            var output = await Repository.Queryable()
                .Where(x => x.OrganizationId == agencyOwner.OrganizationId && x.SkillId == skillId)
                .ProjectTo<OrganizationSkillOutput>(this.ProjectionMapping)
                .FirstAsync();

            return output;
        }
    }
}