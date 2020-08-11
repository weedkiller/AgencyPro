// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.StoryTemplates.Models;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Services.StoryTemplates
{
    public partial class StoryTemplateService
    {
        public async Task<bool> Delete(StoryTemplate template)
        {
            template.IsDeleted = true;

            var response = await Repository.UpdateAsync(template, true);

            return response!=0;
        }

        public async Task<bool> Delete(IOrganizationAccountManager am, Guid templateId)
        {
            var template = await Repository.Queryable()
                .Where(x => x.ProviderOrganizationId == am.OrganizationId && x.Id == templateId)
                .FirstAsync();

            return await Delete(template);
        }

        public async Task<bool> Delete(IAgencyOwner ao, Guid templateId)
        {
            var template = await Repository.Queryable()
                .Where(x => x.ProviderOrganizationId == ao.OrganizationId && x.Id == templateId)
                .FirstAsync();

            return await Delete(template);
        }

        public async Task<bool> Delete(IOrganizationProjectManager pm, Guid templateId)
        {
            var template = await Repository.Queryable()
                .Where(x => x.ProviderOrganizationId == pm.OrganizationId && x.Id == templateId)
                .FirstAsync();

            return await Delete(template);
        }
    }
}