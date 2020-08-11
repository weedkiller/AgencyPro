// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.BonusIntents.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;

namespace AgencyPro.Core.BonusIntents.Services
{
    public interface IOrganizationBonusIntentService
    {
        Task<List<OrganizationBonusIntentOutput>> GetBonusIntents(IAgencyOwner person);
        Task<BonusResult> Create(CreateBonusIntentOptions createBonusIntentOptions);
    }
}