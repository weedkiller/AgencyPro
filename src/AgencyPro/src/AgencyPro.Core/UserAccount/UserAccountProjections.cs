// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AutoMapper;
using AgencyPro.Core.UserAccount.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace AgencyPro.Core.UserAccount
{
    public class UserAccountProjections : Profile
    {
        public UserAccountProjections()
        {
            CreateMap<IdentityUserClaim<Guid>, UserClaimOutput>()
                .ForMember(x => x.Type, opt => opt.MapFrom(x => x.ClaimType))
                .ForMember(x => x.Value, opt => opt.MapFrom(x => x.ClaimValue))
                .IncludeAllDerived();

        }
    }
}
