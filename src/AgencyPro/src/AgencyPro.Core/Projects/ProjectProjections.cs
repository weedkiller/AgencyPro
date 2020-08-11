// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.Invoices.ViewModels;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.Projects.ViewModels;
using AgencyPro.Core.TimeEntries.Enums;
using System.Linq;

namespace AgencyPro.Core.Projects
{
    public partial class ProjectProjections : Profile
    {
        public ProjectProjections()
        {
            CreateMap<Project, ProjectInvoiceOutput>()

                .ForMember(x => x.CustomerId, opt => opt.MapFrom(x => x.CustomerId))
                .ForMember(x => x.CustomerOrganizationId, opt => opt.MapFrom(x => x.CustomerOrganizationId))
                .ForMember(x => x.CustomerName, opt => opt.MapFrom(x => x.Customer.Person.DisplayName))
                .ForMember(x => x.CustomerEmail, opt => opt.MapFrom(x => x.Customer.Person.ApplicationUser.Email))
                .ForMember(x => x.CustomerPhoneNumber, opt => opt.MapFrom(x => x.Customer.Person.ApplicationUser.PhoneNumber))
                .ForMember(x => x.CustomerOrganizationImageUrl, opt => opt.MapFrom(x => x.OrganizationCustomer.Organization.ImageUrl))
                .ForMember(x => x.CustomerAddressLine1, o => o.MapFrom(x => x.OrganizationCustomer.Organization.AddressLine1))
                .ForMember(x => x.CustomerAddressLine2, o => o.MapFrom(x => x.OrganizationCustomer.Organization.AddressLine2))
                .ForMember(x => x.CustomerIso2, o => o.MapFrom(x => x.OrganizationCustomer.Organization.Iso2))
                .ForMember(x => x.CustomerCity, o => o.MapFrom(x => x.OrganizationCustomer.Organization.City))
                .ForMember(x => x.CustomerOrganizationName, o => o.MapFrom(x => x.OrganizationCustomer.Organization.Name))
                .ForMember(x => x.CustomerStateProvince, o => o.MapFrom(x => x.OrganizationCustomer.Organization.ProvinceState))
                .ForMember(x => x.CustomerPostalCode, o => o.MapFrom(x => x.OrganizationCustomer.Organization.PostalCode))
                .ForMember(x => x.AccountManagerImageUrl, opt => opt.MapFrom(x => x.AccountManager.Person.ImageUrl))
                .ForMember(x => x.AccountManagerName, opt => opt.MapFrom(x => x.AccountManager.Person.DisplayName))
                .ForMember(x => x.AccountManagerEmail, opt => opt.MapFrom(x => x.AccountManager.Person.ApplicationUser.Email))
                .ForMember(x => x.AccountManagerPhoneNumber, opt => opt.MapFrom(x => x.AccountManager.Person.ApplicationUser.PhoneNumber))

                .ForMember(x => x.AccountManagerId,
                    opt => opt.MapFrom(
                        x => x.AccountManagerId))

                .ForMember(x => x.AccountManagerOrganizationImageUrl,
                    opt => opt.MapFrom(
                        x => x.OrganizationAccountManager.Organization.ImageUrl))

                .ForMember(x => x.AccountManagerOrganizationName,
                    opt => opt.MapFrom(
                        x => x.OrganizationAccountManager.Organization.Name))

                .ForMember(x => x.AccountManagerOrganizationId,
                    opt => opt.MapFrom(
                        x => x.AccountManagerOrganizationId))

                .ForMember(x => x.ProjectManagerImageUrl,
                    opt => opt.MapFrom(
                        x => x.ProjectManager.Person.ImageUrl))
                .ForMember(x => x.ProjectManagerName, opt => opt.MapFrom(x => x.ProjectManager.Person.DisplayName))
                .ForMember(x => x.ProjectManagerId, opt => opt.MapFrom(x => x.ProjectManagerId))
                .ForMember(x => x.ProjectManagerOrganizationId, opt => opt.MapFrom(x => x.ProjectManagerOrganizationId))
                .ForMember(x => x.ProjectManagerOrganizationImageUrl, opt => opt.MapFrom(x => x.OrganizationProjectManager.Organization.ImageUrl))
                .ForMember(x => x.ProjectManagerOrganizationName, opt => opt.MapFrom(x => x.OrganizationProjectManager.Organization.Name))
                .ForMember(x => x.ProjectName, opt => opt.MapFrom(x => x.Name));

            CreateMap<Project, ProjectOutput>()
                .ForMember(x => x.StatusTransitions, opt => opt.MapFrom(x => x.StatusTransitions.ToDictionary(a => a.Created, b => b.Status)))

                .ForMember(x => x.ContractCount, opt => opt.MapFrom(c => c.Contracts.Count))
                .ForMember(x => x.HasProposal, opt => opt.MapFrom(p => p.Proposal != null))
                .ForMember(x => x.HasProposalApproved, opt => opt.MapFrom(p => p.Proposal != null && p.Proposal.Status == Proposals.Enums.ProposalStatus.Accepted))
                .ForMember(x => x.MaxBillableHours, opt => opt.MapFrom(x => x.Contracts.Sum(y => y.MaxWeeklyHours)))
                .ForMember(x => x.MaxContractorStream, opt => opt.MapFrom(x => x.Contracts.Sum(y => y.MaxWeeklyHours * y.ContractorStream)))
                .ForMember(x => x.MaxRecruiterStream, opt => opt.MapFrom(x => x.Contracts.Sum(y => y.MaxWeeklyHours * y.RecruiterStream)))
                .ForMember(x => x.MaxMarketerStream, opt => opt.MapFrom(x => x.Contracts.Sum(y => y.MaxWeeklyHours * y.MarketerStream)))
                .ForMember(x => x.MaxAccountManagerStream, opt => opt.MapFrom(x => x.Contracts.Sum(y => y.MaxWeeklyHours * y.AccountManagerStream)))
                .ForMember(x => x.MaxProjectManagerStream, opt => opt.MapFrom(x => x.Contracts.Sum(y => y.MaxWeeklyHours * y.ProjectManagerStream)))
                .ForMember(x => x.MaxSystemStream, opt => opt.MapFrom(x => x.Contracts.Sum(y => y.MaxWeeklyHours * y.SystemStream)))
                .ForMember(x => x.MaxAgencyStream, opt => opt.MapFrom(x => x.Contracts.Sum(y => y.MaxWeeklyHours * y.AgencyStream)))
                .ForMember(x => x.MaxRecruitingAgencyStream, opt => opt.MapFrom(x => x.Contracts.Sum(y => y.MaxWeeklyHours * y.RecruitingAgencyStream)))
                .ForMember(x => x.MaxMarketingAgencyStream, opt => opt.MapFrom(x => x.Contracts.Sum(y => y.MaxWeeklyHours * y.MarketingAgencyStream)))
                .ForMember(x => x.AccountManagerOrganizationId, opt => opt.MapFrom(x => x.AccountManagerOrganizationId))
                .ForMember(x => x.AccountManagerId, opt => opt.MapFrom(x => x.AccountManagerId))
                .ForMember(x => x.AccountManagerName, opt => opt.MapFrom(x => x.AccountManager.Person.DisplayName))
                .ForMember(x => x.AccountManagerEmail, opt => opt.MapFrom(x => x.AccountManager.Person.ApplicationUser.Email))
                .ForMember(x => x.AccountManagerPhoneNumber, opt => opt.MapFrom(x => x.AccountManager.Person.ApplicationUser.PhoneNumber))
                .ForMember(x => x.AccountManagerImageUrl, opt => opt.MapFrom(x => x.AccountManager.Person.ImageUrl))
                .ForMember(x => x.AccountManagerOrganizationName, opt => opt.MapFrom(x => x.OrganizationAccountManager.Organization.Name))
                .ForMember(x => x.AccountManagerOrganizationImageUrl, opt => opt.MapFrom(x => x.OrganizationAccountManager.Organization.ImageUrl))
                .ForMember(x => x.ProjectManagerOrganizationName,
                    opt => opt.MapFrom(x => x.OrganizationProjectManager.Organization.Name))
                .ForMember(x => x.ProjectManagerName,
                    opt => opt.MapFrom(x => x.ProjectManager.Person.DisplayName))
                .ForMember(x => x.ProjectManagerEmail,
                    opt => opt.MapFrom(x => x.ProjectManager.Person.ApplicationUser.Email))
                .ForMember(x => x.ProjectManagerPhoneNumber,
                    opt => opt.MapFrom(x => x.ProjectManager.Person.ApplicationUser.PhoneNumber))
                .ForMember(x => x.ProjectManagerImageUrl,
                    opt => opt.MapFrom(x => x.ProjectManager.Person.ImageUrl))
                .ForMember(x => x.ProjectManagerOrganizationImageUrl,
                    opt => opt.MapFrom(x => x.OrganizationProjectManager.Organization.ImageUrl))
                .ForMember(x => x.CustomerOrganizationId,
                    opt => opt.MapFrom(x => x.CustomerOrganizationId))
                .ForMember(x => x.CustomerId, opt => opt.MapFrom(x => x.CustomerId))
                .ForMember(x => x.CustomerName, opt => opt.MapFrom(x => x.Customer.Person.DisplayName))
                .ForMember(x => x.CustomerEmail, opt => opt.MapFrom(x => x.Customer.Person.ApplicationUser.Email))
                .ForMember(x => x.CustomerPhoneNumber, opt => opt.MapFrom(x => x.Customer.Person.ApplicationUser.PhoneNumber))
                .ForMember(x => x.CustomerImageUrl, opt => opt.MapFrom(x => x.Customer.Person.ImageUrl))
                .ForMember(x => x.CustomerOrganizationName, opt => opt.MapFrom(x => x.OrganizationCustomer.Organization.Name))
                .ForMember(x => x.CustomerOrganizationImageUrl, opt => opt.MapFrom(x => x.OrganizationCustomer.Organization.ImageUrl))
                .ForMember(x => x.StoryCount, opt => opt.MapFrom(x=>x.Stories.Count))
                .ForMember(x => x.InvoiceCount, opt => opt.MapFrom(x=>x.Invoices.Count))
                .ForMember(x => x.TotalHoursLogged, opt => opt.MapFrom(x => x.TimeEntries.Where(y => y.Status != TimeStatus.Rejected && y.Status != TimeStatus.Voided ).Sum(z => z.TotalHours)))
                .ForMember(x => x.TotalHoursApproved, opt => opt.MapFrom(x=>x.TimeEntries.Where(y=>y.Status == TimeStatus.Approved && y.InvoiceItemId == null).Sum(z=>z.TotalHours)))
                .ForMember(x => x.AccountNumber, opt => opt.MapFrom(x => x.CustomerAccount.Number))

                .IncludeAllDerived();

            CreateMap<Project, AccountManagerProjectOutput>();
            CreateMap<Project, AccountManagerProjectDetailsOutput>()

                .ForMember(x => x.TimeEntryStatus, opt => opt.MapFrom(x => x.TimeEntries.GroupBy(y => y.Status).ToDictionary(a => a.Key, b => b.Count())))
                .ForMember(x => x.StoryStatus, opt => opt.MapFrom(x => x.Stories.GroupBy(y => y.Status).ToDictionary(a => a.Key, b => b.Count())))
                .ForMember(x => x.ContractStatus, opt => opt.MapFrom(x => x.Contracts.GroupBy(y => y.Status).ToDictionary(a => a.Key, b => b.Count())))

                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments.OrderBy(y => y.Created)))
                .ForMember(x => x.Proposal, opt => opt.MapFrom(x => x.Proposal));

