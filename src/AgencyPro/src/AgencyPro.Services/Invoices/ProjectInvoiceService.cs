// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Projects.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AgencyPro.Core.BuyerAccounts.Services;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.Invoices.Extensions;
using AgencyPro.Core.Invoices.Filters;
using AgencyPro.Core.Invoices.Models;
using AgencyPro.Core.Invoices.Services;
using AgencyPro.Core.Invoices.ViewModels;
using AgencyPro.Core.Stripe.Model;
using AgencyPro.Core.TimeEntries.Models;
using AgencyPro.Services.Invoices.Messaging;
using Stripe;
using StripeInvoiceService = Stripe.InvoiceService;
using AgencyPro.Core.Common;
using AgencyPro.Core.Extensions;

namespace AgencyPro.Services.Invoices
{
    public partial class ProjectInvoiceService : Service<ProjectInvoice>, IProjectInvoiceService
    {
        private readonly StripeInvoiceService _invoiceService;
        private readonly InvoiceItemService _invoiceItemService;
        private readonly IRepositoryAsync<StripeInvoiceItem> _items;
        private readonly IRepositoryAsync<StripeInvoice> _invoices;
        private readonly IRepositoryAsync<TimeEntry> _timeEntries;
        private readonly IRepositoryAsync<Contract> _contracts;

        private readonly ILogger<ProjectInvoiceService> _logger;
        private readonly IProjectService _projectService;
        private readonly IBuyerAccountService _buyerAccountService;

        public ProjectInvoiceService(
            IServiceProvider serviceProvider,
            StripeInvoiceService invoiceService,
            InvoiceItemService invoiceItemService,
            ILogger<ProjectInvoiceService> logger,
            InvoicesEventHandlers events,
            IProjectService projectService,
            IBuyerAccountService buyerAccountService) : base(serviceProvider)
        {
            _invoiceService = invoiceService;
            _invoiceItemService = invoiceItemService;
            _logger = logger;
            _projectService = projectService;
            _buyerAccountService = buyerAccountService;
            _invoices = UnitOfWork.RepositoryAsync<StripeInvoice>();
            _timeEntries = UnitOfWork.RepositoryAsync<TimeEntry>();
            _items = UnitOfWork.RepositoryAsync<StripeInvoiceItem>();
            _contracts = UnitOfWork.RepositoryAsync<Contract>();

            AddEventHandler(events);
        }

        public Task<PackedList<T>> GetInvoices<T>(IProviderAgencyOwner ao, InvoiceFilters filters)
            where T : AgencyOwnerProjectInvoiceOutput,new()
        {
            return Repository.Queryable()
                .ForAgencyOwner(ao)
                .ApplyWhereFilters(filters)
                .PaginateProjection<ProjectInvoice, T>(filters, ProjectionMapping);
        }

        public Task<T> GetInvoice<T>(IOrganizationCustomer organizationCustomer, string invoiceId)
            where T : CustomerProjectInvoiceOutput
        {
            return Repository.Queryable()
                .ForOrganizationCustomer(organizationCustomer)
                .Where(x=>x.InvoiceId == invoiceId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<PackedList<T>> GetInvoices<T>(IOrganizationCustomer cu, InvoiceFilters filters)
            where T : CustomerProjectInvoiceOutput, new()
        {
            return Repository.Queryable()
                .Include(x => x.Invoice)
                .ForOrganizationCustomer(cu)
                .Where(x => x.Invoice.Status == "open" || x.Invoice.Status == "paid")
                .ApplyWhereFilters(filters)
                .PaginateProjection<ProjectInvoice, T>(filters, ProjectionMapping);
        }

        public Task<PackedList<T>> GetInvoices<T>(IOrganizationAccountManager am,
            InvoiceFilters filters)
            where T : AccountManagerProjectInvoiceOutput, new()
        {
            return Repository.Queryable()
                 .ForOrganizationAccountManager(am)
                 .ApplyWhereFilters(filters)
                 .PaginateProjection<ProjectInvoice, T>(filters, ProjectionMapping);
        }

        public Task<T> GetAsync<T>(string invoiceId) where T : ProjectInvoiceOutput, new()
        {
            return Repository.Queryable()
                .Where(x => x.InvoiceId == invoiceId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetInvoice<T>(IProviderAgencyOwner agencyOwner, string invoiceId)
            where T : AgencyOwnerProjectInvoiceOutput, new()
        {
            return Repository.Queryable()
                .ForAgencyOwner(agencyOwner)
                .Where(x => x.InvoiceId == invoiceId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetInvoice<T>(IOrganizationAccountManager am, string invoiceId)
            where T : AccountManagerProjectInvoiceOutput, new()
        {
            return Repository.Queryable()
                .ForOrganizationAccountManager(am)
                .Where(x => x.InvoiceId == invoiceId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(ProjectInvoiceService)}.{callerName}] - {message}";
        }
    }
}