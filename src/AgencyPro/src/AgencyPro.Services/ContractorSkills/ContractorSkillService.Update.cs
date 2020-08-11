// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Skills.ViewModels;
using Microsoft.EntityFrameworkCore;
using Omu.ValueInjecter;

namespace AgencyPro.Services.ContractorSkills
{
    public partial class ContractorSkillService
    {
        public async Task<ContractorSkillOutput> Update(IContractor contractor, Guid skillId,
            ContractorSkillInput input)
        {
            var skill = await Repository.Queryable()
                .Where(x => x.ContractorId == contractor.Id && x.SkillId == skillId)
                .FirstAsync();

            skill.SelfAssessment = input.Priority;
            skill.Updated = DateTimeOffset.UtcNow;
            skill.InjectFrom(input);

            var result = await Repository.UpdateAsync(skill, true);

            return await Repository.Queryable()
                .Where(x => x.ContractorId == contractor.Id && x.SkillId == skillId)
                .ProjectTo<ContractorSkillOutput>(ProjectionMapping)
                .FirstAsync();
        }
    }
}