// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Invoices.Services;
using AgencyPro.Core.Invoices.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Projects.Extensions;
using AgencyPro.Core.Projects.Models;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Services.Invoices
{
    public class InvoiceProjectSummaryService : Service<Project>, IInvoiceProjectSummaryService
    {
        public InvoiceProjectSummaryService(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        public async Task<ProjectInvoiceSummary> GetProjectInvoiceSummary(IProviderAgencyOwner agencyOwner)
        {
            var projects = await Repository.Queryable()
                .ForAgencyOwner(agencyOwner)
                .ProjectTo<ProjectInvoiceSummaryItem>(ProjectionMapping)
                .ToListAsync();

            var projectInvoiceSummary = new ProjectInvoiceSummary()
            {
                Projects = projects
            };
            
            return projectInvoiceSummary;
        }

        public async Task<ProjectInvoiceDetails> GetProjectInvoiceDetails(IProviderAgencyOwner agencyOwner, Guid projectId)
        {
            var project = await Repository.Queryable()
                .ForAgencyOwner(agencyOwner)
                .Where(x=>x.Id == projectId)
                .ProjectTo<ProjectInvoiceDetails>(ProjectionMapping)
                .FirstAsync();

            return project;
        }
    }
}