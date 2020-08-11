// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Stories.Services;
using AgencyPro.Core.StoryTemplates.Models;
using AgencyPro.Core.StoryTemplates.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Services.StoryTemplates
{
    public partial class StoryTemplateService
    {
        private async Task<StoryTemplateOutput> ConvertStory(IStory story, Organization organization)
        {

            var template = new StoryTemplate()
            {
                Description = story.Description,
                Hours = organization.ProviderOrganization.EstimationBasis * story.StoryPoints.GetValueOrDefault(),
                Title = story.Title,
                ProviderOrganizationId = organization.Id,
                StoryPoints = story.StoryPoints
            };

            return await Create(template);
        }

        public async Task<StoryTemplateOutput> ConvertStory(IAgencyOwner ao, Guid storyId,
            ConvertStoryInput input)
        {
            var story = await _stories.Queryable()
                .Where(x => x.Id == storyId && x.Project.ProjectManagerOrganizationId == ao.OrganizationId)
                .FirstAsync();

            var agency = await _organizations.Queryable()
                .Include(x=>x.ProviderOrganization)
                .Where(x => x.Id == ao.OrganizationId)
                .FirstOrDefaultAsync();

            return await ConvertStory(story, agency);

        }

        public async Task<StoryTemplateOutput> ConvertStory(IOrganizationAccountManager am, Guid storyId,
            ConvertStoryInput input)
        {
            var story = await _stories.Queryable()
                .Where(x => x.Id == storyId && x.Project.ProjectManagerOrganizationId == am.OrganizationId)
                .FirstAsync();

            var agency = await _organizations.Queryable()
                .Include(x => x.ProviderOrganization)
                .Where(x => x.Id == am.OrganizationId)
                .FirstOrDefaultAsync();

            return await ConvertStory(story, agency);
        }

        public async Task<StoryTemplateOutput> ConvertStory(IOrganizationProjectManager pm, Guid storyId,
            ConvertStoryInput input)
        {
            var story = await _stories.Queryable()
                .Where(x => x.Id == storyId && x.Project.ProjectManagerOrganizationId == pm.OrganizationId)
                .FirstAsync();

            var agency = await _organizations.Queryable()
                .Include(x => x.ProviderOrganization)
                .Where(x => x.Id == pm.OrganizationId)
                .FirstOrDefaultAsync();

            return await ConvertStory(story, agency);
        }
    }
}