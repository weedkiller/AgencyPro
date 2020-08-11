// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.EmailSending.Services;
using AgencyPro.Core.Proposals.Emails;
using AgencyPro.Core.Proposals.Events;
using AgencyPro.Core.Templates.Models;
using AgencyPro.Data.EFCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Proposals.Messaging
{
    public partial class MultiProposalEventHandler
    {
        private void ProposalAcceptedSendAgencyOwnerEmail(Guid proposalId)
        {
            _logger.LogInformation(GetLogMessage("Proposal Id: {0}"), proposalId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var project = context.FixedPriceProposals
                    .Where(x => x.Id == proposalId)
                    .ProjectTo<AgencyOwnerProposalEmail>(ProjectionMapping)
                    .First();


                project.Initialize(Settings);

                Send(TemplateTypes.AgencyOwnerProposalAccepted, project,
                    $@"[{project.ProjectManagerOrganizationName}] A proposal was accepted");
            }
        }

        private void ProposalAcceptedSendAccountManagerEmail(Guid proposalId)
        {
            _logger.LogInformation(GetLogMessage("Proposal Id: {0}"), proposalId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var project = context.FixedPriceProposals
                    .Where(x => x.Id == proposalId)
                    .ProjectTo<AccountManagerProposalEmail>(ProjectionMapping)
                    .First();


                project.Initialize(Settings);

                Send(TemplateTypes.AccountManagerProposalAccepted, project,
                    $@"[{project.ProjectManagerOrganizationName} : Account Management] A proposal was accepted");
            }
        }

        private void ProposalAcceptedSendProjectManagerEmail(Guid proposalId)
        {
            _logger.LogInformation(GetLogMessage("Proposal Id: {0}"), proposalId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var project = context.FixedPriceProposals
                    .Where(x => x.Id == proposalId)
                    .ProjectTo<ProjectManagerProposalEmail>(ProjectionMapping)
                    .First();


                project.Initialize(Settings);

                Send(TemplateTypes.ProjectManagerProposalAccepted, project,
                    $@"[{project.ProjectManagerOrganizationName} : Project Management] A proposal was accepted");
            }
        }

        private void ProposalAcceptedSendCustomerEmail(Guid proposalId)
        {
            _logger.LogInformation(GetLogMessage("Proposal Id: {0}"), proposalId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var project = context.FixedPriceProposals
                    .Where(x => x.Id == proposalId)
                    .ProjectTo<CustomerProposalEmail>(ProjectionMapping)
                    .First();


                project.Initialize(Settings);

                Send(TemplateTypes.CustomerProposalAccepted, project,
                    $@"A proposal was accepted");
            }
        }

        public void Handle(ProposalAcceptedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Proposal: {0}"), evt.ProposalId);

            Parallel.Invoke(new List<Action>
            {
                () => ProposalAcceptedSendAgencyOwnerEmail(evt.ProposalId),
                () => ProposalAcceptedSendAccountManagerEmail(evt.ProposalId),
                () => ProposalAcceptedSendProjectManagerEmail(evt.ProposalId),
                () => ProposalAcceptedSendCustomerEmail(evt.ProposalId),
            }.ToArray());
        }
    }
}