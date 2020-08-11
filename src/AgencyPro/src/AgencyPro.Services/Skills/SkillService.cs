// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Skills.Models;
using AgencyPro.Core.Skills.Services;
using AgencyPro.Core.Skills.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Services.Skills
{
    public class SkillService : Service<Skill>, ISkillService
    {
        public SkillService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public Task<List<T>> GetAllSkills<T>(
        )
            where T : SkillOutput
        {
            return Repository.Queryable()
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public async Task<List<T>> GetSkillsForCategory<T>(int categoryId)
            where T : SkillOutput
        {
            return await Repository.Queryable()
                .Where(x => x.SkillCategories.Select(y => y.CategoryId).Contains(categoryId))
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }
    }
}