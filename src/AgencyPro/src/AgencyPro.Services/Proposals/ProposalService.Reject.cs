// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Proposals.Enums;
using AgencyPro.Core.Proposals.Events;
using AgencyPro.Core.Proposals.Extensions;
using AgencyPro.Core.Proposals.Models;
using AgencyPro.Core.Proposals.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace AgencyPro.Services.Proposals
{
    public partial class ProposalService
    {
        public async Task<ProposalResult> Reject(IOrganizationCustomer cu, Guid proposalId,
            ProposalRejectionInput input
        )
        {
            _logger.LogTrace(GetLogMessage($@"Rejecting Proposal: {proposalId}"));

            var proposal = await Repository.Queryable()
                .ForOrganizationCustomer(cu)
                .FindById(proposalId)
                .FirstOrDefaultAsync();

            return await RejectProposal(proposal, input);
        }

        public async Task<ProposalResult> Reject(Guid proposalId, ProposalRejectionInput input)
        {
            _logger.LogTrace(GetLogMessage($@"Rejecting Proposal: {proposalId}"));

            var proposal = await Repository.Queryable()
                .FindById(proposalId)
                .FirstOrDefaultAsync();

            return await RejectProposal(proposal, input);
        }

        private async Task<ProposalResult> RejectProposal(FixedPriceProposal proposal, ProposalRejectionInput input)
        {
            if (proposal == null)
                throw new ApplicationException("No proposal found with this id for this organization");

            var retVal = new ProposalResult()
            {
                ProposalId = proposal.Id
            };

            proposal.Status = ProposalStatus.Rejected;
            proposal.UpdatedById = _userInfo.UserId;
            proposal.Updated = DateTimeOffset.UtcNow;

            proposal.StatusTransitions.Add(new ProposalStatusTransition()
            {
                Status = ProposalStatus.Rejected,
                ObjectState = ObjectState.Added
            });

            proposal.InjectFrom(input);

            var result = Update(proposal);

            if (result.Succeeded)
            {
                retVal.Succeeded = true;
                await Task.Run(() =>
                {
                    RaiseEvent(new ProposalRejectedEvent
                    {
                        ProposalId = result.ProposalId
                    });
                });
            }

            return retVal;

        }
        
    }
}