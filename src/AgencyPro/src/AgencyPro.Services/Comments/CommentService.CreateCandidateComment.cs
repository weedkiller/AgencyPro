// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Candidates.Extensions;
using AgencyPro.Core.Candidates.Models;
using AgencyPro.Core.Comments.Models;
using AgencyPro.Core.Comments.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Services.Comments
{
    public partial class CommentService
    {
        private async Task<bool> CreateCandidateComment(Candidate candidate, CommentInput input, Guid organizationId)
        {
            var comment = new Comment()
            {
                CandidateId = candidate.Id,
                OrganizationId = organizationId
            };

            return await CreateComment(comment, input);
        }

        public async Task<bool> CreateCandidateComment(IProviderAgencyOwner agencyOwner, Guid candidateId, CommentInput input)
        {
            var candidate = await _candidateRepository.Queryable().ForAgencyOwner(agencyOwner)
                .Where(x => x.Id == candidateId)
                .FirstAsync();

            return await CreateCandidateComment(candidate, input, agencyOwner.OrganizationId);
        }

        public async Task<bool> CreateCandidateComment(IOrganizationProjectManager projectManager, Guid candidateId,
            CommentInput input)
        {
            var candidate = await _candidateRepository.Queryable().ForOrganizationProjectManager(projectManager)
                .Where(x => x.Id == candidateId)
                .FirstAsync();

            return await CreateCandidateComment(candidate, input, projectManager.OrganizationId);
        }

        public async Task<bool> CreateCandidateComment(IOrganizationRecruiter recruiter, Guid candidateId,
            CommentInput input)
        {
            var candidate = await _candidateRepository.Queryable().ForOrganizationRecruiter(recruiter)
                .Where(x => x.Id == candidateId)
                .FirstAsync();

            return await CreateCandidateComment(candidate, input, recruiter.OrganizationId);
        }
    }
}