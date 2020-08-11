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
        public void Handle(LeadPromotedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Lead Promoted Event Triggered"));

            // send email to provider agency owner
           
            Parallel.Invoke(new List<Action>
            {
                () => LeadPromotedMarketerEmail(evt.LeadId),
                () => LeadPromotedMarketingAgencyEmail(evt.LeadId),
                () => LeadPromotedAccountManagerEmail(evt.LeadId),
                () => LeadPromotedAgencyOwnerEmail(evt.LeadId)
            }.ToArray());

        }

        private void LeadPromotedAccountManagerEmail(Guid leadId)
        {
            _logger.LogInformation(GetLogMessage("LeadId: {0}"), leadId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var lead = context.Leads
                    .Where(x => x.Id == leadId)
                    .ProjectTo<AccountManagerLeadEmail>(ProjectionMapping)
                    .First();

                lead.Initialize(Settings);

                Send(TemplateTypes.AccountManagerLeadPromoted, lead,
                    $@"[{lead.ProviderOrganizationName} - Account Management] Your Lead Was Promoted");

                AddLeadNotification(context, "Lead was promoted", lead);

            };
        }

        private void LeadPromotedMarketerEmail(Guid leadId)
        {
            _logger.LogInformation(GetLogMessage("LeadId: {0}"), leadId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var lead = context.Leads
                    .Where(x => x.Id == leadId)
                    .ProjectTo<MarketerLeadEmail>(ProjectionMapping)
                    .First();

                lead.Initialize(Settings);

                Send(TemplateTypes.MarketerLeadPromoted, lead,
                    $@"[{lead.MarketerOrganizationName} - Marketing] Your Lead Was Promoted");


            };
        }

        private void LeadPromotedAgencyOwnerEmail(Guid leadId)
        {
            _logger.LogInformation(GetLogMessage("LeadId: {0}"), leadId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var lead = context.Leads
                    .Where(x => x.Id == leadId)
                    .ProjectTo<AgencyOwnerLeadEmail>(ProjectionMapping)
                    .First();

                lead.Initialize(Settings);

                Send(TemplateTypes.AgencyOwnerLeadPromoted, lead,
                    $@"[{lead.MarketerOrganizationName}] Your Lead Was Promoted");

                AddLeadNotification(context, "Lead was promoted", lead);

            }


        }

        private void LeadPromotedMarketingAgencyEmail(Guid leadId)
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
                    Send(TemplateTypes.MarketingAgencyOwnerLeadPromoted, lead,
                        $@"[{lead.MarketerOrganizationName} - Marketing] Your Lead Was Promoted");

                    AddLeadNotification(context, "Lead was promoted", lead);
                }
                else
                {
                    _logger.LogDebug(GetLogMessage("Mail not required"));
                }
            }

            
        }
    }
}