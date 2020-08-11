// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using AutoMapper;
using AgencyPro.Core.TimeEntries.Models;
using AgencyPro.Core.TimeEntries.ViewModels;

namespace AgencyPro.Core.TimeEntries
{
    public partial class TimeEntryProjections : Profile
    {
        public TimeEntryProjections()
        {
            CreateMap<TimeEntry, TimeEntryOutput>()
                .ForMember(x => x.CustomerId, opt => opt.MapFrom(x => x.CustomerId))
                .ForMember(x => x.CustomerOrganizationId, opt => opt.MapFrom(x => x.CustomerOrganizationId))
                .ForMember(x => x.StoryNumber, opt => opt.MapFrom(x => x.Story != null ? x.Story.StoryId : null))
                .ForMember(x => x.StoryDescription, opt => opt.MapFrom(x => x.Story != null ? x.Story.Description : null))
                .ForMember(x => x.ProjectName, opt => opt.MapFrom(x => x.Project.Name))
                .ForMember(x => x.ProjectId, opt => opt.MapFrom(x => x.ProjectId))
                .ForMember(x => x.ContractorName, opt => opt.MapFrom(x => x.Contractor.Person.DisplayName))
                .ForMember(x => x.ContractorImageUrl, opt => opt.MapFrom(x => x.Contractor.Person.ImageUrl))
                .ForMember(x => x.ContractorEmail, opt => opt.MapFrom(x => x.Contractor.Person.ApplicationUser.Email))
                .ForMember(x => x.ContractorPhoneNumber, opt => opt.MapFrom(x => x.Contractor.Person.ApplicationUser.PhoneNumber))

                .ForMember(x => x.ContractorOrganizationName, opt => opt.MapFrom(x => x.OrganizationContractor.Organization.Name))
                .ForMember(x => x.ContractorOrganizationImageUrl, opt => opt.MapFrom(x => x.OrganizationContractor.Organization.ImageUrl))
                .ForMember(x => x.ContractorOrganizationId, opt => opt.MapFrom(x => x.ProviderOrganizationId))
                .ForMember(x => x.ContractorId, opt => opt.MapFrom(x => x.ContractorId))

                .ForMember(x => x.RecruitingOrganizationName, opt => opt.MapFrom(x => x.OrganizationRecruiter.Organization.Name))
                .ForMember(x => x.RecruitingOrganizationImageUrl, opt => opt.MapFrom(x => x.OrganizationRecruiter.Organization.ImageUrl))
                .ForMember(x => x.RecruitingOrganizationId, opt => opt.MapFrom(x => x.OrganizationRecruiter.OrganizationId))
                .ForMember(x => x.RecruiterId, opt => opt.MapFrom(x => x.Contract.RecruiterId))

                .ForMember(x => x.MarketingOrganizationName, opt => opt.MapFrom(x => x.OrganizationMarketer.Organization.Name))
                .ForMember(x => x.MarketingOrganizationImageUrl, opt => opt.MapFrom(x => x.OrganizationMarketer.Organization.ImageUrl))
                .ForMember(x => x.MarketingOrganizationId, opt => opt.MapFrom(x => x.OrganizationMarketer.OrganizationId))
                .ForMember(x => x.MarketerId, opt => opt.MapFrom(x => x.Contract.MarketerId))
                
                .ForMember(x => x.RecruitingOrganizationOwnerId, opt => opt.MapFrom(x => x.Contract.RecruiterOrganization.Organization.CustomerId))
                .ForMember(x => x.MarketingOrganizationOwnerId, opt => opt.MapFrom(x => x.Contract.MarketerOrganization.Organization.CustomerId))
                .ForMember(x => x.ProviderOrganizationOwnerId, opt => opt.MapFrom(x => x.Contract.ProviderOrganization.Organization.CustomerId))
                .ForMember(x => x.AccountManagerId, opt => opt.MapFrom(x => x.AccountManagerId))

                .ForMember(x => x.StatusTransitions, opt => opt.MapFrom(x => x.StatusTransitions.ToDictionary(a => a.Created, b => b.Status)))

                .IncludeAllDerived();

            CreateMap<TimeEntry, ProviderAgencyOwnerTimeEntryOutput>()
                .IncludeAllDerived();

            CreateMap<TimeEntry, AccountManagerTimeEntryOutput>()
                .IncludeAllDerived();

            CreateMap<TimeEntry, ContractorTimeEntryOutput>()
                .IncludeAllDerived();

            CreateMap<TimeEntry, ProjectManagerTimeEntryOutput>()
                .IncludeAllDerived();

            CreateMap<TimeEntry, RecruiterTimeEntryOutput>()
                .IncludeAllDerived();


            CreateMap<TimeEntry, RecruitingAgencyTimeEntryOutput>()
                .IncludeAllDerived();

            CreateMap<TimeEntry, MarketerTimeEntryOutput>()
                .IncludeAllDerived();

            CreateMap<TimeEntry, CustomerTimeEntryOutput>()
                .IncludeAllDerived();

            CreateMap<TimeEntry, ProviderAgencyOwnerTimeEntryDetailsOutput>();
            CreateMap<TimeEntry, ContractorTimeEntryDetailsOutput>();
            CreateMap<TimeEntry, AccountManagerTimeEntryDetailsOutput>();
            CreateMap<TimeEntry, ProjectManagerTimeEntryDetailsOutput>();
            CreateMap<TimeEntry, CustomerTimeEntryDetailsOutput>();
            CreateMap<TimeEntry, MarketerTimeEntryDetailsOutput>();
            CreateMap<TimeEntry, RecruiterTimeEntryDetailsOutput>();
           // CreateMap<TimeEntry, RecruitingAgencyTimeEntryDetailsOutput>();

            CreateEmailMaps();
        }
    }
}