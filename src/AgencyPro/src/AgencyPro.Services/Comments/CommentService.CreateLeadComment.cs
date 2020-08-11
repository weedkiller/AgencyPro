// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Comments.Models;
using AgencyPro.Core.Comments.ViewModels;
using AgencyPro.Core.Leads.Extensions;
using AgencyPro.Core.Leads.Models;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Services.Comments
{
    public partial class CommentService
    {
        private async Task<bool> CreateLeadComment(Lead lead, CommentInput input, Guid organizationId)
        {
            var comment = new Comment()
            {
                LeadId = lead.Id,
                OrganizationId = organizationId
            };

            return await CreateComment(comment, input);
        }

        public async Task<bool> CreateLeadComment(IProviderAgencyOwner agencyOwner, Guid leadId, CommentInput input)
        {
            var lead = await _leadRepository.Queryable().ForAgencyOwner(agencyOwner)
                .Where(x => x.Id == leadId)
                .FirstAsync();

            return await CreateLeadComment(lead, input, agencyOwner.OrganizationId);
        }

        public async Task<bool> CreateLeadComment(IOrganizationAccountManager accountManager, Guid leadId,
            CommentInput input)
        {
            var lead = await _leadRepository.Queryable().ForOrganizationAccountManager(accountManager)
                .Where(x => x.Id == leadId)
                .FirstAsync();

            return await CreateLeadComment(lead, input, accountManager.OrganizationId);
        }

        public async Task<bool> CreateLeadComment(IOrganizationMarketer marketer, Guid leadId, CommentInput input)
        {
            var lead = await _leadRepository.Queryable().ForOrganizationMarketer(marketer)
                .Where(x => x.Id == leadId)
                .FirstAsync();

            return await CreateLeadComment(lead, input, marketer.OrganizationId);
        }
    }
}