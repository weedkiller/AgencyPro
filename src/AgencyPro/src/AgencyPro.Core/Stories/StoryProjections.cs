// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.Stories.Models;
using AgencyPro.Core.Stories.ViewModels;
using System.Linq;

namespace AgencyPro.Core.Stories
{
    public partial class StoryProjections : Profile
    {
        public StoryProjections()
        {
            CreateMap<Story, ProposedStoryOutput>()
                .ForMember(x => x.StoryPoints, opt => opt.MapFrom(x => x.StoryPoints))
                .ForMember(x => x.Title, opt => opt.MapFrom(x => x.Title))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id));

            CreateMap<Story, StoryOutput>()

                .ForMember(x => x.CustomerId, opt => opt.MapFrom(x => x.Project.CustomerId))
                .ForMember(x => x.CustomerOrganizationId, opt => opt.MapFrom(x => x.Project.CustomerOrganizationId))
                .ForMember(x => x.CustomerOrganizationImageUrl, opt => opt.MapFrom(x => x.Project.BuyerOrganization.Name))
                .ForMember(x => x.CustomerOrganizationName, opt => opt.MapFrom(x => x.Project.BuyerOrganization.ImageUrl))
                .ForMember(x => x.AccountManagerId, opt => opt.MapFrom(x => x.Project.AccountManagerId))

                .ForMember(x => x.ProviderOrganizationOwnerId, opt => opt.MapFrom(x => x.Project.ProviderOrganization.Organization.CustomerId))
                .ForMember(x => x.ProviderOrganizationId, opt => opt.MapFrom(x => x.Project.ProjectManagerOrganizationId))
                .ForMember(x => x.ProviderOrganizationName, opt => opt.MapFrom(x => x.Project.ProviderOrganization.Organization.Name))
                .ForMember(x => x.ProviderOrganizationImageUrl, opt => opt.MapFrom(x => x.Project.ProviderOrganization.Organization.ImageUrl))
                .ForMember(x => x.StatusTransitions, opt => opt.MapFrom(x => x.StatusTransitions.ToDictionary(a => a.Created, b => b.Status)))
                .ForMember(x => x.TotalHoursLogged, o => o.MapFrom(x => x.TimeEntries.Sum(y => y.TotalHours)))
                .ForMember(x => x.CustomerApprovedHours, o => o.MapFrom(x => x.CustomerApprovedHours))
                .ForMember(x => x.Status, o => o.MapFrom(x => x.Status))
                .ForMember(x => x.IsCustomerApproved, o => o.MapFrom(x => x.CustomerAcceptanceDate != null))
                .ForMember(x => x.ProjectManagerName,
                    opt => opt.MapFrom(x => x.Project.ProjectManager.Person.DisplayName))
                .ForMember(x => x.ProjectManagerEmail,
                    opt => opt.MapFrom(x => x.Project.ProjectManager.Person.ApplicationUser.Email))
                .ForMember(x => x.ProjectManagerPhoneNumber,
                    opt => opt.MapFrom(x => x.Project.ProjectManager.Person.ApplicationUser.PhoneNumber))
                .ForMember(x => x.ProjectManagerImageUrl,
                    opt => opt.MapFrom(x => x.Project.ProjectManager.Person.ImageUrl))
                .ForMember(x => x.ProjectManagerOrganizationName,
                    opt => opt.MapFrom(x => x.Project.OrganizationProjectManager.Organization.Name))
                .ForMember(x => x.ProjectManagerOrganizationImageUrl,
                    opt => opt.MapFrom(x => x.Project.OrganizationProjectManager.Organization.ImageUrl))
                .ForMember(x => x.TotalMinutesLogged,
                    opt => opt.MapFrom(x => x.TimeEntries.Sum(y => y.TotalMinutes)))
                .ForMember(x => x.ProjectName,
                    opt => opt.MapFrom(x => x.Project.Name))
                .ForMember(x => x.ProjectAbbreviation,
                    opt => opt.MapFrom(x => x.Project.Abbreviation))
                .ForMember(x => x.ProjectManagerId, opt => opt.MapFrom(x => x.Project.ProjectManagerId))
                .ForMember(x => x.ContractorId, opt => opt.MapFrom(x => x.ContractorId))
                .ForMember(x => x.ContractorOrganizationId, opt => opt.MapFrom(x => x.ContractorOrganizationId))
                .ForMember(x => x.ContractorName, opt => opt.MapFrom(x => x.Contractor != null ? x.Contractor.Person.DisplayName : null))
                .ForMember(x => x.ContractorImageUrl, opt => opt.MapFrom(x => x.Contractor != null ? x.Contractor.Person.ImageUrl : null))
                .ForMember(x => x.ContractorEmail, x => x.MapFrom(y => y.Contractor != null ? y.Contractor.Person.ApplicationUser.Email : null))
                .ForMember(x => x.ContractorPhoneNumber, x => x.MapFrom(y => y.Contractor != null ? y.Contractor.Person.ApplicationUser.PhoneNumber : null))
                .IncludeAllDerived();

            CreateMap<Story, AgencyOwnerStoryOutput>().IncludeAllDerived();
            CreateMap<Story, AgencyOwnerStoryDetailsOutput>()
                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments.OrderBy(y => y.Created)));

            CreateMap<Story, AccountManagerStoryOutput>().IncludeAllDerived();
            CreateMap<Story, AccountManagerStoryDetailsOutput>()
                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments.OrderBy(y => y.Created)));

            CreateMap<Story, ProjectManagerStoryOutput>().IncludeAllDerived();
            CreateMap<Story, ProjectManagerStoryDetailsOutput>()
                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments.OrderBy(y => y.Created)));

            CreateMap<Story, ContractorStoryOutput>().IncludeAllDerived();
            CreateMap<Story, ContractorStoryDetailsOutput>()
                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments.OrderBy(y => y.Created)));

            CreateMap<Story, CustomerStoryOutput>().IncludeAllDerived();
            CreateMap<Story, CustomerStoryDetailsOutput>()
                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments.OrderBy(y => y.Created)));


            EmailProjections();
        }
    }
}