// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Skills.Models;
using AgencyPro.Core.Skills.Services;
using AgencyPro.Core.Skills.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Services.ContractorSkills
{
    public partial class ContractorSkillService : Service<ContractorSkill>, IContractorSkillService
    {
        public ContractorSkillService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public Task<List<ContractorSkillOutput>> GetSkills(IContractor contractor)
        {
            return Repository.Queryable().Where(x => x.ContractorId == contractor.Id)
                .ProjectTo<ContractorSkillOutput>(ProjectionMapping)
                .ToListAsync();
        }
    }
}