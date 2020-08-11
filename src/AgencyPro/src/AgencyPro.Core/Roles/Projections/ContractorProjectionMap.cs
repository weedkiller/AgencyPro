// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.Extensions;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Roles.ViewModels.Contractors;
using System.Linq;

namespace AgencyPro.Core.Roles.Projections
{
    public class ContractorProjectionMap : Profile
    {
        public ContractorProjectionMap()
        {
            CreateMap<Contractor, ContractorOutput>()
                .ForMember(x => x.IsAvailable, opt => opt.MapFrom(x => x.IsAvailable))
                .ForMember(x => x.ContractorImageUrl, opt => opt.MapFrom(x => x.Person.ImageUrl))
                .ForMember(x => x.ContractorName, opt => opt.MapFrom(x => x.Person.DisplayName))
                .ForMember(x => x.RecruiterStream, opt => opt.MapFrom(x => x.OrganizationContractors.SelectMany(y => y.Contracts).WeightedAverage(a => a.RecruiterStream, b => b.MaxWeeklyHours)))
                .ForMember(x => x.DisplayName, opt => opt.MapFrom(x => x.Person.DisplayName))
                .ForMember(x => x.RecruiterName, opt => opt.MapFrom(x => x.Recruiter.Person.DisplayName))
                .ForMember(x => x.RecruiterImageUrl, opt => opt.MapFrom(x => x.Recruiter.Person.ImageUrl))
                .IncludeAllDerived();

            CreateMap<Contractor, ContractorDetailsOutput>()
                .ForMember(x=>x.Skills, opt=>opt.MapFrom(x=>x.ContractorSkills));
            CreateMap<Contractor, RecruiterContractorOutput>()
                .ForMember(x => x.Skills, opt => opt.MapFrom(x => x.ContractorSkills));

            CreateMap<Contractor, OrganizationContractorOutput>();

        }
    }
}