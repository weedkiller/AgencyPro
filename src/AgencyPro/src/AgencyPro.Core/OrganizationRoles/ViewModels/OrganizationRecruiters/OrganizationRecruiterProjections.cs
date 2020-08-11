// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.Organizations.RecruitingOrganizations.ViewModels;

namespace AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationRecruiters
{
    public class OrganizationRecruiterProjections : Profile
    {
        public OrganizationRecruiterProjections()
        {
            CreateMap<OrganizationRecruiter, OrganizationRecruiterOutput>()
                .ForMember(x => x.DisplayName, opt => opt.MapFrom(x => x.Recruiter.Person.DisplayName))
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Recruiter.Person.ApplicationUser.Email))
                .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(x => x.Recruiter.Person.ApplicationUser.PhoneNumber))
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x => x.Recruiter.Person.ImageUrl))
                .ForMember(x => x.Status, opt => opt.MapFrom(x => x.OrganizationPerson.Status))
                .IncludeAllDerived();

            CreateMap<OrganizationRecruiter, OrganizationRecruiterStatistics>()
                .ForMember(x => x.TotalCandidates, opt => opt.MapFrom(x => x.Candidates.Count))
                .ForMember(x => x.TotalContractors, opt => opt.MapFrom(x => x.Contractors.Count))
                .ForMember(x => x.TotalContracts, opt => opt.MapFrom(x => x.Contracts.Count))
                .IncludeAllDerived();

            CreateMap<OrganizationRecruiter, AgencyOwnerOrganizationRecruiterOutput>();
            CreateMap<OrganizationRecruiter, AccountManagerOrganizationRecruiterOutput>();
            CreateMap<OrganizationRecruiter, RecruiterOrganizationRecruiterOutput>();
            CreateMap<OrganizationRecruiter, ContractorOrganizationRecruiterOutput>();

            CreateMap<OrganizationRecruiter, RecruiterCounts>()
                .ForMember(x => x.Candidates, opt => opt.MapFrom(x => x.Candidates.Count))
                .ForMember(x => x.Contracts, opt => opt.MapFrom(x => x.Contracts.Count))
                .ForMember(x => x.Contractors, opt => opt.MapFrom(x => x.Contractors.Count));


            CreateMap<OrganizationRecruiter, RecruiterOrganizationOutput>()
                .IncludeMembers(x => x.Organization)
                .ForMember(x => x.RecruiterStream, o => o.MapFrom(x => x.RecruiterStream))
                .ForMember(x => x.RecruiterBonus, o => o.MapFrom(x => x.RecruiterBonus))
                .IncludeAllDerived();

        }
    }
}