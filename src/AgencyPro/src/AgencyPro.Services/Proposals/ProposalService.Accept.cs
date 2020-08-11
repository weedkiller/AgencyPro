// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Proposals.Enums;
using AgencyPro.Core.Proposals.Extensions;
using AgencyPro.Core.Proposals.Models;
using AgencyPro.Core.Proposals.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Contracts.Enums;
using AgencyPro.Core.Contracts.Events;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.Projects.Enums;
using AgencyPro.Core.Proposals.Events;
using AgencyPro.Core.Stories.Enums;
using AgencyPro.Core.Stories.Events;
using AgencyPro.Core.Stories.Models;

namespace AgencyPro.Services.Proposals
{
    
    

    public partial class ProposalService
    {
        private async Task<ProposalResult> AcceptFixedPriceProposal(FixedPriceProposal proposal)
        {
            var retVal = new ProposalResult()
            {
                ProposalId = proposal.Id
            };

            if (proposal.Status == ProposalStatus.Accepted && proposal.ProposalAcceptance != null)
            {
                retVal.ErrorMessage = "Proposal is already accepted";
                return retVal;
            }

            var now = DateTimeOffset.UtcNow;


            proposal.Status = ProposalStatus.Accepted;
            proposal.UpdatedById = _userInfo.UserId;
            proposal.Updated = now;
            proposal.ObjectState = ObjectState.Modified;

            proposal.Project.Status = ProjectStatus.Active;
            proposal.Project.ObjectState = ObjectState.Modified;
            proposal.Project.Updated = now;
            proposal.Project.UpdatedById = _userInfo.UserId;


            proposal.StatusTransitions.Add(new ProposalStatusTransition()
            {
                Status = ProposalStatus.Accepted,
                ObjectState = ObjectState.Added
            });

            foreach (var contract in proposal.Project.Contracts.Where(x => x.Status == ContractStatus.Pending))
            {
                contract.Status = ContractStatus.Active;
                contract.ObjectState = ObjectState.Modified;
                contract.UpdatedById = _userInfo.UserId;
                contract.Updated = now;
                contract.StatusTransitions.Add(new ContractStatusTransition()
                {
                    ObjectState = ObjectState.Added,
                    Status = ContractStatus.Active
                });
            }

            proposal.ProposalAcceptance = new ProposalAcceptance()
            {
                Created = now,
                AcceptedCompletionDate = DateTimeOffset.UtcNow.Date
                    .AddDays(Convert.ToDouble(proposal.TotalDays))
                    .Date,
                TotalCost = proposal.TotalPriceQuoted,
                CustomerRate = proposal.CustomerRateBasis,
                NetTerms = 7, // todo: fix this
                ProposalType = proposal.ProposalType,
                TotalDays = proposal.TotalDays,
                Velocity = proposal.VelocityBasis,
                AgreementText = proposal.AgreementText,
                Updated = now,
                CustomerId = proposal.Project.CustomerId,
                CustomerOrganizationId = proposal.Project.CustomerOrganizationId,
                AcceptedBy = _userInfo.UserId,
                ProposalBlob = string.Empty,
                ObjectState = ObjectState.Added
            };


            foreach (var story in proposal.Project.Stories.Where(x => x.Status == StoryStatus.Pending))
            {
                story.CustomerAcceptanceDate = now;
                story.Updated = now;
                story.ObjectState = ObjectState.Modified;
                story.Status = StoryStatus.Approved;
                story.CustomerApprovedHours = story.StoryPoints * proposal.EstimationBasis;

                story.StatusTransitions.Add(new StoryStatusTransition()
                {
                    Status = StoryStatus.Approved,
                    ObjectState = ObjectState.Added
                });

                _logger.LogDebug(GetLogMessage("Setting approved story hours for story: {0}, amount: {1}"),
                    story.Id, story.CustomerApprovedHours);
            }

            var proposalResult = Repository.InsertOrUpdateGraph(proposal, true);

            _logger.LogDebug(GetLogMessage("{0} records updated"), proposalResult);

            if (proposalResult > 0)
            {
                retVal.Succeeded = true;
                await Task.Run(() =>
                {
                    RaiseEvent(new ProposalAcceptedEvent()
                    {
                        ProposalId = proposal.Id
                    });
                });

                foreach (var contract in proposal.Project.Contracts.Where(x => x.Updated == now))
                {
                    await Task.Run(() =>
                    {
                        RaiseEvent(new ContractApprovedEvent()
                        {
                            ContractId = contract.Id
                        });
                    });

                }

                foreach (var story in proposal.Project.Stories.Where(x => x.Updated == now))
                {
                    await Task.Run(() =>
                    {
                        RaiseEvent(new StoryApprovedEvent()
                        {
                            StoryId = story.Id
                        });
                    });

                }
            }

            return retVal;
        }

        public async Task<ProposalResult> AcceptFixedPriceProposal(IOrganizationCustomer cu, Guid proposalId)
        {
            _logger.LogInformation(GetLogMessage("Accepting Proposal: {proposalId}"), proposalId);

            var proposal = await Repository
                .Queryable()
                .Include(x => x.Project)
                .ThenInclude(x => x.Stories)
                .Include(x=>x.Project)
                .ThenInclude(x=>x.Contracts)
                .Include(x=>x.ProposalAcceptance)
                .ForOrganizationCustomer(cu)
                .FirstAsync(x => x.Id == proposalId);
            
            return await AcceptFixedPriceProposal(proposal);
        }

        public async Task<ProposalResult> AcceptFixedPriceProposal(Guid proposalId)
        {
            _logger.LogInformation(GetLogMessage("Accepting Proposal: {proposalId}"), proposalId);

            var proposal = await Repository
                .Queryable()
                .Include(x => x.Project)
                .ThenInclude(x => x.Stories)
                .Include(x => x.Project)
                .ThenInclude(x => x.Contracts)
                .Include(x => x.ProposalAcceptance)
                .FirstAsync(x => x.Id == proposalId);

            return await AcceptFixedPriceProposal(proposal);
        }

        public async Task<ProposalResult> AcceptFixedPriceProposal(IProviderAgencyOwner ao, Guid proposalId)
        {
            var proposal = await Repository
                .Queryable()
                .Include(x => x.Project)
                .ThenInclude(x => x.Stories)
                .Include(x => x.Project)
                .ThenInclude(x => x.Contracts)
                .Include(x => x.ProposalAcceptance)
                .ForAgencyOwner(ao)
                .FirstAsync(x => x.Id == proposalId);

            return await AcceptFixedPriceProposal(proposal);
        }

        public async Task<ProposalResult> AcceptFixedPriceProposal(IOrganizationAccountManager ao, Guid proposalId)
        {
            var proposal = await Repository
                .Queryable()
                .Include(x => x.Project)
                .ThenInclude(x => x.Stories)
                .Include(x => x.Project)
                .ThenInclude(x => x.Contracts)
                .Include(x => x.ProposalAcceptance)
                .ForOrganizationAccountManager(ao)
                .FirstAsync(x => x.Id == proposalId);

            return await AcceptFixedPriceProposal(proposal);
        }
    }
}