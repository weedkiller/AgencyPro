// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.Organizations.MarketingOrganizations.ViewModels;

namespace AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationMarketers
{
    public class OrganizationMarketerProjections : Profile
    {
        public OrganizationMarketerProjections()
        {
            CreateMap<OrganizationMarketer, OrganizationMarketerOutput>()
                .ForMember(x => x.DisplayName, opt => opt.MapFrom(x => x.Marketer.Person.DisplayName))
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x => x.Marketer.Person.ImageUrl))
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Marketer.Person.ApplicationUser.Email))
                .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(x => x.Marketer.Person.ApplicationUser.PhoneNumber))
                .ForMember(x => x.Status, opt => opt.MapFrom(x => x.OrganizationPerson.Status))

                .IncludeAllDerived();

            CreateMap<OrganizationMarketer, OrganizationMarketerStatistics>()
                .ForMember(x => x.TotalLeads, opt => opt.MapFrom(x => x.Leads.Count))
                .ForMember(x => x.TotalContracts, opt => opt.MapFrom(x => x.Contracts.Count))
                .ForMember(x => x.TotalCustomers, opt => opt.MapFrom(x => x.Customers.Count))
                .IncludeAllDerived();


            CreateMap<OrganizationMarketer, AgencyOwnerOrganizationMarketerOutput>();
            CreateMap<OrganizationMarketer, AccountManagerOrganizationMarketerOutput>();

            CreateMap<OrganizationMarketer, MarketerCounts>()
                .ForMember(x => x.Leads, opt => opt.MapFrom(x => x.Leads.Count))
                .ForMember(x => x.Contracts, opt => opt.MapFrom(x => x.Contracts.Count))
                .ForMember(x => x.Customers, opt => opt.MapFrom(x => x.Customers.Count));


            CreateMap<OrganizationMarketer, MarketerOrganizationOutput>()
                .IncludeMembers(x => x.Organization)
                .ForMember(x => x.MarketerStream, o => o.MapFrom(x => x.MarketerStream))
                .ForMember(x => x.MarketerBonus, o => o.MapFrom(x => x.MarketerBonus))
                .IncludeAllDerived();

        }
    }
}