            CreateMap<Project, AgencyOwnerProjectOutput>();
            CreateMap<Project, AgencyOwnerProjectDetailsOutput>()


                .ForMember(x => x.TimeEntryStatus, opt => opt.MapFrom(x => x.TimeEntries.GroupBy(y => y.Status).ToDictionary(a => a.Key, b => b.Count())))
                .ForMember(x => x.StoryStatus, opt => opt.MapFrom(x => x.Stories.GroupBy(y => y.Status).ToDictionary(a => a.Key, b => b.Count())))
                .ForMember(x => x.ContractStatus, opt => opt.MapFrom(x => x.Contracts.GroupBy(y => y.Status).ToDictionary(a => a.Key, b => b.Count())))


                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments.OrderBy(y => y.Created)))
                .ForMember(x => x.AvailablePaymentTerms, o => o.MapFrom(x => x.OrganizationAccountManager.Organization.PaymentTerms))
                .ForMember(x => x.Contracts, opt => opt.MapFrom(x => x.Contracts))
                .ForMember(x => x.Stories, opt => opt.MapFrom(x => x.Stories))
                .ForMember(x => x.AvailableBillingCategories,
                    opt => opt.MapFrom(x => x.OrganizationAccountManager.Organization
                        .BillingCategories))
                .ForMember(x => x.BillingCategoryIds, opt => opt.MapFrom(x => x.ProjectBillingCategories.Select(y => y.BillingCategoryId)))
                .ForMember(x => x.Proposal, opt => opt.MapFrom(x => x.Proposal));

            CreateMap<Project, ProjectManagerProjectOutput>();
            CreateMap<Project, ProjectManagerProjectDetailsOutput>()


                .ForMember(x => x.TimeEntryStatus, opt => opt.MapFrom(x => x.TimeEntries.GroupBy(y => y.Status).ToDictionary(a => a.Key, b => b.Count())))
                .ForMember(x => x.StoryStatus, opt => opt.MapFrom(x => x.Stories.GroupBy(y => y.Status).ToDictionary(a => a.Key, b => b.Count())))
                .ForMember(x => x.ContractStatus, opt => opt.MapFrom(x => x.Contracts.GroupBy(y => y.Status).ToDictionary(a => a.Key, b => b.Count())))


                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments.OrderBy(y => y.Created)))
                .ForMember(x => x.Contracts, opt => opt.MapFrom(x => x.Contracts))
                .ForMember(x => x.Stories, opt => opt.MapFrom(x => x.Stories))
                .ForMember(x => x.PendingTimeEntries, opt => opt.Ignore());

            // todo: figure out how to do apply query on child array 
            CreateMap<Project, ContractorProjectOutput>();
            CreateMap<Project, ContractorProjectDetailsOutput>()


                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments.OrderBy(y => y.Created)))
                .ForMember(x => x.Stories, opt => opt.Ignore())
                .ForMember(x => x.Contracts, opt => opt.Ignore());

            CreateMap<Project, CustomerProjectOutput>();
            CreateMap<Project, CustomerProjectDetailsOutput>()
                .ForMember(x => x.Proposal, opt => opt.MapFrom(x => x.Proposal))
                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments.Where(c => c.Internal == false).OrderBy(c => c.Created)))
                .ForMember(x => x.Contracts, opt => opt.MapFrom(x => x.Contracts))
                .ForMember(x => x.Stories, opt => opt.MapFrom(x => x.Stories))
                .ForMember(x => x.PendingTimeEntries, opt => opt.Ignore());

            CreateMap<Project, ProjectInvoiceSummaryItem>().ForMember(x => x.ApprovedHours, opt => opt.MapFrom(x => x.TimeEntries.Where(z => z.Status == TimeStatus.Approved && z.InvoiceItemId == null).Sum(a => a.TotalHours)))
                .ForMember(x => x.ApprovedCustomerAmount, opt => opt.MapFrom(x => x.TimeEntries.Where(z => z.Status == TimeStatus.Approved && z.InvoiceItemId == null).Sum(a => a.TotalCustomerAmount)))

                .ForMember(x => x.ProjectName, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.ProjectAbbreviation, opt => opt.MapFrom(x => x.Abbreviation))
                .ForMember(x => x.ProjectId, opt => opt.MapFrom(x => x.Id))

                .ForMember(x => x.CustomerOrganizationId,
                    opt => opt.MapFrom(x => x.CustomerOrganizationId))
                .ForMember(x => x.CustomerId, opt => opt.MapFrom(x => x.CustomerId))
                .ForMember(x => x.CustomerName,
                    opt => opt.MapFrom(x => x.Customer.Person.DisplayName))
                .ForMember(x => x.CustomerImageUrl,
                    opt => opt.MapFrom(x => x.Customer.Person.ImageUrl))
                .ForMember(x => x.CustomerOrganizationName,
                    opt => opt.MapFrom(x => x.OrganizationCustomer.Organization.Name))
                .ForMember(x => x.CustomerOrganizationImageUrl,
                    opt => opt.MapFrom(x => x.OrganizationCustomer.Organization.ImageUrl))


                .IncludeAllDerived();

            CreateMap<Project, ProjectInvoiceDetails>()

                .ForMember(x => x.Contracts, opt => opt.MapFrom(x => x.Contracts));

            CreateEmailProjections();

        }
    }
}