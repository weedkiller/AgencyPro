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
        public void Handle(ProposalCreatedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Proposal: {0}"), evt.ProposalId);
            
            Parallel.Invoke(new List<Action>
            {
                () => ProposalCreatedSendProjectManagerEmail(evt.ProposalId),
                () => ProposalCreatedSendAgencyOwnerEmail(evt.ProposalId),
            }.ToArray());
        }

        private void ProposalCreatedSendProjectManagerEmail(Guid proposalId)
        {
            _logger.LogInformation(GetLogMessage("Proposal Id: {0}"), proposalId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var project = context.FixedPriceProposals
                    .Where(x => x.Id == proposalId)
                    .ProjectTo<ProjectManagerProposalEmail>(ProjectionMapping)
                    .First();


                project.Initialize(Settings);

                Send(TemplateTypes.ProjectManagerProposalCreated, project,
                    $@"[{project.ProjectManagerOrganizationName} : Project Management] A new proposal has been created");
            }
           
        }

        private void ProposalCreatedSendAgencyOwnerEmail(Guid proposalId)
        {
            _logger.LogInformation(GetLogMessage("Proposal Id: {0}"), proposalId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var project = context.FixedPriceProposals
                    .Where(x => x.Id == proposalId)
                    .ProjectTo<AgencyOwnerProposalEmail>(ProjectionMapping)
                    .First();


                project.Initialize(Settings);

                Send(TemplateTypes.AgencyOwnerProposalCreated, project,
                    $@"[{project.ProjectManagerOrganizationName}] A new proposal has been created");
            }

        }

    }
}