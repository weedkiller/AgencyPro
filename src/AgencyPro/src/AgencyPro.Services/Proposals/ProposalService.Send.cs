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

namespace AgencyPro.Services.Proposals
{
    public partial class ProposalService
    {
        private async Task<ProposalResult> SendProposal(FixedPriceProposal proposal)
        {
            _logger.LogInformation(GetLogMessage("Sending Proposal: {0}"), proposal.Id);

            var retVal = new ProposalResult
            {
                ProposalId = proposal.Id
            };

            if (proposal == null)
            {
                retVal.ErrorMessage = "Proposal does not exist";
                return retVal;
            }

            if (proposal.Status != ProposalStatus.Pending)
            {
                _logger.LogDebug(GetLogMessage("Proposal ready to be sent"));

                proposal.Status = ProposalStatus.Pending;
                proposal.StatusTransitions.Add(new ProposalStatusTransition()
                {
                    Status = ProposalStatus.Pending,
                    ObjectState = ObjectState.Added
                });

                proposal.UpdatedById = _userInfo.UserId;
                proposal.Updated = DateTimeOffset.UtcNow;
                proposal.ObjectState = ObjectState.Modified;

                var records = Repository.Update(proposal, true);

                _logger.LogDebug("{0} records updated in database");

                if (records > 0)
                {
                    retVal.Succeeded = true;
                    await Task.Run(() =>
                    {
                        RaiseEvent(new ProposalSentEvent()
                        {
                            ProposalId = proposal.Id
                        });
                    });
                }
            }

            return retVal;
        }

        public async Task<ProposalResult> SendProposal(IProviderAgencyOwner agencyOwner, Guid proposalId)
        {
            _logger.LogInformation(GetLogMessage("AO: {0}; Sending Proposal: {1}"), agencyOwner.OrganizationId, proposalId);

            var proposal = await Repository.Queryable()
                .ForAgencyOwner(agencyOwner)
                .FirstOrDefaultAsync(x => x.Id == proposalId);

            return await SendProposal(proposal);
        }

       
        public async Task<ProposalResult> SendProposal(IOrganizationAccountManager am, Guid proposalId)
        {
            _logger.LogInformation(GetLogMessage("AM: {0}; Sending Proposal: {1}"), am.OrganizationId, proposalId);

            var proposal = await Repository.Queryable()
                .ForOrganizationAccountManager(am)
                .FirstOrDefaultAsync(x => x.Id == proposalId);

            return await SendProposal(proposal);

        }
    }
}