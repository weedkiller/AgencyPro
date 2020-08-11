// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Roles.ViewModels.Customers;

namespace AgencyPro.Core.Roles.Projections
{
    public class CustomerProjectionMap : Profile
    {
        public CustomerProjectionMap()
        {

            CreateMap<Customer, CustomerOutput>()
                .ForMember(x => x.DisplayName, opt => opt.MapFrom(x => x.Person.DisplayName))
                .IncludeAllDerived();

            CreateMap<Customer, CustomerDetailsOutput>()
                .IncludeAllDerived();

            CreateMap<Customer, MarketerCustomerOutput>()
                .IncludeAllDerived();
        }
    }
}