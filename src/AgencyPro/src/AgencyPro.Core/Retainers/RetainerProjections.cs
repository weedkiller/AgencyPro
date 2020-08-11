// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.Retainers.Models;
using AgencyPro.Core.Retainers.ViewModels;
using System.Linq;

namespace AgencyPro.Core.Retainers
{
    public class RetainerProjections : Profile
    {
        public RetainerProjections()
        {
            CreateMap<ProjectRetainerIntent, RetainerOutput>()
                .ForMember(x => x.Amount, opt => opt.MapFrom(x => x.Credits.Sum(y => y.Amount)))
                .ForMember(x => x.ProjectId, opt => opt.MapFrom(x => x.ProjectId))
                .ForMember(x => x.ProjectName, opt => opt.MapFrom(x => x.Project.Name))
                .ForMember(x=>x.CustomerName, opt=>opt.MapFrom(x=>x.Customer.Person.DisplayName))
                .ForMember(x=>x.CustomerOrganizationName, opt=>opt.MapFrom(x=>x.CustomerOrganization.Name))
                .ForMember(x=>x.ProviderOrganizationName, opt=>opt.MapFrom(x=>x.ProviderOrganization.Name))
                .ForMember(x => x.CustomerId, opt => opt.MapFrom(x => x.CustomerId))
                .IncludeAllDerived();

            CreateMap<ProjectRetainerIntent, RetainerDetails>()
                .ForMember(x => x.Charges, opt => opt.MapFrom(x => x.Credits));
        }
    }
}
