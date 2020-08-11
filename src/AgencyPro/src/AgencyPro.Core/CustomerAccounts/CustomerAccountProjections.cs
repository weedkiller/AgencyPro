// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.CustomerAccounts.Models;
using AgencyPro.Core.CustomerAccounts.ViewModels;
using System.Linq;

namespace AgencyPro.Core.CustomerAccounts
{
    public class CustomerAccountProjections : Profile
    {
        public CustomerAccountProjections()
        {
            CreateMap<CustomerAccount, CustomerAccountOutput>()
                .ForMember(x => x.StatusTransitions, opt => opt.MapFrom(x => x.StatusTransitions.ToDictionary(a => a.Created, b => b.Status)))
                .ForMember(x=>x.AmountPaid, o=>o.MapFrom(x=>x.Invoices.Where(y=>y.Invoice.Status=="paid").Sum(z=>z.Invoice.AmountPaid)))
                .ForMember(x=>x.AmountDue, o=>o.MapFrom(x=>x.Invoices.Where(y=>y.Invoice.Status == "open").Sum(z=>z.Invoice.AmountDue)))
                .ForMember(x => x.IsInternal, o => o.MapFrom(x => x.IsInternal))
                .ForMember(x => x.IsCorporate, o => o.MapFrom(x => x.IsCorporate))
                .ForMember(x => x.PaymentTermId, o => o.MapFrom(x => x.PaymentTermId))
                .ForMember(x => x.PaymentTermName, o => o.MapFrom(x => x.PaymentTerm.Name))
                .ForMember(x => x.IsDeactivated, opt => opt.MapFrom(y => y.IsDeactivated))
                .ForMember(x => x.TotalProjects, opt => opt.MapFrom(x => x.Projects.Count))
                .ForMember(x => x.TotalInvoices, opt => opt.MapFrom(x => x.Invoices.Count))
                .ForMember(x => x.TotalContracts, opt => opt.MapFrom(x => x.Contracts.Count))
                .ForMember(x => x.AccountManagerOrganizationName, opt => opt.MapFrom(x => x.OrganizationAccountManager.Organization.Name))
                .ForMember(x => x.AccountManagerOrganizationImageUrl, opt => opt.MapFrom(x => x.OrganizationAccountManager.Organization.ImageUrl))
                .ForMember(x => x.CustomerOrganizationImageUrl, opt => opt.MapFrom(x => x.OrganizationCustomer.Organization.ImageUrl))
                .ForMember(x => x.CustomerOrganizationName, opt => opt.MapFrom(x => x.OrganizationCustomer.Organization.Name))
                .ForMember(x => x.AccountManagerName, opt => opt.MapFrom(x => x.AccountManager.Person.DisplayName))
                .ForMember(x => x.AccountManagerEmail, opt => opt.MapFrom(x => x.AccountManager.Person.ApplicationUser.Email))
                .ForMember(x => x.AccountManagerPhoneNumber, opt => opt.MapFrom(x => x.AccountManager.Person.ApplicationUser.PhoneNumber))
                .ForMember(x => x.AccountManagerImageUrl, opt => opt.MapFrom(x => x.AccountManager.Person.ImageUrl))
                .ForMember(x => x.CustomerImageUrl, opt => opt.MapFrom(x => x.Customer.Person.ImageUrl))
                .ForMember(x => x.CustomerName, opt => opt.MapFrom(x => x.Customer.Person.DisplayName))
                .ForMember(x => x.CustomerEmail, opt => opt.MapFrom(x => x.Customer.Person.ApplicationUser.Email))
                .ForMember(x => x.CustomerPhoneNumber, opt => opt.MapFrom(x => x.Customer.Person.ApplicationUser.PhoneNumber))
                .IncludeAllDerived();

            CreateMap<CustomerAccount, AgencyOwnerCustomerAccountOutput>();
            CreateMap<CustomerAccount, AgencyOwnerCustomerAccountDetailsOutput>()
                .ForMember(x=>x.ContractSummary, opt=>opt.MapFrom(x=>x.Contracts.GroupBy(y=>y.Status).ToDictionary(z=>z.Key, z=>z.Count())))
                .ForMember(x=>x.ProjectSummary, opt=>opt.MapFrom(x=>x.Projects.GroupBy(y=>y.Status).ToDictionary(z=>z.Key, z=>z.Count())))
                .ForMember(x=>x.WorkOrderSummary, opt=>opt.MapFrom(x=>x.WorkOrders.GroupBy(y=>y.Status).ToDictionary(z=>z.Key, z=>z.Count())))
                .ForMember(x=>x.InvoiceSummary, opt=>opt.MapFrom(x=>x.Invoices.GroupBy(y=>y.Invoice.Status).ToDictionary(z=>z.Key, z=>z.Count())))
                .ForMember(x => x.AvailablePaymentTerms, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.PaymentTerms))
                .ForMember(x => x.Invoices, opt => opt.MapFrom(x => x.Invoices))
                .ForMember(x => x.Projects, o => o.MapFrom(x => x.Projects))
                .ForMember(x => x.Comments, o => o.MapFrom(x => x.Comments.OrderBy(y => y.Created)))
                .ForMember(x => x.WorkOrders, o => o.MapFrom(x => x.WorkOrders))
                .ForMember(x => x.Contracts, opt => opt.MapFrom(x => x.Contracts));

