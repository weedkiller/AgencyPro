// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using AgencyPro.Core.BonusIntents.Models;
using AgencyPro.Core.BonusIntents.ViewModels;

namespace AgencyPro.Core.BonusIntents
{
    public class BonusProjections : Profile
    {
        public BonusProjections()
        {
            CreateMap<IndividualBonusIntent, IndividualBonusIntentOutput>().IncludeAllDerived();
            CreateMap<OrganizationBonusIntent, OrganizationBonusIntentOutput>().IncludeAllDerived();
        }
    }
}
