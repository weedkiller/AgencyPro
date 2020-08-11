// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.StoryTemplates.Models;
using AgencyPro.Core.StoryTemplates.ViewModels;
using Microsoft.EntityFrameworkCore;
using Omu.ValueInjecter;

namespace AgencyPro.Services.StoryTemplates
{
    public partial class StoryTemplateService
    {
        public async Task<StoryTemplateOutput> Update(IOrganizationAccountManager am, Guid templateId,
            StoryTemplateInput input)
        {
            var template = await Repository.Queryable()
                .Where(x => x.Id == templateId && x.ProviderOrganizationId == am.OrganizationId)
                .FirstAsync();

            template.InjectFrom(input);

            return await Update(template);
        }

        public async Task<StoryTemplateOutput> Update(IAgencyOwner ao, Guid templateId, StoryTemplateInput input)
        {
            var template = await Repository.Queryable()
                .Where(x => x.Id == templateId && x.ProviderOrganizationId == ao.OrganizationId)
                .FirstAsync();

            template.InjectFrom(input);

            return await Update(template);
        }

        public async Task<StoryTemplateOutput> Update(IOrganizationProjectManager pm, Guid templateId,
            StoryTemplateInput input)
        {
            var template = await Repository.Queryable()
                .Where(x => x.Id == templateId && x.ProviderOrganizationId == pm.OrganizationId)
                .FirstAsync();

            template.InjectFrom(input);

            return await Update(template);
        }

        private async Task<StoryTemplateOutput> Update(StoryTemplate template)
        {
            template.Updated = DateTimeOffset.Now;

            var result =await Repository.UpdateAsync(template, true);

            return await Repository.Queryable()
                .Where(x => x.Id == template.Id)
                .ProjectTo<StoryTemplateOutput>(ProjectionMapping)
                .FirstAsync();
        }
    }
}