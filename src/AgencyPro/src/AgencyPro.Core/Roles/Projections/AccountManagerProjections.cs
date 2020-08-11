// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Roles.ViewModels.AccountManagers;

namespace AgencyPro.Core.Roles.Projections
{
    public class AccountManagerProjections : Profile
    {
        public AccountManagerProjections()
        {
            CreateMap<AccountManager, AccountManagerOutput>()
                .IncludeAllDerived();
            CreateMap<AccountManager, AccountManagerDetailsOutput>();
        }
    }
}