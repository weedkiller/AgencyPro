// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.Leads.Enums;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Models;
using System.Linq;
using AgencyPro.Core.Orders.Model;
using AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels;
using AgencyPro.Core.Proposals.Enums;

namespace AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationAccountManagers
{
    public class OrganizationAccountManagerProjections : Profile
    {
        public OrganizationAccountManagerProjections()
        {
            CreateMap<OrganizationAccountManager, OrganizationAccountManagerOutput>()
                .ForMember(x => x.DisplayName, opt => opt.MapFrom(x => x.AccountManager.Person.DisplayName))
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x => x.AccountManager.Person.ImageUrl))
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.AccountManager.Person.ApplicationUser.Email))
                .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(x => x.AccountManager.Person.ApplicationUser.PhoneNumber))
                .ForMember(x => x.Status, opt => opt.MapFrom(x => x.OrganizationPerson.Status))
                .IncludeAllDerived();

            CreateMap<OrganizationAccountManager, OrganizationAccountManagerStatistics>()
                .ForMember(x => x.TotalAccounts, opt => opt.MapFrom(x => x.Accounts.Count))
                .ForMember(x => x.TotalContracts, opt => opt.MapFrom(x => x.Contracts.Count))
                .ForMember(x => x.TotalProjects, opt => opt.MapFrom(x => x.Projects.Count))
                .ForMember(x => x.TotalLeads, opt => opt.MapFrom(x => x.Leads.Count(y => y.Status == LeadStatus.Qualified)))
                .ForMember(x => x.MaxWeeklyHours, opt => opt.MapFrom(x => x.Contracts.Sum(a => a.MaxWeeklyHours)))
                .IncludeAllDerived();

            CreateMap<OrganizationAccountManager, AccountManagerOrganizationAccountManagerDetailsOutput>()
                .ForMember(x => x.Accounts, opt => opt.MapFrom(x => x.Accounts))
                .ForMember(x => x.Leads, opt => opt.MapFrom(x => x.Leads.Where(y => y.Status == LeadStatus.Qualified).ToList()));

            CreateMap<OrganizationAccountManager, AgencyOwnerOrganizationAccountManagerDetailsOutput>()
                .ForMember(x => x.Accounts, opt => opt.MapFrom(x => x.Accounts))
                .ForMember(x => x.Leads, opt => opt.MapFrom(x => x.Leads.Where(y => y.Status == LeadStatus.Qualified).ToList()));

            CreateMap<OrganizationAccountManager, ContractorOrganizationAccountManagerOutput>()
                .IncludeAllDerived();

            CreateMap<OrganizationAccountManager, CustomerOrganizationAccountManagerOutput>()
                .IncludeAllDerived();

            CreateMap<OrganizationAccountManager, AgencyOwnerOrganizationAccountManagerOutput>()
                .IncludeAllDerived();

            CreateMap<OrganizationAccountManager, AccountManagerOrganizationAccountManagerOutput>()
                .IncludeAllDerived();

            CreateMap<OrganizationAccountManager, ProjectManagerOrganizationAccountManagerOutput>()
                .IncludeAllDerived();

            CreateMap<OrganizationAccountManager, RecruiterOrganizationAccountManagerOutput>()
                .IncludeAllDerived();

            CreateMap<OrganizationAccountManager, AccountManagerCounts>()
                .ForMember(x => x.Accounts, opt => opt.MapFrom(x => x.Accounts.Count))
                .ForMember(x => x.WorkOrders, opt => opt.MapFrom(x => x.Accounts.SelectMany(y => y.WorkOrders.Where(z=>z.Status== OrderStatus.Sent || z.Status == OrderStatus.AwaitingProposal )).Count()))
                .ForMember(x => x.Leads, opt => opt.MapFrom(x => x.Leads.Count(y => y.Status == LeadStatus.Qualified)))
                .ForMember(x => x.Projects, opt => opt.MapFrom(x => x.Projects.Count))
                .ForMember(x => x.People, opt => opt.MapFrom(x => x.Organization.OrganizationPeople.Count))
                .ForMember(x => x.Stories, opt => opt.MapFrom(x => x.Projects.SelectMany(y => y.Stories).Count()))
                .ForMember(x => x.Invoices, opt => opt.MapFrom(x => x.Projects.SelectMany(y => y.Invoices).Count()))
                .ForMember(x => x.Proposals, opt => opt.MapFrom(x => x.Projects.Count(y => y.Proposal != null && y.Proposal.Status != ProposalStatus.Rejected)))
                .ForMember(x => x.Contracts, opt => opt.MapFrom(x => x.Contracts.Count));


            CreateMap<OrganizationAccountManager, AccountManagerOrganizationOutput>()
                .IncludeMembers(x => x.Organization)
                .ForMember(x => x.AccountManagerStream, o => o.MapFrom(x => x.AccountManagerStream))
                .IncludeAllDerived();

        }
    }
}