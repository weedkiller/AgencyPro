// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.BuyerAccounts.Models;
using AgencyPro.Core.BuyerAccounts.ViewModels;
using AgencyPro.Core.OrganizationRoles.Models;

namespace AgencyPro.Core.BuyerAccounts
{
    public class BuyerAccountProjections : Profile 
    {
        public BuyerAccountProjections()
        {
            CreateMap<BuyerAccount, BuyerAccountOutput>()
                .ForMember(x=>x.Balance, opt=>opt.MapFrom(x=>x.Balance))
                .ForMember(x=>x.Delinquent, opt=>opt.MapFrom(x=>x.Delinquent))
                .IncludeAllDerived();

            CreateMap<OrganizationBuyerAccount, BuyerAccountOutput>()
                .IncludeMembers(x => x.BuyerAccount);

            CreateMap<IndividualBuyerAccount, BuyerAccountOutput>()
                .IncludeMembers(x => x.BuyerAccount);
        }

    }
}