            CreateMap<CustomerAccount, CustomerCustomerAccountOutput>();
            CreateMap<CustomerAccount, CustomerCustomerAccountDetailsOutput>()

                .ForMember(x => x.ContractSummary, opt => opt.MapFrom(x => x.Contracts.GroupBy(y => y.Status).ToDictionary(z => z.Key, z => z.Count())))
                .ForMember(x => x.ProjectSummary, opt => opt.MapFrom(x => x.Projects.GroupBy(y => y.Status).ToDictionary(z => z.Key, z => z.Count())))
                .ForMember(x => x.WorkOrderSummary, opt => opt.MapFrom(x => x.WorkOrders.GroupBy(y => y.Status).ToDictionary(z => z.Key, z => z.Count())))
                .ForMember(x => x.InvoiceSummary, opt => opt.MapFrom(x => x.Invoices.GroupBy(y => y.Invoice.Status).ToDictionary(z => z.Key, z => z.Count())))

                .ForMember(x => x.Invoices, opt => opt.MapFrom(x => x.Invoices))
                .ForMember(x => x.Comments, o => o.MapFrom(x => x.Comments.Where(a => a.Internal == false).OrderBy(y => y.Created)))

                .ForMember(x => x.WorkOrders, o => o.MapFrom(x => x.WorkOrders))
                .ForMember(x => x.Projects, o => o.MapFrom(x => x.Projects))
                .ForMember(x => x.Contracts, opt => opt.MapFrom(x => x.Contracts));

            CreateMap<CustomerAccount, AccountManagerCustomerAccountOutput>();
            CreateMap<CustomerAccount, AccountManagerCustomerAccountDetailsOutput>()
                .ForMember(x => x.Invoices, opt => opt.MapFrom(x => x.Invoices))
                .ForMember(x => x.Comments, o => o.MapFrom(x => x.Comments.OrderBy(y => y.Created)))

                .ForMember(x => x.ContractSummary, opt => opt.MapFrom(x => x.Contracts.GroupBy(y => y.Status).ToDictionary(z => z.Key, z => z.Count())))
                .ForMember(x => x.ProjectSummary, opt => opt.MapFrom(x => x.Projects.GroupBy(y => y.Status).ToDictionary(z => z.Key, z => z.Count())))
                .ForMember(x => x.WorkOrderSummary, opt => opt.MapFrom(x => x.WorkOrders.GroupBy(y => y.Status).ToDictionary(z => z.Key, z => z.Count())))
                .ForMember(x => x.InvoiceSummary, opt => opt.MapFrom(x => x.Invoices.GroupBy(y => y.Invoice.Status).ToDictionary(z => z.Key, z => z.Count())))


                .ForMember(x => x.WorkOrders, o => o.MapFrom(x => x.WorkOrders))
                .ForMember(x => x.Projects, o => o.MapFrom(x => x.Projects))
                .ForMember(x => x.Contracts, opt => opt.MapFrom(x => x.Contracts));
        }
    }
}