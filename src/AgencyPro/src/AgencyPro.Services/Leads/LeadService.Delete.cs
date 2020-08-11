// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Leads.Enums;
using AgencyPro.Core.Leads.Events;
using AgencyPro.Core.Leads.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Leads.Extensions;

namespace AgencyPro.Services.Leads
{
    public partial class LeadService
    {
        private async Task<LeadResult> DeleteLead(Guid leadId)
        {
            _logger.LogInformation(GetLogMessage("Lead:{0}"), leadId);
            var retVal = new LeadResult();

            var lead = await Repository.FirstOrDefaultAsync(x => x.Id == leadId);
            lead.UpdatedById = _userInfo.Value.UserId;
            lead.Updated = DateTimeOffset.UtcNow;
            lead.IsDeleted = true;

            var records = await Repository.UpdateAsync(lead, true);

            _logger.LogDebug(GetLogMessage("{0} records updated"));
            if (records > 0)
            {
                retVal.Succeeded = true;
                await Task.Run(() =>
                {
                    RaiseEvent(new LeadDeletedEvent
                    {
                        LeadId = leadId
                    });
                });
            }


            return retVal;
        }

        public async Task<LeadResult> DeleteLead(IProviderAgencyOwner agencyOwner, Guid leadId)
        {
            var lead = await Repository.Queryable()
                .ForAgencyOwner(agencyOwner)
                .Where(x => x.Id == leadId).FirstAsync();

            return await DeleteLead(lead.Id);
        }

        public async Task<LeadResult> DeleteLead(IOrganizationMarketer ma, Guid leadId)
        {
            var lead = await Repository.Queryable()
                .ForOrganizationMarketer(ma)
                .Where(x => x.Id == leadId).FirstAsync();

            if (lead.Status != LeadStatus.New)
                throw new InvalidOperationException("You cannot delete a lead in this state");
            return await DeleteLead(lead.Id);
        }

        public async Task<LeadResult> DeleteLead(IOrganizationAccountManager am, Guid leadId)
        {
            var lead = await Repository.Queryable()
                .ForOrganizationAccountManager(am)
                .Where(x => x.Id == leadId).FirstAsync();

            return await DeleteLead(lead.Id);
        }
    }
}