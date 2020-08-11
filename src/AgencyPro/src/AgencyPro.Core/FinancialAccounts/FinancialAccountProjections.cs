// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.FinancialAccounts.Models;
using AgencyPro.Core.FinancialAccounts.ViewModels;

namespace AgencyPro.Core.FinancialAccounts
{
    public class FinancialAccountProjections : Profile
    {
        public FinancialAccountProjections()
        {
            CreateMap<FinancialAccount, FinancialAccountOutput>()
                .ForMember(x => x.AccountId, opt => opt.MapFrom(x => x.AccountId))
                .IncludeAllDerived();

            CreateMap<FinancialAccount, FinancialAccountDetails>()
                .ForMember(x => x.Transfers, opt => opt.MapFrom(x => x.Transfers))
                .IncludeAllDerived();


            CreateMap<OrganizationFinancialAccount, FinancialAccountDetails>()
                .IncludeMembers(x => x.FinancialAccount);


            CreateMap<IndividualFinancialAccount, FinancialAccountDetails>()
                .IncludeMembers(x => x.FinancialAccount);
        }
    }
}
