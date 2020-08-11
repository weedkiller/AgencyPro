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
        public async Task<StoryTemplateOutput> Create(IOrganizationAccountManager am, StoryTemplateInput input)
        {
            var template = new StoryTemplate()
            {
                ProviderOrganizationId = am.OrganizationId
            }.InjectFrom(input) as StoryTemplate;

            return await Create(template);
        }

        public async Task<StoryTemplateOutput> Create(IAgencyOwner ao, StoryTemplateInput input)
        {
            var template = new StoryTemplate()
            {
                ProviderOrganizationId = ao.OrganizationId
            }.InjectFrom(input) as StoryTemplate;

            return await Create(template);
        }

        public async Task<StoryTemplateOutput> Create(IOrganizationProjectManager pm, StoryTemplateInput input)
        {
            var template = new StoryTemplate()
            {
                ProviderOrganizationId = pm.OrganizationId
            }.InjectFrom(input) as StoryTemplate;

            return await Create(template);
        }

        private async Task<StoryTemplateOutput> Create(StoryTemplate template)
        {
            template.Created = DateTimeOffset.Now;

            var result = await Repository.InsertAsync(template, true);

            return await Repository.Queryable().Where(x => x.Id == template.Id)
                .ProjectTo<StoryTemplateOutput>(ProjectionMapping)
                .FirstAsync();
        }
    }
}