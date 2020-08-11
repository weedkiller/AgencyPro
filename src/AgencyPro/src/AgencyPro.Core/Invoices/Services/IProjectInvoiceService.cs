// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Common;
using AgencyPro.Core.Invoices.Filters;
using AgencyPro.Core.Invoices.Models;
using AgencyPro.Core.Invoices.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Services;
using Stripe;

namespace AgencyPro.Core.Invoices.Services
{
    public interface IProjectInvoiceService : IService<ProjectInvoice>
    {
        Task<T> GetAsync<T>(string invoiceId) where T : ProjectInvoiceOutput, new();
        Task<T> GetInvoice<T>(IProviderAgencyOwner agencyOwner, string invoiceId)
            where T : AgencyOwnerProjectInvoiceOutput, new();

        Task<T> GetInvoice<T>(IOrganizationAccountManager am, string invoiceId)
            where T : AccountManagerProjectInvoiceOutput, new();

        Task<InvoiceResult> CreateInvoice(IProviderAgencyOwner agencyOwner, InvoiceInput input);

        Task<InvoiceResult> CreateInvoice(IOrganizationAccountManager am, InvoiceInput input);

        Task<InvoiceResult> FinalizeInvoice(IProviderAgencyOwner ao, string invoiceId);
        Task<InvoiceResult> VoidInvoice(IProviderAgencyOwner ao, string invoiceId);

        Task<InvoiceResult> FinalizeInvoice(IOrganizationAccountManager am, string invoiceId);

        Task<T> GetInvoice<T>(IOrganizationCustomer cu, string invoiceId) 
            where T : CustomerProjectInvoiceOutput;
        Task<PackedList<T>> GetInvoices<T>(IProviderAgencyOwner ao, InvoiceFilters filters) 
            where T : AgencyOwnerProjectInvoiceOutput,new();
        Task<PackedList<T>> GetInvoices<T>(IOrganizationCustomer cu, InvoiceFilters filters)
            where T : CustomerProjectInvoiceOutput, new();
        Task<PackedList<T>> GetInvoices<T>(IOrganizationAccountManager am, InvoiceFilters filters)
            where T : AccountManagerProjectInvoiceOutput, new();

        Task<InvoiceResult> DeleteInvoice(IProviderAgencyOwner agencyOwner, string invoiceId);
        Task<InvoiceResult> DeleteInvoice(IOrganizationAccountManager accountManager, string invoiceId);

        Task<InvoiceResult> SendInvoice(IProviderAgencyOwner agencyOwner, string invoiceId);
        Task<InvoiceResult> SendInvoice(IOrganizationAccountManager agencyOwner, string invoiceId);

        Task<InvoiceItemResult> InvoiceItemUpdated(InvoiceItem invoiceItem);
        //Task<int> ImportInvoices(int limit);
        Task<InvoiceItemResult> InvoiceItemCreated(InvoiceItem invoiceItem);
        Task<InvoiceItemResult> InvoiceItemDeleted(InvoiceItem invoiceItem);

        Task<InvoiceResult> InvoiceFinalized(Invoice invoice);
        Task<InvoiceResult> InvoiceSent(Invoice invoice);
        Task<InvoiceResult> InvoiceDeleted(Invoice invoice);
        Task<InvoiceResult> InvoiceUpdated(Invoice invoice);
        Task<InvoiceResult> InvoicePaymentSucceeded(Invoice invoice);

        Task<InvoiceResult> InvoiceCreated(Invoice invoice, string refNo);
    }
}