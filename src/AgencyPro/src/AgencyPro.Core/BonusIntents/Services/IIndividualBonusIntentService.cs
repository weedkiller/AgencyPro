// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.BonusIntents.Models;
using AgencyPro.Core.BonusIntents.ViewModels;
using AgencyPro.Core.OrganizationPeople.Services;

namespace AgencyPro.Core.BonusIntents.Services
{
    public interface IIndividualBonusIntentService
    {
        Task<List<IndividualBonusIntentOutput>> GetBonusIntents(IOrganizationPerson person);
        Task<BonusResult> Create(CreateBonusIntentOptions options);
        Task<List<IndividualBonusIntentOutput>> GetPending(IOrganizationPerson person, BonusFilters filters);
    }
}
