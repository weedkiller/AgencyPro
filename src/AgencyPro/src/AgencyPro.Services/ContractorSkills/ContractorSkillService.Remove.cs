// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Roles.Services;

namespace AgencyPro.Services.ContractorSkills
{
    public partial class ContractorSkillService
    {
        public async Task<bool> Remove(IContractor contractor, Guid skillId)
        {
            return await Repository
                .DeleteAsync(x => x.ContractorId == contractor.Id && x.SkillId == skillId, true);

        }
    }
}