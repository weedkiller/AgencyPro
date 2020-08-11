// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
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
        public async Task<ProposalResult> Update(IProviderAgencyOwner agencyOwner, Guid proposalId, ProposalOptions input)
        {
            var proposal = await Repository
                .Queryable()
                .ForAgencyOwner(agencyOwner)
                .Where(x => x.Id == proposalId)
                .FirstAsync();

            proposal.InjectFrom(input);

            proposal.EstimationBasis = input.EstimationBasis ?? 0;

            return Update(proposal);
        }

        public async Task<ProposalResult> Update(IOrganizationAccountManager am, Guid proposalId, ProposalOptions input)
        {
            var proposal = await Repository
                .Queryable()
                .ForOrganizationAccountManager(am)
                .Where(x => x.Id == proposalId)
                .FirstAsync();

            proposal.InjectFrom(input);

            proposal.EstimationBasis = input.EstimationBasis ?? 0;

            return Update(proposal);
        }

        private  ProposalResult Update(FixedPriceProposal proposal, bool commit = true, bool fireEvents = true)
        {
            _logger.LogInformation(GetLogMessage("Proposal:{0}"), proposal.Id);

            _logger.LogInformation(GetLogMessage("Proposal story Point basis : {0}"), proposal.StoryPointBasis);

            _logger.LogInformation(GetLogMessage("Proposal estimation basis : {0}"), proposal.EstimationBasis);

            _logger.LogInformation(GetLogMessage("Proposal customer rate basis : {0}"), proposal.CustomerRateBasis);

            _logger.LogInformation(GetLogMessage("Proposal other percent basis : {0}"), proposal.OtherPercentBasis);

            _logger.LogInformation(GetLogMessage("Proposal total hours : {0}"), proposal.TotalHours);

            _logger.LogInformation(GetLogMessage("Proposal total price quoted : {0}"), proposal.TotalPriceQuoted);

            var retVal = new ProposalResult()
            {
                ProposalId = proposal.Id
            };

            proposal.UpdatedById = _userInfo.UserId;
            proposal.Updated = DateTimeOffset.UtcNow;
            proposal.ObjectState = ObjectState.Modified;

            var records = Repository.InsertOrUpdateGraph(proposal, commit);
            _logger.LogDebug(GetLogMessage("{0} records updated"), records);
            if (records > 0)
            {
                retVal.Succeeded = true;
            }

            return retVal;
        }
    }
}