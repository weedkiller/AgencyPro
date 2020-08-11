// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Comments.Models;
using AgencyPro.Core.Comments.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Projects.Extensions;
using AgencyPro.Core.Projects.Models;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Services.Comments
{
    public partial class CommentService
    {
        private async Task<bool> CreateProjectComment(Project p, CommentInput input, Guid organizationId)
        {
            var comment = new Comment()
            {
                ProjectId = p.Id,
                OrganizationId = organizationId
            };

            return await CreateComment(comment, input);
        }
        
        public async Task<bool> CreateProjectComment(IProviderAgencyOwner agencyOwner, Guid projectId, CommentInput input)
        {
            var project = await _projectRepository
                .Queryable()
                .ForAgencyOwner(agencyOwner)
                .FindById(projectId)
                .FirstAsync();

            return await CreateProjectComment(project, input, agencyOwner.OrganizationId);
        }

        public async Task<bool> CreateProjectComment(IOrganizationAccountManager accountManager, Guid projectId,
            CommentInput input) 
        {
            var project = await _projectRepository
                .Queryable()
                .ForOrganizationAccountManager(accountManager)
                .FindById(projectId)
                .FirstAsync();

            return await CreateProjectComment(project, input, accountManager.OrganizationId);
        }

        public async Task<bool> CreateProjectComment(IOrganizationProjectManager projectManager, Guid projectId,
            CommentInput input) 
        {
            var project = await _projectRepository
                .Queryable()
                .ForOrganizationProjectManager(projectManager)
                .FindById(projectId)
                .FirstAsync();

            return await CreateProjectComment(project, input, projectManager.OrganizationId);
        }

        public async Task<bool> CreateProjectComment(IOrganizationCustomer customer, Guid projectId, CommentInput input)
            
        {
            var project = await _projectRepository
                .Queryable()
                .ForOrganizationCustomer(customer)
                .FindById(projectId)
                .FirstAsync();

            return await CreateProjectComment(project, input, customer.OrganizationId);
        }

        public async Task<bool> CreateProjectComment(IOrganizationContractor contractor, Guid projectId,
            CommentInput input) 
        {
            var project = await _projectRepository
                .Queryable()
                .ForOrganizationContractor(contractor)
                .FindById(projectId)
                .FirstAsync();

            return await CreateProjectComment(project, input, contractor.OrganizationId);
        }
    }
}