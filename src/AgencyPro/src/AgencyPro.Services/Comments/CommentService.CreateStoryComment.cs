// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Comments.Models;
using AgencyPro.Core.Comments.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Stories.Extensions;
using AgencyPro.Core.Stories.Models;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Services.Comments
{
    public partial class CommentService
    {
        private async Task<bool> CreateStoryComment(Story s, CommentInput input, Guid organizationId)
        {
            var comment = new Comment()
            {
                StoryId = s.Id,
                OrganizationId = organizationId
            };

            return await CreateComment(comment, input);
        }

        public async Task<bool> CreateStoryComment(IProviderAgencyOwner agencyOwner, Guid storyId, CommentInput input)

        {
            var story = await _storyRepository.Queryable().ForAgencyOwner(agencyOwner)
                .FindById(storyId).FirstAsync();

            return await CreateStoryComment(story, input, agencyOwner.OrganizationId);
        }

        public async Task<bool> CreateStoryComment(IOrganizationAccountManager accountManager, Guid storyId,
            CommentInput input) 
        {
            var story = await _storyRepository.Queryable().ForOrganizationAccountManager(accountManager)
                .FindById(storyId).FirstAsync();

            return await CreateStoryComment(story, input, accountManager.OrganizationId);
        }

        public async Task<bool> CreateStoryComment(IOrganizationProjectManager projectManager, Guid storyId,
            CommentInput input) 
        {
            var story = await _storyRepository.Queryable().ForOrganizationProjectManager(projectManager)
                .FindById(storyId).FirstAsync();

            return await CreateStoryComment(story, input, projectManager.OrganizationId);
        }

        public async Task<bool> CreateStoryComment(IOrganizationCustomer customer, Guid storyId, CommentInput input)
            
        {
            var story = await _storyRepository.Queryable().ForOrganizationCustomer(customer)
                .FindById(storyId).FirstAsync();

            return await CreateStoryComment(story, input, customer.OrganizationId);
        }

        public async Task<bool> CreateStoryComment(IOrganizationContractor contractor, Guid storyId, CommentInput input)
            
        {
            var story = await _storyRepository.Queryable().ForOrganizationContractor(contractor)
                .FindById(storyId).FirstAsync();

            return await CreateStoryComment(story, input, contractor.OrganizationId);
        }
    }
}