// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Common;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Services;
using AgencyPro.Core.Stories.Filters;
using AgencyPro.Core.Stories.Models;
using AgencyPro.Core.Stories.ViewModels;

namespace AgencyPro.Core.Stories.Services
{
    public interface IStoryService : IService<Story>
    {
        Task<StoryResult> UpdateStory(IOrganizationContractor co, Guid storyId, UpdateStoryInput model);

        Task<StoryResult> UpdateStory(IOrganizationProjectManager pm, Guid storyId, UpdateStoryInput model);

        Task<StoryResult> UpdateStory(IOrganizationAccountManager am, Guid storyId, UpdateStoryInput model);

        Task<StoryResult> UpdateStory(IProviderAgencyOwner ao, Guid storyId, UpdateStoryInput model);

        Task<T> GetStory<T>(IOrganizationCustomer cu, Guid storyId) 
            where T : CustomerStoryOutput;

        Task<T> GetStory<T>(IOrganizationProjectManager pm, Guid storyId) 
            where T : ProjectManagerStoryOutput;

        Task<T> GetStory<T>(IOrganizationContractor co, Guid storyId) 
            where T : ContractorStoryOutput;

        Task<T> GetStory<T>(IOrganizationAccountManager am, Guid storyId) 
            where T : AccountManagerStoryOutput;

        Task<T> GetStory<T>(IProviderAgencyOwner ao, Guid storyId) 
            where T : AgencyOwnerStoryOutput;

        Task<PackedList<T>> GetStories<T>(IOrganizationProjectManager pm, StoryFilters filters) 
            where T : ProjectManagerStoryOutput;

        Task<PackedList<T>> GetStories<T>(IOrganizationContractor co, StoryFilters filters) 
            where T : ContractorStoryOutput;

        Task<PackedList<T>> GetStories<T>(IOrganizationCustomer cu, StoryFilters filters) 
            where T : CustomerStoryOutput;

        Task<PackedList<T>> GetStories<T>(IOrganizationAccountManager am, StoryFilters filters) 
            where T : AccountManagerStoryOutput;

        Task<PackedList<T>> GetStories<T>(IProviderAgencyOwner ao, StoryFilters filters) 
            where T : AgencyOwnerStoryOutput;

        Task<StoryResult> CreateStory(IOrganizationProjectManager pm, CreateStoryInput input);

        Task<StoryResult> CreateStory(IOrganizationAccountManager organizationAccountManager,
            CreateStoryInput input);

        Task<StoryResult> CreateStory(IProviderAgencyOwner agencyOwner, CreateStoryInput input);

        Task<StoryResult> DeleteStory(IOrganizationContractor co, Guid storyId);

        Task<StoryResult> DeleteStory(IOrganizationProjectManager pm, Guid storyId);

        Task<StoryResult> DeleteStory(IProviderAgencyOwner agencyOwner, Guid storyId);

        Task<List<T>> GetStories<T>(IProviderAgencyOwner owner, Guid?[] uniqueStoryIds) 
            where T : AgencyOwnerStoryOutput;

        Task<List<T>> GetStories<T>(IOrganizationAccountManager am, Guid?[] uniqueStoryIds)
            where T : AccountManagerStoryOutput;

        Task<List<T>> GetStories<T>(IOrganizationProjectManager pm, Guid?[] uniqueStoryIds)
            where T : ProjectManagerStoryOutput;

        Task<List<T>> GetStories<T>(IOrganizationCustomer cu, Guid?[] uniqueStoryIds) 
            where T : CustomerStoryOutput;

        Task<List<T>> GetStories<T>(IOrganizationContractor co, Guid?[] uniqueStoryIds) 
            where T : ContractorStoryOutput;

        Task<StoryResult> AssignStory(IOrganizationProjectManager pm, Guid storyId, AssignStoryInput input);

        Task<StoryResult> AssignStory(IProviderAgencyOwner ao, Guid storyId, AssignStoryInput input);
    }
}