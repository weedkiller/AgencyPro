// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.Cards.Models;
using AgencyPro.Core.Cards.ViewModels;

namespace AgencyPro.Core.Cards
{
    public class CardProjections : Profile
    {
        public CardProjections()
        {
            CreateProjections();
            CreateAccountCardProjections();
            CreateCustomerCardProjections();
        }

        private void CreateProjections()
        {
            CreateMap<StripeCard, AccountCardViewModel>()
                .ForMember(x => x.Brand, opt => opt.MapFrom(x => x.Brand))
                .ForMember(x => x.Last4, opt => opt.MapFrom(x => x.Last4))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id));

            CreateMap<StripeCard, CustomerCardViewModel>()
                .ForMember(x => x.Brand, opt => opt.MapFrom(x => x.Brand))
                .ForMember(x => x.Last4, opt => opt.MapFrom(x => x.Last4))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id));
        }

        private void CreateCustomerCardProjections()
        {
            CreateMap<CustomerCard, CustomerCardViewModel>()
                .IncludeMembers(x => x.StripeCard);
        }

        private void CreateAccountCardProjections()
        {
            CreateMap<AccountCard, AccountCardViewModel>()
                .ForMember(x => x.Type, opt => opt.MapFrom(x => x.Type))
                .ForMember(x => x.Status, opt => opt.MapFrom(x => x.Status))
                .IncludeMembers(x => x.StripeCard);
        }
    }
}
