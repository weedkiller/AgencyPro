// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Common;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.StoryTemplates.ViewModels;

namespace AgencyPro.Core.StoryTemplates.Services
{
    public interface IStoryTemplateService
    {
        Task<StoryTemplateOutput> GetStoryTemplate(IOrganizationAccountManager am, Guid templateId);
        Task<bool> Delete(IOrganizationAccountManager am, Guid templateId);
        Task<PackedList<StoryTemplateOutput>> GetStoryTemplates(IOrganizationAccountManager am, CommonFilters filters);

        Task<StoryTemplateOutput> GetStoryTemplate(IAgencyOwner ao, Guid templateId);
        Task<bool> Delete(IAgencyOwner ao, Guid templateId);
        Task<PackedList<StoryTemplateOutput>> GetStoryTemplates(IAgencyOwner ao, CommonFilters filters);

        Task<StoryTemplateOutput> GetStoryTemplate(IOrganizationProjectManager pm, Guid templateId);
        Task<bool> Delete(IOrganizationProjectManager pm, Guid templateId);
        Task<PackedList<StoryTemplateOutput>> GetStoryTemplates(IOrganizationProjectManager pm, CommonFilters filters);

        Task<StoryTemplateOutput> Update(IOrganizationAccountManager am, Guid templateId,
            StoryTemplateInput input);
        Task<StoryTemplateOutput> Update(IAgencyOwner ao, Guid templateId,
            StoryTemplateInput input);
        Task<StoryTemplateOutput> Update(IOrganizationProjectManager pm, Guid templateId,
            StoryTemplateInput input);

        Task<StoryTemplateOutput> Create(IOrganizationAccountManager am, 
            StoryTemplateInput input);
        Task<StoryTemplateOutput> Create(IAgencyOwner ao,
            StoryTemplateInput input);
        Task<StoryTemplateOutput> Create(IOrganizationProjectManager pm,
            StoryTemplateInput input);

        Task<StoryTemplateOutput> ConvertStory(IAgencyOwner ao, Guid storyId, ConvertStoryInput input);
        Task<StoryTemplateOutput> ConvertStory(IOrganizationAccountManager am, Guid storyId, ConvertStoryInput input);
        Task<StoryTemplateOutput> ConvertStory(IOrganizationProjectManager pm, Guid storyId, ConvertStoryInput input);
    }
}
