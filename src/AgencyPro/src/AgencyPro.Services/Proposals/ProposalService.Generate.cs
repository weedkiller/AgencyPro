// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Proposals.Enums;
using AgencyPro.Core.Proposals.Events;
using AgencyPro.Core.Proposals.Models;
using AgencyPro.Core.Proposals.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyPro.Services.Proposals
{
    public partial class ProposalService
    {
        public Task<ProposalResult> Create(IAgencyOwner agencyOwner,
            Guid projectId,
            ProposalOptions input
        )
        {
            _logger.LogInformation(GetLogMessage("AM: {0}; Project: {1}; Input: {@input}"), agencyOwner.OrganizationId, projectId, input);

            return Create(input, projectId, agencyOwner.OrganizationId);
        }

        public Task<ProposalResult> Create(IOrganizationAccountManager am,
            Guid projectId,
            ProposalOptions input
        )
        {
            _logger.LogInformation(GetLogMessage("AM: {0}; Project: {1}; Input: {@input}"), am.OrganizationId, projectId, input);

            return Create(input, projectId, am.OrganizationId);
        }

        private async Task<ProposalResult> Create(ProposalOptions input, Guid projectId, Guid organizationId)
        {
            _logger.LogInformation(GetLogMessage($@"Create Proposal Proposal For Project ID: {projectId}"));

            var retVal = new ProposalResult();
            
            var project = _projectRepository.Queryable()
                .Include(x => x.Proposal)
                .Include(x => x.Stories)
                .Include(x => x.Contracts)
                .FirstAsync(x => x.Id == projectId);

            var org = _organizationRepository.Queryable()
                .Include(x => x.ProviderOrganization)
                .FirstOrDefaultAsync(x => x.Id == organizationId);

            await Task.WhenAll(project, org);


            if (project.Result.Proposal != null)
                throw new ApplicationException("Proposal already exists");

            if (project.Result.Stories.Count == 0)
                throw new ApplicationException("Project must have at least one story with one story point");

            if (project.Result.Contracts.Count == 0)
                throw new ApplicationException("Project must have at least one contract");

            var proposal = new FixedPriceProposal()
            {
                Id = projectId,
                ObjectState = ObjectState.Added,
                Status = ProposalStatus.Draft,
                ProposalType = ProposalType.Fixed,
                UpdatedById = _userInfo.UserId,
                CreatedById = _userInfo.UserId,
                StoryPointBasis = input.StoryPointBasis.GetValueOrDefault(
                    project
                        .Result.Stories.Sum(x => x.StoryPoints.GetValueOrDefault())),
                AgreementText = input.AgreementText,
                OtherPercentBasis = input.OtherPercentBasis,
                BudgetBasis = input.BudgetBasis,
                CustomerRateBasis = input.CustomerRateBasis.GetValueOrDefault(),
                EstimationBasis = input.EstimationBasis
                    .GetValueOrDefault(org.Result.ProviderOrganization.EstimationBasis),
                WeeklyMaxHourBasis = input.WeeklyMaxHourBasis
                    .GetValueOrDefault(project
                        .Result.Contracts.Sum(x => x.MaxWeeklyHours)),

            };

            proposal.StatusTransitions.Add(new ProposalStatusTransition()
            {
                Status = ProposalStatus.Draft,
                ObjectState = ObjectState.Added
            });

            proposal.InjectFrom(input);

            _logger.LogInformation(GetLogMessage("Proposal story Point basis : {0}"), proposal.StoryPointBasis);

            _logger.LogInformation(GetLogMessage("Proposal estimation basis : {0}"), proposal.EstimationBasis);

            _logger.LogInformation(GetLogMessage("Proposal customer rate basis : {0}"), proposal.CustomerRateBasis);

            _logger.LogInformation(GetLogMessage("Proposal other percent basis : {0}"), proposal.OtherPercentBasis);

            _logger.LogInformation(GetLogMessage("Proposal total hours : {0}"), proposal.TotalHours);

            _logger.LogInformation(GetLogMessage("Proposal total price quoted : {0}"), proposal.TotalPriceQuoted);

            var records = await Repository.InsertAsync(proposal, true);

            _logger.LogDebug(GetLogMessage("{0} records updated"), records);

            if (records > 0)
            {
                retVal.Succeeded = true;
                retVal.ProposalId = projectId;

                await Task.Run(() =>
                {
                    RaiseEvent(new ProposalCreatedEvent
                    {
                        ProposalId = projectId
                    });
                });

            }

            return retVal;
        }
    }
}