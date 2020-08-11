// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.Contracts.ViewModels;
using AgencyPro.Core.Invoices.ViewModels;
using AgencyPro.Core.TimeEntries.Enums;
using System.Linq;

namespace AgencyPro.Core.Contracts
{
    public partial class ContractProjections : Profile
    {
        public ContractProjections()
        {
            CreateMap<Contract, ContractOutput>()
                .ForMember(x => x.StatusTransitions, opt => opt.MapFrom(x => x.StatusTransitions.ToDictionary(a => a.Created, b => b.Status)))

              .ForMember(x => x.IsPaused, opt => opt.MapFrom(x => x.IsPaused))
              .ForMember(x => x.IsEnded, opt => opt.MapFrom(x => x.IsEnded))
              .ForMember(x => x.Status, opt => opt.MapFrom(x => x.Status))

              .ForMember(x => x.ContractorName, opt => opt.MapFrom(x => x.Contractor.Person.DisplayName))
              .ForMember(x => x.ContractorImageUrl, opt => opt.MapFrom(x => x.Contractor.Person.ImageUrl))
              .ForMember(x => x.ContractorId, opt => opt.MapFrom(x => x.ContractorId))
              .ForMember(x => x.ContractorOrganizationImageUrl, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.ImageUrl))
              .ForMember(x => x.ContractorOrganizationName, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.Name))
              .ForMember(x => x.ContractorOrganizationId, opt => opt.MapFrom(x => x.ContractorOrganizationId))

              .ForMember(x => x.CustomerName, opt => opt.MapFrom(x => x.Customer.Person.DisplayName))
              .ForMember(x => x.CustomerImageUrl, opt => opt.MapFrom(x => x.Customer.Person.ImageUrl))
              .ForMember(x => x.CustomerId, opt => opt.MapFrom(x => x.CustomerId))
              .ForMember(x => x.CustomerOrganizationImageUrl, opt => opt.MapFrom(x => x.BuyerOrganization.ImageUrl))
              .ForMember(x => x.CustomerOrganizationName, opt => opt.MapFrom(x => x.BuyerOrganization.Name))
              .ForMember(x => x.CustomerOrganizationId, opt => opt.MapFrom(x => x.BuyerOrganizationId))

              .ForMember(x => x.MarketerName, opt => opt.MapFrom(x => x.Marketer.Person.DisplayName))
              .ForMember(x => x.MarketerImageUrl, opt => opt.MapFrom(x => x.Marketer.Person.ImageUrl))
              .ForMember(x => x.MarketerId, opt => opt.MapFrom(x => x.MarketerId))
              .ForMember(x => x.MarketerOrganizationImageUrl, opt => opt.MapFrom(x => x.MarketerOrganization.Organization.ImageUrl))
              .ForMember(x => x.MarketerOrganizationName, opt => opt.MapFrom(x => x.MarketerOrganization.Organization.Name))
              .ForMember(x => x.MarketerOrganizationId, opt => opt.MapFrom(x => x.MarketerOrganizationId))

              .ForMember(x => x.RecruiterName, opt => opt.MapFrom(x => x.Recruiter.Person.DisplayName))
              .ForMember(x => x.RecruiterImageUrl, opt => opt.MapFrom(x => x.Recruiter.Person.ImageUrl))
              .ForMember(x => x.RecruiterId, opt => opt.MapFrom(x => x.RecruiterId))
              .ForMember(x => x.RecruiterOrganizationImageUrl, opt => opt.MapFrom(x => x.OrganizationRecruiter.Organization.ImageUrl))
              .ForMember(x => x.RecruiterOrganizationName, opt => opt.MapFrom(x => x.OrganizationRecruiter.Organization.Name))
              .ForMember(x => x.RecruiterOrganizationId, opt => opt.MapFrom(x => x.RecruiterOrganizationId))


