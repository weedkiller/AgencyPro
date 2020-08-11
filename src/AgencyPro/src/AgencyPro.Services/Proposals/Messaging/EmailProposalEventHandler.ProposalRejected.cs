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
        private void ProposalRejectedSendAgencyOwnerEmail(Guid proposalId)
        {
            _logger.LogInformation(GetLogMessage("Proposal Id: {0}"), proposalId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var project = context.FixedPriceProposals
                    .Where(x => x.Id == proposalId)
                    .ProjectTo<AgencyOwnerProposalEmail>(ProjectionMapping)
                    .First();


                project.Initialize(Settings);

                Send(TemplateTypes.AgencyOwnerProposalRejected, project,
                    $@"[{project.ProjectManagerOrganizationName}] A proposal was rejected");
            }
        }

        private void ProposalRejectedSendAccountManagerEmail(Guid proposalId)
        {
            _logger.LogInformation(GetLogMessage("Proposal Id: {0}"), proposalId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var project = context.FixedPriceProposals
                    .Where(x => x.Id == proposalId)
                    .ProjectTo<AccountManagerProposalEmail>(ProjectionMapping)
                    .First();


                project.Initialize(Settings);

                Send(TemplateTypes.AccountManagerProposalRejected, project,
                    $@"[{project.ProjectManagerOrganizationName}] A proposal was rejected");
            }
        }

        private void ProposalRejectedSendProjectManagerEmail(Guid proposalId)
        {
            _logger.LogInformation(GetLogMessage("Proposal Id: {0}"), proposalId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var project = context.FixedPriceProposals
                    .Where(x => x.Id == proposalId)
                    .ProjectTo<ProjectManagerProposalEmail>(ProjectionMapping)
                    .First();


                project.Initialize(Settings);

                Send(TemplateTypes.ProjectManagerProposalRejected, project,
                    $@"[{project.ProjectManagerOrganizationName}] A proposal was rejected");
            }
        }

        private void ProposalRejectedSendCustomerEmail(Guid proposalId)
        {
            _logger.LogInformation(GetLogMessage("Proposal Id: {0}"), proposalId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var project = context.FixedPriceProposals
                    .Where(x => x.Id == proposalId)
                    .ProjectTo<CustomerProposalEmail>(ProjectionMapping)
                    .First();


                project.Initialize(Settings);

                Send(TemplateTypes.CustomerProposalRejected, project,
                    $@"A proposal was rejected");
            }
        }

        public void Handle(ProposalRejectedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Proposal: {0}"), evt.ProposalId);

            Parallel.Invoke(new List<Action>
            {
                () => ProposalRejectedSendAgencyOwnerEmail(evt.ProposalId),
                () => ProposalRejectedSendAccountManagerEmail(evt.ProposalId),
                () => ProposalRejectedSendProjectManagerEmail(evt.ProposalId),
                () => ProposalRejectedSendCustomerEmail(evt.ProposalId),
            }.ToArray());
        }
    }
}