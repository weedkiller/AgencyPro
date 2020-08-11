// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.OrganizationPeople.Models;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.People.Enums;
using System.Linq;

namespace AgencyPro.Core.OrganizationPeople
{
    public partial class OrganizationPersonProjections : Profile
    {
        public OrganizationPersonProjections()
        {
            CreateMap<OrganizationPerson, OrganizationPersonInput>()
                .IncludeAllDerived();

            CreateMap<OrganizationPerson, OrganizationPersonOutput>()
                .ForMember(x => x.LastLogin, opt => opt.MapFrom(x => x.Person.ApplicationUser.LastLogin))
                .ForMember(x => x.HasFinancialAccount, opt => opt.MapFrom(x => x.Person.IndividualFinancialAccount !=null))
                .ForMember(x => x.OrganizationName, opt => opt.MapFrom(x => x.Organization.Name))
                .ForMember(x => x.OrganizationImageUrl, opt => opt.MapFrom(x => x.Organization.ImageUrl))
                .ForMember(x => x.IsCustomer, opt => opt.MapFrom(x => x.Customer != null))
                .ForMember(x => x.IsOrganizationOwner, opt => opt.MapFrom(x => x.Customer != null))
                .ForMember(x => x.IsMarketer, opt => opt.MapFrom(x => x.Marketer != null))
                .ForMember(x => x.IsDefaultMarketer, opt => opt.MapFrom(x => x.Marketer != null && x.Marketer.OrganizationDefaults.Any()))
                .ForMember(x => x.MarketerStream, opt => opt.MapFrom(x => x.Marketer != null ? (decimal?)x.Marketer.MarketerStream : null))
                .ForMember(x => x.MarketerBonus, opt => opt.MapFrom(x => x.Marketer != null ? (decimal?)x.Marketer.MarketerBonus : null))
                .ForMember(x => x.IsAccountManager, opt => opt.MapFrom(x => x.AccountManager != null))
                .ForMember(x => x.IsDefaultAccountManager, opt => opt.MapFrom(x => x.AccountManager.DefaultOrganizations.Any()))
                .ForMember(x => x.AccountManagerStream, opt => opt.MapFrom(x =>  x.AccountManager != null ? (decimal?)x.AccountManager.AccountManagerStream : null))
                .ForMember(x => x.IsProjectManager, opt => opt.MapFrom(x => x.ProjectManager != null))
                .ForMember(x => x.IsDefaultProjectManager, opt => opt.MapFrom(x => x.ProjectManager.DefaultOrganizations.Any()))
                .ForMember(x => x.ProjectManagerStream, opt => opt.MapFrom(x => x.ProjectManager != null ? (decimal?)x.ProjectManager.ProjectManagerStream : null))
                .ForMember(x => x.IsRecruiter, opt => opt.MapFrom(x => x.Recruiter != null))
                .ForMember(x => x.IsDefaultRecruiter, opt => opt.MapFrom(x => x.Recruiter != null && x.Recruiter.RecruitingOrganizationDefaults.Any()))
                .ForMember(x => x.RecruiterStream, opt => opt.MapFrom(x => x.Recruiter != null ? (decimal?)x.Recruiter.RecruiterStream : null))
                .ForMember(x => x.RecruiterBonus, opt => opt.MapFrom(x => x.Recruiter != null ? (decimal?)x.Recruiter.RecruiterBonus : null))
                .ForMember(x => x.DisplayName, opt => opt.MapFrom(x => x.Person.DisplayName))
                .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(x => x.Person.ApplicationUser.PhoneNumber))
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Person.ApplicationUser.Email))
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x => x.Person.ImageUrl))
                .ForMember(x => x.IsContractor, opt => opt.MapFrom(x => x.Contractor != null))
                .ForMember(x => x.ContractorStream, opt => opt.MapFrom(x => x.Contractor != null ? (decimal?)x.Contractor.ContractorStream : null))
                .IncludeAllDerived();

            CreateMap<OrganizationPerson, OrganizationPersonDetailsOutput>()
                .ForMember(x => x.TotalAmountEarned, opt => opt.Ignore())
                .IncludeAllDerived();

            CreateMap<OrganizationPerson, AgencyOwnerOrganizationPersonOutput>()
                .IncludeAllDerived();

            CreateMap<OrganizationPerson, AgencyOwnerOrganizationPersonDetailsOutput>()
                .ForMember(x => x.AccountManager, opt => opt.MapFrom(x => x.AccountManager))
                .ForMember(x => x.ProjectManager, opt => opt.MapFrom(x => x.ProjectManager))
                .ForMember(x => x.Contractor, opt => opt.MapFrom(x => x.Contractor))
                .ForMember(x => x.Recruiter, opt => opt.MapFrom(x => x.Recruiter))
                .ForMember(x => x.Marketer, opt => opt.MapFrom(x => x.Marketer))
                .IncludeAllDerived();

            CreateMap<OrganizationPerson, AccountManagerOrganizationPersonOutput>()
                .IncludeAllDerived();

            CreateMap<OrganizationPerson, AccountManagerOrganizationPersonDetailsOutput>()
                .ForMember(x => x.AccountManager, opt => opt.MapFrom(x => x.AccountManager))
                .ForMember(x => x.ProjectManager, opt => opt.MapFrom(x => x.ProjectManager))
                .ForMember(x => x.Contractor, opt => opt.MapFrom(x => x.Contractor))
                .ForMember(x => x.Recruiter, opt => opt.MapFrom(x => x.Recruiter))
                .ForMember(x => x.Marketer, opt => opt.MapFrom(x => x.Marketer))
                .IncludeAllDerived();

            CreateMap<OrganizationPerson, AffiliationOutput>()
                .ForMember(x => x.AffiliateCode, opt => opt.MapFrom(x => x.AffiliateCode))
                .ForMember(x => x.CategoryId, opt => opt.MapFrom(x => x.Organization.CategoryId))
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(x => x.Organization.Category.Name))
                .ForMember(x => x.PrimaryColor, opt => opt.MapFrom(x => x.Organization.PrimaryColor))
                .ForMember(x => x.SecondaryColor, opt => opt.MapFrom(x => x.Organization.SecondaryColor))
                .ForMember(x => x.TertiaryColor, opt => opt.MapFrom(x => x.Organization.TertiaryColor))
                .ForMember(x => x.IsHidden, opt => opt.MapFrom(x => x.Status == PersonStatus.Inactive || x.IsHidden))
                .ForMember(x => x.OrganizationId, opt => opt.MapFrom(x => x.OrganizationId))
                .ForMember(x => x.OrganizationImageUrl, opt => opt.MapFrom(x => x.Organization.ImageUrl))
                .ForMember(x => x.IsAgencyOwner, opt => opt.MapFrom(x => x.IsOrganizationOwner))
                .ForMember(x => x.IsMarketingAgencyOwner, opt => opt.MapFrom(x => x.IsOrganizationOwner && x.Organization.MarketingOrganization != null))
                .ForMember(x => x.MarketingAgencyFeaturesEnabled, opt => opt.MapFrom(x => x.Organization.MarketingOrganization != null))
                .ForMember(x => x.RecruitingAgencyFeaturesEnabled, opt => opt.MapFrom(x => x.Organization.RecruitingOrganization != null))
                .ForMember(x => x.ProviderAgencyFeaturesEnabled, opt => opt.MapFrom(x => x.Organization.ProviderOrganization != null))
                .ForMember(x => x.IsRecruitingAgencyOwner, opt => opt.MapFrom(x => x.IsOrganizationOwner && x.Organization.RecruitingOrganization != null))
                .ForMember(x => x.IsProviderAgencyOwner, opt => opt.MapFrom(x => x.IsOrganizationOwner && x.Organization.ProviderOrganization != null))
                .ForMember(x => x.IsAccountManager, opt => opt.MapFrom(x => x.AccountManager != null))
                .ForMember(x => x.IsProjectManager, opt => opt.MapFrom(x => x.ProjectManager != null))
                .ForMember(x => x.IsContractor, opt => opt.MapFrom(x => x.Contractor != null))
                .ForMember(x => x.IsCustomer, opt => opt.MapFrom(x => x.Customer != null))
                .ForMember(x => x.IsRecruiter, opt => opt.MapFrom(x => x.Recruiter != null))
                .ForMember(x => x.IsMarketer, opt => opt.MapFrom(x => x.Marketer != null))
                .ForMember(x => x.IsDefault, opt => opt.MapFrom(x => x.IsDefault))
                .ForMember(x => x.OrganizationName, opt => opt.MapFrom(x => x.Organization.Name));

            EmailProjections();
        }
    }
}