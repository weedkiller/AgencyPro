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

        private void LeadRejectedMarketerEmail(Guid leadId)
        {
            _logger.LogInformation(GetLogMessage("LeadId: {0}"), leadId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var lead = context.Leads
                    .Where(x => x.Id == leadId)
                    .ProjectTo<MarketerLeadEmail>(ProjectionMapping)
                    .First();

                lead.Initialize(Settings);

                Send(TemplateTypes.MarketerLeadRejected, lead,
                    $@"[{lead.MarketerOrganizationName} : Marketing] Your lead was rejected");

                AddLeadNotification(context, "Lead was rejected", lead);

            }

        }

        private void LeadRejectedMarketingAgencyEmail(Guid leadId)
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
                    Send(TemplateTypes.MarketingAgencyOwnerLeadRejected, lead,
                        $@"[{lead.MarketerOrganizationName} : Marketing] Your lead was rejected");

                    AddLeadNotification(context, "Lead was rejected", lead);

                }
                else
                {
                    _logger.LogDebug(GetLogMessage("Mail not required"));
                }
            }


        }

        public void Handle(LeadRejectedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Lead Rejected Event Triggered"));

            LeadRejectedMarketerEmail(evt.LeadId);
            LeadRejectedMarketingAgencyEmail(evt.LeadId);


            Parallel.Invoke(new List<Action>
            {
                () => LeadRejectedMarketerEmail(evt.LeadId),
                () => LeadRejectedMarketingAgencyEmail(evt.LeadId)
            }.ToArray());

        }
    }
}