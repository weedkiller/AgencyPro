// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.Charges.Models;
using AgencyPro.Core.Charges.ViewModels;

namespace AgencyPro.Core.Charges
{
    public class ChargeProjections : Profile
    {
        public ChargeProjections()
        {
            CreateMap<StripeCharge, ChargeOutput>()
                .ForMember(x=>x.CustomerOrganizationName, 
                    opt=>opt.MapFrom(x=>x.Customer.OrganizationBuyerAccount.Organization.Name));
        }
    }
}
