// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.EmailSending.Services;
using AgencyPro.Core.Leads.Emails;
using AgencyPro.Core.Leads.Events;
using AgencyPro.Core.Templates.Models;
using AgencyPro.Data.EFCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Leads.EventHandlers
{
    public partial class MultiLeadEventHandler
    {
        public void Handle(LeadCreatedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Lead Created Event Triggered"));

            Parallel.Invoke(new List<Action>
            {
                () => LeadCreatedAgencyOwnerEmail(evt.LeadId),
                () => LeadCreatedMarketingAgencyOwnerEmail(evt.LeadId),
                () => LeadCreatedMarketerEmail(evt.LeadId)
            }.ToArray());
        }
        
        private void LeadCreatedMarketerEmail(Guid leadId)
        {
            _logger.LogInformation(GetLogMessage("LeadId: {0}"), leadId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var lead = context.Leads
                    .Where(x => x.Id == leadId)
                    .ProjectTo<MarketerLeadEmail>(ProjectionMapping)
                    .First();

                lead.Initialize(Settings);

                Send(TemplateTypes.MarketerLeadCreated, lead,
                    $@"[{lead.MarketerOrganizationName} : Marketing] You have a new lead");
                
                AddLeadNotification(context, "Lead was created", lead);
            }
        }

        private void LeadCreatedAgencyOwnerEmail(Guid leadId)
        {
            _logger.LogInformation(GetLogMessage("LeadId: {0}"), leadId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var lead = context.Leads
                    .Where(x => x.Id == leadId)
                    .ProjectTo<AgencyOwnerLeadEmail>(ProjectionMapping)
                    .First();

                lead.Initialize(Settings);

                Send(TemplateTypes.AgencyOwnerLeadCreated, lead,
                    $@"[{lead.ProviderOrganizationName}] You have a new lead");

                AddLeadNotification(context, "Lead was created", lead);
            };

              
        }

        private void LeadCreatedMarketingAgencyOwnerEmail(Guid leadId)
        {
            _logger.LogInformation(GetLogMessage("LeadId: {0}"), leadId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var lead = context.Leads
                    .Where(x => x.Id == leadId)
                    .ProjectTo<MarketingAgencyOwnerLeadEmail>(ProjectionMapping)
                    .First();

                lead.Initialize(Settings);

                if (lead.IsExternal)
                {
                    Send(TemplateTypes.MarketingAgencyOwnerLeadCreated, lead,
                        $@"[{lead.MarketerOrganizationName} : Marketing] A lead was created on behalf or your Agency");

                    AddLeadNotification(context, "Lead was created", lead);
                }
                else
                {
                    _logger.LogDebug(GetLogMessage("Mail not required"));
                }
            };
        }
    }
}