// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Skills.ViewModels;

namespace AgencyPro.Core.Skills.Services
{
    public interface IContractorSkillService
    {
        Task<ContractorSkillOutput> Add(IContractor contractor, Guid skillId, ContractorSkillInput input);
        Task<ContractorSkillOutput> Update(IContractor contractor, Guid skillId, ContractorSkillInput input);

        Task<bool> Remove(IContractor contractor, Guid skillId);

        Task<List<ContractorSkillOutput>> GetSkills(IContractor contractor);
    }
}