// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Agreements.Models;
using AgencyPro.Core.BonusIntents.Services;
using AgencyPro.Core.Common;
using AgencyPro.Core.Config;
using AgencyPro.Core.CustomerAccounts.Services;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.Extensions;
using AgencyPro.Core.Leads.Enums;
using AgencyPro.Core.Leads.Extensions;
using AgencyPro.Core.Leads.Models;
using AgencyPro.Core.Leads.Services;
using AgencyPro.Core.Leads.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.UserAccount.Models;
using AgencyPro.Core.UserAccount.Services;
using AgencyPro.Services.Leads.EventHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Leads
{
    public partial class LeadService : Service<Lead>, ILeadService
    {
        private readonly Lazy<IUserInfo> _userInfo;
        private readonly ICustomerAccountService _accountService;
        private readonly ILogger<LeadService> _logger;
        private readonly IIndividualBonusIntentService _individualBonusIntents;
        private readonly IOrganizationBonusIntentService _organizationBonusIntents;
        private readonly IOrganizationMarketerService _marketerService;
        private readonly IRepositoryAsync<MarketingAgreement> _marketingAgreements;
        private readonly IRepositoryAsync<Customer> _customers;
        private readonly IRepositoryAsync<ApplicationUser> _applicationUsers;
        private readonly IRepositoryAsync<Organization> _organizations;

        public LeadService(IServiceProvider unitOfWork,
            Lazy<IUserInfo> userInfo,
            ICustomerAccountService accountService,
            ILogger<LeadService> logger, 
            IIndividualBonusIntentService individualBonusIntents,
            IOrganizationBonusIntentService organizationBonusIntents,
            MultiLeadEventHandler multiHandler,
            IOrganizationMarketerService marketerService) : base(unitOfWork)
        {

            _userInfo = userInfo;
            _accountService = accountService;
            _logger = logger;
            _individualBonusIntents = individualBonusIntents;
            _organizationBonusIntents = organizationBonusIntents;
            _marketerService = marketerService;
            _marketingAgreements = UnitOfWork.RepositoryAsync<MarketingAgreement>();
            _customers = UnitOfWork.RepositoryAsync<Customer>();
            _applicationUsers = UnitOfWork.RepositoryAsync<ApplicationUser>();
            _organizations = UnitOfWork.RepositoryAsync<Organization>();

            AddEventHandler(multiHandler);
        }
        
        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(LeadService)}.{callerName}] - {message}";
        }

        public AuthSettings Settings { get; set; }

        public Task<PackedList<T>> GetLeads<T>(IMarketingAgencyOwner ao, CommonFilters filters) where T : AgencyOwnerLeadOutput
        {
            return Repository.Queryable()
                .ForMarketingAgencyOwner(ao)
                .Where(x => x.IsInternal == false)
                .OrderByDescending(x => x.Updated)
                .PaginateProjection<Lead, T>(filters, ProjectionMapping);
        }

        public async Task<PackedList<T>> GetLeads<T>(IOrganizationAccountManager am, CommonFilters filters) where T : AccountManagerLeadOutput
        {
            return await Repository.Queryable()
                .ForOrganizationAccountManager(am)
                .Where(x => x.Status == LeadStatus.Qualified)
                .OrderByDescending(x => x.Updated)
                .PaginateProjection<Lead, T>(filters, ProjectionMapping);
        }

        public Task<PackedList<T>> GetLeads<T>(IProviderAgencyOwner ao, CommonFilters filters) where T : AgencyOwnerLeadOutput
        {
            _logger.LogInformation(GetLogMessage("{ProviderAgencyOwner}"), ao.OrganizationId);

            return Repository.Queryable()
                .ForAgencyOwner(ao)
                .Where(x => x.Status == LeadStatus.New)
                .OrderByDescending(x => x.Updated)
                .PaginateProjection<Lead, T>(filters, ProjectionMapping);
        }

        public Task<int> MatchingLeadsByEmail(string email)
        {
            _logger.LogInformation(GetLogMessage("Email: {0}"),email);

            return Repository.Queryable().Where(x => x.EmailAddress == email).CountAsync();
        }

        public Task<int> CountLeadsPerProviderByEmail(Guid providerOrganizationId, string email)
        {
            return Repository.Queryable()
                .Where(x => x.ProviderOrganizationId == providerOrganizationId && x.EmailAddress == email)
                .CountAsync();
        }

        public Task<PackedList<T>> GetLeads<T>(IOrganizationMarketer ma, CommonFilters filters) where T : MarketerLeadOutput
        {
            return Repository.Queryable()
                .ForOrganizationMarketer(ma)
                .OrderByDescending(x => x.Updated)
                .PaginateProjection<Lead, T>(filters, ProjectionMapping);
        }

        public Task<T> GetAsync<T>(Guid leadId) where T : LeadOutput
        {
            return Repository.Queryable().Where(x => x.Id == leadId)
                .OrderByDescending(x => x.Updated)
                .ProjectTo<T>(ProjectionMapping).FirstAsync();
        }

        public Task<Lead> GetAsync(Guid leadId)
        {
            return Repository.Queryable().Where(x => x.Id == leadId).FirstAsync();
        }

        public Task<T> GetLead<T>(IOrganizationMarketer ma, Guid leadId) where T : MarketerLeadOutput
        {
            return Repository.Queryable()
                .ForOrganizationMarketer(ma)
                .Where(x => x.Id == leadId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstAsync();
        }

        public Task<T> GetLead<T>(IOrganizationAccountManager organizationAccountManager, Guid leadId)
            where T : AccountManagerLeadOutput
        {
            return Repository.Queryable()
                .Where(x => x.Id == leadId && x.AccountManagerId == organizationAccountManager.AccountManagerId && x.AccountManagerOrganizationId == organizationAccountManager.OrganizationId)
                .ForOrganizationAccountManager(organizationAccountManager)
                .ProjectTo<T>(ProjectionMapping)
                .FirstAsync();
        }

        public Task<T> GetLead<T>(IProviderAgencyOwner agencyOwner, Guid leadId) where T : AgencyOwnerLeadOutput
        {
            return Repository.Queryable()
                .Where(x => x.Id == leadId && x.ProviderOrganizationId == agencyOwner.OrganizationId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstAsync();
        }

        public Task<T> GetLead<T>(IMarketingAgencyOwner agencyOwner, Guid leadId) where T : AgencyOwnerLeadOutput
        {
            return Repository.Queryable()
                .Where(x => x.Id == leadId && x.MarketerOrganizationId == agencyOwner.OrganizationId && x.IsInternal == false)
                .ProjectTo<T>(ProjectionMapping)
                .FirstAsync();
        }

        public async Task<T> GetLead<T>(Expression<Func<Lead, bool>> filter) where T : LeadOutput
        {
            var entity = await Repository.Queryable()
                .Where(filter)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();

            return entity;
        }

        public async Task<T> GetAsync<T>(Expression<Func<Lead, bool>> filter) where T : LeadOutput
        {
            var entity = await Repository.Queryable()
                .Where(filter)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();

            return entity;
        }

        public async Task<T> GetLead<T>(Guid leadId) where T : LeadOutput
        {
            return await GetLead<T>(p => p.Id == leadId);
        }
        
    }
}