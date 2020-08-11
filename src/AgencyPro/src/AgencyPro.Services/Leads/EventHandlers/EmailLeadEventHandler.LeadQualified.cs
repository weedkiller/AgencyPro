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
        private void LeadQualifiedMarketerEmail(Guid leadId)
        {
            _logger.LogInformation(GetLogMessage("LeadId: {0}"), leadId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var lead = context.Leads
                    .Where(x => x.Id == leadId)
                    .ProjectTo<MarketerLeadEmail>(ProjectionMapping)
                    .First();

                lead.Initialize(Settings);

                Send(TemplateTypes.MarketerLeadQualified, lead,
                    $@"[{lead.MarketerOrganizationName} : Marketing] Your lead has been qualified");

                AddLeadNotification(context, "Lead was qualified", lead);

            }


        }

        private void LeadQualifiedMarketingAgencyOwnerEmail(Guid leadId)
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
                    Send(TemplateTypes.MarketingAgencyOwnerLeadQualified, lead,
                        $@"[{lead.MarketerOrganizationName} : Marketing] One of your marketer's leads has been qualified");

                    AddLeadNotification(context, "Lead was qualified", lead);

                }
                else
                {
                    _logger.LogDebug(GetLogMessage("Mail not required"));
                }
            }

           
        }

        private void LeadQualifiedAccountManagerEmail(Guid leadId)
        {
            _logger.LogInformation(GetLogMessage("LeadId: {0}"), leadId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var lead = context.Leads
                    .Where(x => x.Id == leadId)
                    .ProjectTo<AccountManagerLeadEmail>(ProjectionMapping)
                    .First();

                lead.Initialize(Settings);

                Send(TemplateTypes.AccountManagerLeadQualified, lead,
                    $@"[{lead.ProviderOrganizationName} : Account Management] A new opportunity has been assigned to you");

                AddLeadNotification(context, "Lead was qualified", lead);

            }



        }

        private void LeadQualifiedAgencyOwnerEmail(Guid leadId)
        {
            _logger.LogInformation(GetLogMessage("LeadId: {0}"), leadId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var lead = context.Leads
                    .Where(x => x.Id == leadId)
                    .ProjectTo<AgencyOwnerLeadEmail>(ProjectionMapping)
                    .First();

                lead.Initialize(Settings);

                Send(TemplateTypes.AgencyOwnerLeadQualified, lead,
                    $@"[{lead.ProviderOrganizationName}] A lead in your organization was qualified");

                AddLeadNotification(context, "Lead was qualified", lead);
            }

        }

        public void Handle(LeadQualifiedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Lead Qualified Event Triggered"));

          
            Parallel.Invoke(new List<Action>
            {
                () => LeadQualifiedMarketerEmail(evt.LeadId),
                () => LeadQualifiedMarketingAgencyOwnerEmail(evt.LeadId),
                () => LeadQualifiedAccountManagerEmail(evt.LeadId),
                () => LeadQualifiedAgencyOwnerEmail(evt.LeadId)
            }.ToArray());
        }
    }
}