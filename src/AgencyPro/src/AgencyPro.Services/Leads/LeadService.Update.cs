// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Extensions;
using AgencyPro.Core.Leads.Events;
using AgencyPro.Core.Leads.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace AgencyPro.Services.Leads
{
    public partial class LeadService
    {
        public Task<LeadResult> UpdateLead(IProviderAgencyOwner agencyOwner, Guid leadId, LeadInput model)
        {
            _logger.LogInformation(GetLogMessage("PAO: {0}; updating lead: {1}"), agencyOwner.OrganizationId, leadId);

            return UpdateLead(leadId, model);
        }

        public Task<LeadResult> UpdateLead(IOrganizationAccountManager am, Guid leadId, LeadInput model)
        {
            _logger.LogInformation(GetLogMessage("AM: {0}; updating lead: {1}"), am.OrganizationId, leadId);
            return UpdateLead(leadId, model);
        }

        public Task<LeadResult> UpdateLead(IOrganizationMarketer ma, Guid leadId, LeadInput model)
        {
            _logger.LogInformation(GetLogMessage("MA: {0}; updating lead: {1}"), ma.OrganizationId, leadId);

            return UpdateLead(leadId, model);
        }

        public async Task<LeadResult> UpdateLead(Guid leadId, LeadInput model)
        {
            _logger.LogInformation(GetLogMessage("Updating Lead: {0}; Input: {@input}"), leadId, model);
            var retVal =new LeadResult()
            {
                LeadId = leadId
            };

            var entity = await Repository.FirstOrDefaultAsync(n => n.Id == leadId);

            entity.InjectFrom<NullableInjection>(model);
            entity.UpdatedById = _userInfo.Value.UserId;
            entity.Updated = DateTimeOffset.UtcNow;
            entity.ObjectState = ObjectState.Modified;

            var leadResult = Repository.InsertOrUpdateGraph(entity, true);

            _logger.LogDebug(GetLogMessage("{0} records updated in database"), leadResult);

            if (leadResult > 0)
            {
                retVal.Succeeded = true;
                await Task.Run(() =>
                {
                    
                    RaiseEvent(new LeadUpdatedEvent
                    {
                        LeadId = retVal.LeadId.Value
                    });
                });
            }
           

            return retVal;
        }
    }
}