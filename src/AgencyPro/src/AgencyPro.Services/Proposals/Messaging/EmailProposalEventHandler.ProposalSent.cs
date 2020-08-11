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
        private void ProposalSentSendCustomerEmail(Guid proposalId)
        {
            _logger.LogInformation(GetLogMessage("Proposal Id: {0}"), proposalId);


            using (var context = new AppDbContext(DbContextOptions))
            {
                var project = context.FixedPriceProposals
                    .Where(x => x.Id == proposalId)
                    .ProjectTo<CustomerProposalEmail>(ProjectionMapping)
                    .First();


                project.FlowUrl = Settings.Urls.Origin;

                Send(TemplateTypes.CustomerProposalSent, project,
                    $@"You have a new proposal from {project.ProjectManagerOrganizationName}");
            }
            
        }

        private void ProposalSentSendAgencyOwnerEmail(Guid proposalId)
        {
            _logger.LogInformation(GetLogMessage("Proposal Id: {0}"), proposalId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var project = context.FixedPriceProposals
                    .Where(x => x.Id == proposalId)
                    .ProjectTo<AgencyOwnerProposalEmail>(ProjectionMapping)
                    .First();


                project.Initialize(Settings);

                Send(TemplateTypes.AgencyOwnerProposalSent, project,
                    $@"[{project.ProjectManagerOrganizationName}] A proposal was sent");
            }
           
        }

        private void ProposalSentSendAccountManagerEmail(Guid proposalId)
        {
            _logger.LogInformation(GetLogMessage("Proposal Id: {0}"), proposalId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var project = context.FixedPriceProposals
                    .Where(x => x.Id == proposalId)
                    .ProjectTo<AccountManagerProposalEmail>(ProjectionMapping)
                    .First();


                project.Initialize(Settings);

                Send(TemplateTypes.AccountManagerProposalSent, project,
                    $@"[{project.AccountManagerOrganizationName} : Account Management] A proposal was sent");
            }
            
        }

        private void ProposalSentSendProjectManagerEmail(Guid proposalId)
        {
            _logger.LogInformation(GetLogMessage("Proposal Id: {0}"), proposalId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var project = context.FixedPriceProposals
                    .Where(x => x.Id == proposalId)
                    .ProjectTo<ProjectManagerProposalEmail>(ProjectionMapping)
                    .First();


                project.Initialize(Settings);

                Send(TemplateTypes.ProjectManagerProposalSent, project,
                    $@"[{project.ProjectManagerOrganizationName} : Project Management] A proposal was sent");
            }
           
        }

        public void Handle(ProposalSentEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Proposal: {0}"), evt.ProposalId);

            Parallel.Invoke(new List<Action>
            {
                () => ProposalSentSendCustomerEmail(evt.ProposalId),
                () => ProposalSentSendAccountManagerEmail(evt.ProposalId),
                () => ProposalSentSendProjectManagerEmail(evt.ProposalId),
                () => ProposalSentSendAgencyOwnerEmail(evt.ProposalId)
            }.ToArray());
        }
    }
}