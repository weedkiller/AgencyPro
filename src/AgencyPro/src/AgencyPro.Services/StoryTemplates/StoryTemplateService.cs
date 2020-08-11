// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.StoryTemplates.Models;
using AgencyPro.Core.StoryTemplates.Services;
using AgencyPro.Core.StoryTemplates.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Stories.Models;
using AgencyPro.Core.Common;
using AgencyPro.Core.Extensions;

namespace AgencyPro.Services.StoryTemplates
{
    public partial class StoryTemplateService : Service<StoryTemplate>, IStoryTemplateService
    {
        private readonly IRepositoryAsync<Story> _stories;
        private readonly IRepositoryAsync<Organization> _organizations;

        public StoryTemplateService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _stories = UnitOfWork.RepositoryAsync<Story>();
            _organizations = UnitOfWork.RepositoryAsync<Organization>();
        }

        public Task<StoryTemplateOutput> GetStoryTemplate(IOrganizationAccountManager am, Guid templateId)
        {
            return Repository.Queryable()
                .Where(x => x.ProviderOrganizationId == am.OrganizationId && x.Id == templateId)
                .ProjectTo<StoryTemplateOutput>(ProjectionMapping)
                .FirstAsync();
        }

        public Task<PackedList<StoryTemplateOutput>> GetStoryTemplates(IOrganizationAccountManager am, CommonFilters filters)
        {
            return Repository.Queryable()
                .Where(x => x.ProviderOrganizationId == am.OrganizationId)
                .PaginateProjection<StoryTemplate, StoryTemplateOutput>(filters, ProjectionMapping);
        }

        public Task<StoryTemplateOutput> GetStoryTemplate(IAgencyOwner ao, Guid templateId)
        {
            return Repository.Queryable()
                .Where(x => x.ProviderOrganizationId == ao.OrganizationId && x.Id == templateId)
                .ProjectTo<StoryTemplateOutput>(ProjectionMapping)
                .FirstAsync();
        }

        public Task<PackedList<StoryTemplateOutput>> GetStoryTemplates(IAgencyOwner ao, CommonFilters filters)
        {
            return Repository.Queryable()
                .Where(x => x.ProviderOrganizationId == ao.OrganizationId)
                .PaginateProjection<StoryTemplate, StoryTemplateOutput>(filters, ProjectionMapping);
        }

        public Task<StoryTemplateOutput> GetStoryTemplate(IOrganizationProjectManager pm, Guid templateId)
        {
            return Repository.Queryable()
                .Where(x => x.ProviderOrganizationId == pm.OrganizationId && x.Id == templateId)
                .ProjectTo<StoryTemplateOutput>(ProjectionMapping)
                .FirstAsync();
        }

        public Task<PackedList<StoryTemplateOutput>> GetStoryTemplates(IOrganizationProjectManager pm, CommonFilters filters)
        {
            return Repository.Queryable()
                .Where(x => x.ProviderOrganizationId == pm.OrganizationId)
                .PaginateProjection<StoryTemplate, StoryTemplateOutput>(filters, ProjectionMapping);
        }
    }
}
