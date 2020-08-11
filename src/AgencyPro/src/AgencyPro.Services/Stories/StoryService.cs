// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Common;
using AgencyPro.Core.Extensions;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Projects.Services;
using AgencyPro.Core.Stories.Extensions;
using AgencyPro.Core.Stories.Filters;
using AgencyPro.Core.Stories.Models;
using AgencyPro.Core.Stories.Services;
using AgencyPro.Core.Stories.ViewModels;
using AgencyPro.Services.Stories.EmailNotifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Stories
{
    public partial class StoryService : Service<Story>, IStoryService
    {
        private readonly ILogger<StoryService> _logger;
        private readonly IProjectService _projectService;

        public StoryService(IServiceProvider serviceProvider,
            ILogger<StoryService> logger,
            StoryEventHandlers events,
            IProjectService projectService) : base(serviceProvider)
        {
            _logger = logger;
            _projectService = projectService;

            AddEventHandler(events);
        }

        public Task<T> GetStory<T>(IOrganizationProjectManager pm, Guid storyId)
            where T : ProjectManagerStoryOutput
        {
            return Repository.Queryable()
                .ForOrganizationProjectManager(pm)
                .FindById(storyId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetStory<T>(IOrganizationContractor co, Guid storyId) 
            where T : ContractorStoryOutput
        {
            return Repository.Queryable()
                .ForOrganizationContractor(co)
                .FindById(storyId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetStory<T>(IOrganizationAccountManager am, Guid storyId)
            where T : AccountManagerStoryOutput
        {
            return Repository.Queryable()
                .ForOrganizationAccountManager(am)
                .FindById(storyId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetStory<T>(IProviderAgencyOwner ao, Guid storyId)
            where T : AgencyOwnerStoryOutput
        {
            return Repository.Queryable()
                .FindById(storyId)
                .ForAgencyOwner(ao)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<PackedList<T>> GetStories<T>(IOrganizationProjectManager pm, StoryFilters filters
        ) where T : ProjectManagerStoryOutput
        {
            return Repository.Queryable().ForOrganizationProjectManager(pm)
                .ApplyWhereFilters(filters)
                .PaginateProjection<Story, T>(filters, ProjectionMapping);
        }

        public Task<PackedList<T>> GetStories<T>(IOrganizationContractor co, StoryFilters filters)
            where T : ContractorStoryOutput
        {
            return Repository.Queryable().ForOrganizationContractor(co)
                .ApplyWhereFilters(filters)
                .PaginateProjection<Story, T>(filters, ProjectionMapping);
        }

        public Task<PackedList<T>> GetStories<T>(IOrganizationCustomer cu, StoryFilters filters) 
            where T : CustomerStoryOutput
        {
            return Repository.Queryable().ForOrganizationCustomer(cu)
                .ApplyWhereFilters(filters)
                .PaginateProjection<Story, T>(filters, ProjectionMapping);
        }

        public Task<PackedList<T>> GetStories<T>(IOrganizationAccountManager am, StoryFilters filters
        ) where T : AccountManagerStoryOutput
        {
            return Repository.Queryable().ForOrganizationAccountManager(am)
                .ApplyWhereFilters(filters)
                .PaginateProjection<Story, T>(filters, ProjectionMapping);
        }

        public Task<PackedList<T>> GetStories<T>(IProviderAgencyOwner ao, StoryFilters filters
        ) where T : AgencyOwnerStoryOutput
        {
            return Repository.Queryable().ForAgencyOwner(ao)
                .ApplyWhereFilters(filters)
                .PaginateProjection<Story, T>(filters, ProjectionMapping);
        }

        public Task<List<T>> GetStories<T>(IProviderAgencyOwner owner, Guid?[] uniqueStoryIds) 
            where T : AgencyOwnerStoryOutput
        {
            return Repository.Queryable()
                .ForAgencyOwner(owner)
                .Where(x=>uniqueStoryIds.Contains(x.Id))
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<List<T>> GetStories<T>(IOrganizationAccountManager am, Guid?[] uniqueStoryIds) 
            where T : AccountManagerStoryOutput
        {
            return Repository.Queryable()
                .ForOrganizationAccountManager(am)
                .Where(x=>uniqueStoryIds.Contains(x.Id))
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<List<T>> GetStories<T>(IOrganizationProjectManager pm, Guid?[] uniqueStoryIds) 
            where T : ProjectManagerStoryOutput
        {
            return Repository.Queryable()
                .ForOrganizationProjectManager(pm)
                .Where(x => uniqueStoryIds.Contains(x.Id))
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<List<T>> GetStories<T>(IOrganizationCustomer cu, Guid?[] uniqueStoryIds) 
            where T : CustomerStoryOutput
        {
            return Repository.Queryable()
                .ForOrganizationCustomer(cu)
                .Where(x => uniqueStoryIds.Contains(x.Id))
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<List<T>> GetStories<T>(IOrganizationContractor co, Guid?[] uniqueStoryIds) 
            where T : ContractorStoryOutput
        {
            return Repository.Queryable()
                .ForOrganizationContractor(co)
                .Where(x => uniqueStoryIds.Contains(x.Id))
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<T> GetStory<T>(IOrganizationCustomer cu, Guid storyId)
            where T : CustomerStoryOutput
        {
            return Repository.Queryable()
                .FindById(storyId)
                .ForOrganizationCustomer(cu)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }


        public Task<T> GetStory<T>(Guid id)
            where T : StoryOutput
        {
            return Repository.Queryable()
                .FindById(id)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }
    }
}