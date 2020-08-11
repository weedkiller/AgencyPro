// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Common;
using AgencyPro.Core.CustomerAccounts.Services;
using AgencyPro.Core.Extensions;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Projects.Extensions;
using AgencyPro.Core.Projects.Filters;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.Projects.Services;
using AgencyPro.Core.Projects.ViewModels;
using AgencyPro.Core.Proposals.Enums;
using AgencyPro.Core.UserAccount.Services;
using AgencyPro.Services.Projects.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Projects
{
    public partial class ProjectService : Service<Project>, IProjectService
    {
        private readonly ICustomerAccountService _accountService;
        private readonly IUserInfo _userInfo;
        private readonly ILogger<ProjectService> _logger;
        private readonly IOrganizationProjectManagerService _pmService;

        public ProjectService(IServiceProvider serviceProvider,
            ICustomerAccountService accountService,
            IUserInfo userInfo,
            ILogger<ProjectService> logger,
            MultiProjectEventHandler events,
            IOrganizationProjectManagerService pmService) : base(serviceProvider)
        {
            _accountService = accountService;
            _userInfo = userInfo;
            _logger = logger;
            _pmService = pmService;

            AddEventHandler(events);
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(ProjectService)}.{callerName}] - {message}";
        }

        public Task<PackedList<T>> GetProjects<T>(IOrganizationCustomer cu, ProjectFilters filters)
            where T : CustomerProjectOutput, new()
        {
            return Repository.Queryable()
                .ForOrganizationCustomer(cu)
                .Where(x => x.Proposal != null && x.Proposal.Status == ProposalStatus.Accepted)
                .ApplyWhereFilters(filters)
                .OrderByDescending(x => x.Updated)
                .PaginateProjection<Project, T>(filters, ProjectionMapping);
        }

        public Task<PackedList<T>> GetProjects<T>(IProviderAgencyOwner ao, ProjectFilters filters)
            where T : AgencyOwnerProjectOutput, new()
        {
            return Repository.Queryable()
                .ForAgencyOwner(ao)
                .ApplyWhereFilters(filters)
                .OrderByDescending(x => x.Updated)
                .PaginateProjection<Project, T>(filters, ProjectionMapping);
        }

        public Task<T> GetProject<T>(Guid projectId) where T : ProjectOutput,new()
        {
            return Repository.Queryable()
                .FindById(projectId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstAsync();
        }

        public Task<PackedList<T>> GetProjects<T>(IOrganizationAccountManager am, ProjectFilters filters)
            where T : AccountManagerProjectOutput, new()
        {
            return Repository.Queryable()
                .ForOrganizationAccountManager(am)
                .ApplyWhereFilters(filters)
                .OrderByDescending(x => x.Updated)
                .PaginateProjection<Project, T>(filters, ProjectionMapping);
        }

        public Task<T> GetProject<T>(IProviderAgencyOwner agencyOwner, Guid projectId) where T : AgencyOwnerProjectOutput, new()
        {
            return Repository.Queryable()
                .ForAgencyOwner(agencyOwner)
                .FindById(projectId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetProject<T>(IOrganizationAccountManager am, Guid projectId) where T : AccountManagerProjectOutput, new()
        {
            return Repository.Queryable()
                .ForOrganizationAccountManager(am)
                .FindById(projectId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetProject<T>(IOrganizationProjectManager organizationProjectManager, Guid projectId)
            where T : ProjectManagerProjectOutput, new()
        {
            return Repository.Queryable()
                .ForOrganizationProjectManager(organizationProjectManager)
                .FindById(projectId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetProject<T>(IOrganizationContractor co, Guid projectId) where T : ContractorProjectOutput, new()
        {
            return Repository.Queryable()
                .ForOrganizationContractor(co)
                .FindById(projectId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetProject<T>(IOrganizationCustomer organizationCustomer, Guid projectId) where T : CustomerProjectOutput, new()
        {
            return Repository.Queryable()
                .ForOrganizationCustomer(organizationCustomer)
                .FindById(projectId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }
        

        public Task<List<T>> GetProjects<T>(IProviderAgencyOwner owner, Guid[] uniqueProjectIds) where T : AgencyOwnerProjectOutput, new()
        {
            return Repository.Queryable()
                .ForAgencyOwner(owner)
                .Where(x=>uniqueProjectIds.Contains(x.Id))
                .OrderByDescending(x => x.Updated)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<List<T>> GetProjects<T>(IOrganizationCustomer cu, Guid[] uniqueProjectIds) where T : CustomerProjectOutput, new()
        {
            return Repository.Queryable()
                .ForOrganizationCustomer(cu)
                .Where(x => uniqueProjectIds.Contains(x.Id))
                .OrderByDescending(x => x.Updated)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<List<T>> GetProjects<T>(IOrganizationAccountManager am, Guid[] uniqueProjectIds) where T : AccountManagerProjectOutput, new()
        {
            return Repository.Queryable()
                .ForOrganizationAccountManager(am)
                .Where(x => uniqueProjectIds.Contains(x.Id))
                .OrderByDescending(x => x.Updated)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<List<T>> GetProjects<T>(IOrganizationProjectManager pm, Guid[] uniqueProjectIds) where T : ProjectManagerProjectOutput, new()
        {
            return Repository.Queryable()
                .ForOrganizationProjectManager(pm)
                .Where(x => uniqueProjectIds.Contains(x.Id))
                .OrderByDescending(x => x.Updated)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<List<T>> GetProjects<T>(IOrganizationContractor co, Guid[] uniqueProjectIds) where T : ContractorProjectOutput, new()
        {
            return Repository.Queryable()
                .ForOrganizationContractor(co)
                .Where(x => uniqueProjectIds.Contains(x.Id))
                .OrderByDescending(x => x.Updated)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }


        public Task<PackedList<T>> GetProjects<T>(IOrganizationContractor co, ProjectFilters filters) where T : ContractorProjectOutput, new()
        {
            return Repository.Queryable()
                .ForOrganizationContractor(co)
                .ApplyWhereFilters(filters)
                .OrderByDescending(x => x.Updated)
                .PaginateProjection<Project, T>(filters, ProjectionMapping);
        }

        public Task<PackedList<T>> GetProjects<T>(IOrganizationProjectManager pm, ProjectFilters filters)
            where T : ProjectManagerProjectOutput, new()
        {
            return Repository.Queryable()
                .ForOrganizationProjectManager(pm)
                .ApplyWhereFilters(filters)
                .OrderByDescending(x => x.Updated)
                .PaginateProjection<Project, T>(filters, ProjectionMapping);
        }
    }
}