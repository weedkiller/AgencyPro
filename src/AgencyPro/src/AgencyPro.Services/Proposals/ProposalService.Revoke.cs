// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Proposals.Enums;
using AgencyPro.Core.Proposals.Extensions;
using AgencyPro.Core.Proposals.Models;
using AgencyPro.Core.Proposals.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Proposals
{
    public partial class ProposalService
    {
        private async Task<ProposalResult> RevokeProposal(FixedPriceProposal proposal)
        {
            _logger.LogInformation(GetLogMessage("Proposal: {0}"), proposal.Id);

            var retVal = new ProposalResult()
            {
                ProposalId = proposal.Id
            };

            proposal.UpdatedById = _userInfo.UserId;
            proposal.Updated = DateTimeOffset.UtcNow;
            proposal.ObjectState = ObjectState.Modified;


            proposal.Status = ProposalStatus.Draft;
            proposal.StatusTransitions.Add(new ProposalStatusTransition()
            {
                Status = ProposalStatus.Draft,
                ObjectState = ObjectState.Added
            });

            var result = Repository.Update(proposal, true);

            _logger.LogDebug(GetLogMessage("Records updated: {0}"), result);
            if (result > 0)
            {
                retVal.Succeeded = true;
            }

            return retVal;
        }

        public async Task<ProposalResult> RevokeProposal(IProviderAgencyOwner agencyOwner, Guid proposalId)
        {
            _logger.LogInformation(GetLogMessage("Proposal: {0}"), proposalId);

            var proposal = await Repository.Queryable()
                .ForAgencyOwner(agencyOwner)
                .FirstOrDefaultAsync(x => x.Id == proposalId);
            return await RevokeProposal(proposal);
        }

        public async Task<ProposalResult> RevokeProposal(IOrganizationAccountManager am, Guid proposalId)
        {
            _logger.LogInformation(GetLogMessage("Proposal: {0}"), proposalId);

            var proposal = await Repository.Queryable()
                .ForOrganizationAccountManager(am)
                .FirstOrDefaultAsync(x => x.Id == proposalId);

            return await RevokeProposal(proposal);
        }
        
    }
}