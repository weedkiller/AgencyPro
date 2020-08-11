// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Leads.Enums;
using AgencyPro.Core.Leads.Events;
using AgencyPro.Core.Leads.Models;
using AgencyPro.Core.Leads.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Leads
{
    public partial class LeadService
    {
        public Task<LeadResult> RejectLead(IOrganizationAccountManager am, Guid leadId, LeadRejectInput input)
        {
            _logger.LogInformation(GetLogMessage("AM: {0}; LeadId: {1}; Input: {@input}"), am.OrganizationId, leadId, input);

            return RejectLead(leadId, input);
        }

        public Task<LeadResult> RejectLead(IProviderAgencyOwner agencyOwner, Guid leadId, LeadRejectInput input)
        {
            _logger.LogInformation(GetLogMessage("AO: {0}; LeadId: {1}; Input: {@input}"), agencyOwner.OrganizationId, leadId, input);

            return RejectLead(leadId, input);
        }

        private async Task<LeadResult> RejectLead(Guid leadId, LeadRejectInput input)
        {
            _logger.LogInformation(GetLogMessage("Rejecting lead: {0}; for Reason: {1}"), leadId, input.RejectionReason);
            var retVal = new LeadResult()
            {
                LeadId = leadId
            };

            var entity = await Repository
                .FirstOrDefaultAsync(n => n.Id == leadId);

            if (entity.Status == LeadStatus.Promoted)
            {
                retVal.ErrorMessage = "Lead has already been promoted";
                return retVal;
            }

            if (entity.Status != LeadStatus.Rejected)
            {
                _logger.LogDebug(GetLogMessage("Lead is able to be rejected"));

                entity.Status = LeadStatus.Rejected;
                entity.RejectionReason = input.RejectionReason;
                entity.UpdatedById = _userInfo.Value.UserId;
                entity.Updated = DateTimeOffset.UtcNow;
                entity.ObjectState = ObjectState.Modified;

                entity.StatusTransitions.Add(new LeadStatusTransition()
                {
                    Status = LeadStatus.Rejected,
                    ObjectState = ObjectState.Added
                });

                var leadResult = Repository.InsertOrUpdateGraph(entity, true);

                _logger.LogDebug(GetLogMessage("{0} records updated"), leadResult);

                if (leadResult > 0)
                {
                    retVal.Succeeded = true;
                    await Task.Run(() =>
                    {
                        RaiseEvent(new LeadRejectedEvent
                        {
                            LeadId = leadId
                        });
                    });

                }
            }
            else
            {
                _logger.LogDebug(GetLogMessage("Lead is already rejected"));
            }

            return retVal;
        }
    }
}