              .ForMember(x => x.AccountManagerImageUrl, opt => opt.MapFrom(x => x.AccountManager.Person.ImageUrl))
              .ForMember(x => x.AccountManagerName, opt => opt.MapFrom(x => x.AccountManager.Person.DisplayName))
              .ForMember(x => x.AccountManagerId, opt => opt.MapFrom(x => x.AccountManagerId))
              .ForMember(x => x.AccountManagerOrganizationImageUrl, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.ImageUrl))

              .ForMember(x => x.AccountManagerOrganizationName, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.Name))
              .ForMember(x => x.AccountManagerOrganizationId, opt => opt.MapFrom(x => x.AccountManagerOrganizationId))
              .ForMember(x => x.ProjectManagerImageUrl, opt => opt.MapFrom(x => x.ProjectManager.Person.ImageUrl))
              .ForMember(x => x.ProjectManagerName, opt => opt.MapFrom(x => x.ProjectManager.Person.DisplayName))
              .ForMember(x => x.ProjectManagerId, opt => opt.MapFrom(x => x.ProjectManagerId))
              .ForMember(x => x.ProjectManagerOrganizationId, opt => opt.MapFrom(x => x.ProjectManagerOrganizationId))
              .ForMember(x => x.ProjectManagerOrganizationImageUrl, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.ImageUrl))
              .ForMember(x => x.ProjectManagerOrganizationName, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.Name))
              .ForMember(x => x.ContractId, opt => opt.MapFrom(x => x.ProviderNumber.ToString()))
              .ForMember(x => x.CustomerRate, opt => opt.MapFrom(x => x.CustomerRate))
              .ForMember(x => x.MaxCustomerWeekly, opt => opt.MapFrom(x => x.MaxCustomerWeekly))
              .ForMember(x => x.MaxContractorWeekly, opt => opt.MapFrom(x => x.MaxContractorWeekly))
              .ForMember(x => x.MaxRecruiterWeekly, opt => opt.MapFrom(x => x.MaxRecruiterWeekly))
              .ForMember(x => x.MaxMarketerWeekly, opt => opt.MapFrom(x => x.MaxMarketerWeekly))
              .ForMember(x => x.MaxProjectManagerWeekly, opt => opt.MapFrom(x => x.MaxProjectManagerWeekly))
              .ForMember(x => x.MaxAccountManagerWeekly, opt => opt.MapFrom(x => x.MaxAccountManagerWeekly))
              .ForMember(x => x.MaxSystemWeekly, opt => opt.MapFrom(x => x.MaxSystemWeekly))
              .ForMember(x => x.MaxAgencyWeekly, opt => opt.MapFrom(x => x.MaxAgencyWeekly))
              .ForMember(x => x.ProjectName, opt => opt.MapFrom(x => x.Project.Name))
              .ForMember(x => x.ProjectStatus, opt => opt.MapFrom(x => x.Project.Status))
              .ForMember(x => x.ProjectId, opt => opt.MapFrom(x => x.ProjectId))

              .ForMember(x => x.ProjectAbbreviation, opt => opt.MapFrom(x => x.Project.Abbreviation))
              
              .ForMember(x => x.TotalApprovedHours, opt => opt.MapFrom(x => x.TimeEntries.Where(y => y.Status == TimeStatus.Approved).Sum(a => a.TotalHours)))
               
                .ForMember(x => x.ProviderOrganizationOwnerId, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.CustomerId))
                .ForMember(x => x.MarketingOrganizationOwnerId, opt => opt.MapFrom(x => x.MarketerOrganization.Organization.CustomerId))
                .ForMember(x => x.RecruitingOrganizationOwnerId, opt => opt.MapFrom(x => x.RecruiterOrganization.Organization.CustomerId))

              //.ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments.OrderBy(y => y.Created)))
              //.ForMember(x => x.Stories, opt => opt.MapFrom(x => x.TimeEntries.Where(z => z.StoryId.HasValue && z.Status == TimeStatus.Approved).Select(y => y.Story).Distinct()))
              //.ForMember(x => x.ApprovedTimeEntries, opt => opt.Ignore())
              .IncludeAllDerived();

            CreateMap<Contract, ProposedContractOutput>()
                .ForMember(x => x.ContractorName, opt => opt.MapFrom(x => x.Contractor.Person.DisplayName))
                .ForMember(x => x.ContractorImageUrl, opt => opt.MapFrom(x => x.Contractor.Person.ImageUrl))
                .ForMember(x => x.ContractorOrganizationImageUrl, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.ImageUrl))
                .ForMember(x => x.ContractorOrganizationName, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.Name))
                .ForMember(x => x.CustomerRate, opt => opt.MapFrom(x => x.CustomerRate))
                .ForMember(x => x.MaxCustomerWeekly, opt => opt.MapFrom(x => x.MaxCustomerWeekly));

            CreateProviderProjections();
            CreateMarketingProjections();
            CreateRecruitingProjections();
            CreateBuyerProjections();
            
            CreateMap<Contract, ContractInvoiceSummaryItem>()
                .ForMember(x => x.ApprovedHours,
                    opt => opt.MapFrom(y => y.TimeEntries.Where(z => z.Status == TimeStatus.Approved && z.InvoiceItemId == null)
                        .Sum(a => a.TotalHours)))
                .ForMember(x => x.ApprovedCustomerAmount, opt => opt.MapFrom(y => y.TimeEntries.Where(z => z.Status == TimeStatus.Approved && z.InvoiceItemId == null)
                        .Sum(a => a.TotalCustomerAmount)))
                .ForMember(x => x.ContractId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.ContractorName, opt => opt.MapFrom(x => x.Contractor.Person.DisplayName))
                .ForMember(x => x.ContractorImageUrl, opt => opt.MapFrom(x => x.Contractor.Person.ImageUrl))
                .ForMember(x => x.ContractorId, opt => opt.MapFrom(x => x.ContractorId))
                .ForMember(x => x.ContractorOrganizationImageUrl, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.ImageUrl))
                .ForMember(x => x.ContractorOrganizationName, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.Name))
                .ForMember(x => x.ContractorOrganizationId, opt => opt.MapFrom(x => x.ContractorOrganizationId));


        }
    }
}