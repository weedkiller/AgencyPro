// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Common;
using AgencyPro.Core.Contracts.Services;
using AgencyPro.Core.Extensions;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Stories.Services;
using AgencyPro.Core.TimeEntries.Extensions;
using AgencyPro.Core.TimeEntries.Models;
using AgencyPro.Core.TimeEntries.Services;
using AgencyPro.Core.TimeEntries.ViewModels;
using AgencyPro.Core.UserAccount.Services;
using AgencyPro.Services.Stories.EmailNotifications;
using AgencyPro.Services.TimeEntries.EventHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.TimeEntries
{
    public partial class TimeEntryService : Service<TimeEntry>, ITimeEntryService
    {
        private readonly IStoryService _storyService;
        private readonly IContractService _contractService;
        private readonly IUserInfo _userInfo;
        private readonly ILogger<TimeEntryService> _logger;
        
        public TimeEntryService(
            IStoryService storyService,
            IServiceProvider serviceProvider,
            IContractService contractService,
            IUserInfo userInfo,
            TimeEntryEventHandlers timeEntryEvents,
            StoryEventHandlers storyEvents,
            ILogger<TimeEntryService> logger
        ) : base(serviceProvider)
        {
            _storyService = storyService;
            _contractService = contractService;
            _userInfo = userInfo;
            _logger = logger;

            AddEventHandler(timeEntryEvents, storyEvents);
        }
        
        public Task<PackedList<T>> GetTimeEntries<T>(IOrganizationContractor co, TimeMatrixFilters filters)
            where T : ContractorTimeEntryOutput
        {
            return Repository.Queryable()
                .ForOrganizationContractor(co)
                .ApplyWhereFilters(filters)
                .PaginateProjection<TimeEntry, T>(filters, ProjectionMapping);
        }

        public Task<PackedList<T>> GetTimeEntries<T>(IOrganizationProjectManager pm, TimeMatrixFilters filters)
            where T : ProjectManagerTimeEntryOutput
        {
            return Repository.Queryable()
                .ForOrganizationProjectManager(pm)
                .ApplyWhereFilters(filters)
                .PaginateProjection<TimeEntry, T>(filters, ProjectionMapping);
        }

        public Task<PackedList<T>> GetTimeEntries<T>(IOrganizationAccountManager am, TimeMatrixFilters filters)
            where T : AccountManagerTimeEntryOutput
        {
            return Repository.Queryable()
                .ForOrganizationAccountManager(am)
                .ApplyWhereFilters(filters)
                .PaginateProjection<TimeEntry, T>(filters, ProjectionMapping);
        }

        public Task<List<T>> GetTimeEntries<T>(IOrganizationCustomer cu, TimeMatrixFilters filters)
            where T : CustomerTimeEntryOutput
        {
            return Repository.Queryable()
                .ForOrganizationCustomer(cu)
                .ApplyWhereFilters(filters)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<PackedList<T>> GetTimeEntries<T>(IAgencyOwner ao, TimeMatrixFilters filters)
            where T : ProviderAgencyOwnerTimeEntryOutput
        {
            return Repository.Queryable()
                .ForAgencyOwner(ao)
                .ApplyWhereFilters(filters)
                .PaginateProjection<TimeEntry, T>(filters, ProjectionMapping);
        }

        public Task<List<T>> GetTimeEntries<T>(IOrganizationMarketer ma, TimeMatrixFilters filters)
            where T : MarketerTimeEntryOutput
        {
            return Repository.Queryable()
                .ForOrganizationMarketer(ma)
                .ApplyWhereFilters(filters)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<List<T>> GetTimeEntries<T>(IOrganizationRecruiter re, TimeMatrixFilters filters)
            where T : RecruiterTimeEntryOutput
        {
            return Repository.Queryable()
                .ForOrganizationRecruiter(re)
                .ApplyWhereFilters(filters)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public async Task<T> GetTimeEntry<T>(IOrganizationContractor contractor, Guid entryId) where T : ContractorTimeEntryOutput
        {
            return await Repository.Queryable()
                .FindById(entryId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public async Task<T> GetTimeEntry<T>(IOrganizationCustomer customer, Guid entryId) where T : CustomerTimeEntryOutput
        {
            return await Repository.Queryable()
                .FindById(entryId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public async Task<T> GetTimeEntry<T>(IOrganizationAccountManager am, Guid entryId) where T : AccountManagerTimeEntryOutput
        {
            return await Repository.Queryable()
                .FindById(entryId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public async Task<T> GetTimeEntry<T>(IAgencyOwner ao, Guid entryId) where T : ProviderAgencyOwnerTimeEntryOutput
        {
            return await Repository.Queryable()
                .FindById(entryId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public async Task<T> GetTimeEntry<T>(IOrganizationProjectManager pm, Guid entryId) where T : ProjectManagerTimeEntryOutput
        {
            return await Repository.Queryable()
                .FindById(entryId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public async Task<T> GetTimeEntry<T>(IOrganizationRecruiter re, Guid entryId) where T : RecruiterTimeEntryOutput
        {
            return await Repository.Queryable()
                .FindById(entryId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public async Task<T> GetTimeEntry<T>(IOrganizationMarketer ma, Guid entryId) where T : MarketerTimeEntryOutput
        {
            return await Repository.Queryable()
                .FindById(entryId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public async Task<T> GetTimeEntry<T>(Guid id)
            where T : TimeEntryOutput, new()
        {
            return await Repository.Queryable()
                .FindById(id)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }
    }
}