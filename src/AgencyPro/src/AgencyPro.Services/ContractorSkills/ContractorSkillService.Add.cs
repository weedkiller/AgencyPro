// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Skills.Models;
using AgencyPro.Core.Skills.ViewModels;
using Microsoft.EntityFrameworkCore;
using Omu.ValueInjecter;

namespace AgencyPro.Services.ContractorSkills
{
    public partial class ContractorSkillService
    {
        public async Task<ContractorSkillOutput> Add(IContractor contractor, Guid skillId, ContractorSkillInput input)
        {
            var x1 = await Repository.Queryable()
                .Where(x => x.ContractorId == contractor.Id && x.SkillId == skillId)
                .FirstOrDefaultAsync();

            if (x1 == null)
            {
                var entity = new ContractorSkill()
                {
                    Created = DateTimeOffset.UtcNow,
                    ObjectState = ObjectState.Added,
                    ContractorId = contractor.Id,
                    SkillId = skillId
                }.InjectFrom(input) as ContractorSkill;

                var result = await Repository.InsertAsync(entity, true);
            }

            var output = await Repository.Queryable()
                .Where(x => x.ContractorId == contractor.Id && x.SkillId == skillId)
                .ProjectTo<ContractorSkillOutput>(this.ProjectionMapping)
                .FirstAsync();

            return output;
        }
    }
}