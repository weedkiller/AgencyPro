// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Proposals.Extensions;
using AgencyPro.Core.Proposals.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Proposals
{
    public partial class ProposalService
    {
        public async Task<bool> DeleteProposal(IAgencyOwner agencyOwner, Guid proposalId)
        {
            _logger.LogTrace(GetLogMessage($@"Deleting Proposal: {proposalId}"));

            var proposal = await Repository.Queryable().Include(x=>x.ProposalAcceptance)
                .FindById(proposalId).FirstOrDefaultAsync();

            return await DeleteProposal(proposal);
        }

        public async Task<bool> DeleteProposal(IOrganizationAccountManager am, Guid proposalId)
        {
            _logger.LogTrace(GetLogMessage($@"Deleting Proposal: {proposalId}"));

            var proposal = await Repository.Queryable().Include(x => x.ProposalAcceptance)
                .FindById(proposalId).FirstOrDefaultAsync();

            return await DeleteProposal(proposal);
        }

        private async Task<bool> DeleteProposal(FixedPriceProposal proposal)
        {
            bool retVal = false;
            if (proposal?.ProposalAcceptance != null)
            {
                throw new ApplicationException("You cannot delete a proposal that's been accepted");
            }

            if (proposal != null)
            {
                retVal = await Repository.DeleteAsync(proposal, true);
            }

            return retVal;
        }
    }
}