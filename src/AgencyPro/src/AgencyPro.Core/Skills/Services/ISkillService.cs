// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Skills.ViewModels;

namespace AgencyPro.Core.Skills.Services
{
    public interface ISkillService
    {
        Task<List<T>> GetAllSkills<T>() where T : SkillOutput;
        Task<List<T>> GetSkillsForCategory<T>(int categoryId) where T : SkillOutput;
    }
}