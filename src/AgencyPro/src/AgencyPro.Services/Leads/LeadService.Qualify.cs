// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.BonusIntents.ViewModels;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Leads.Enums;
using AgencyPro.Core.Leads.Events;
using AgencyPro.Core.Leads.Extensions;
using AgencyPro.Core.Leads.Models;
using AgencyPro.Core.Leads.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace AgencyPro.Services.Leads
{
    public partial class LeadService
    {
        public async Task<LeadResult> QualifyLead(IProviderAgencyOwner ao, Guid leadId, LeadQualifyInput input)
        {
            _logger.LogInformation(GetLogMessage($@"LeadId: {leadId}"));

            var retVal = new LeadResult()
            {
                LeadId = leadId
            };

            var entity = await Repository
                .Queryable()
                .ForAgencyOwner(ao)
                .Where(x=>x.Id == leadId)
                .FirstAsync();

            if (entity.Status != LeadStatus.Qualified)
            {

                entity.InjectFrom(input);
                entity.AccountManagerId = input.AccountManagerId;
                entity.AccountManagerOrganizationId = ao.OrganizationId;
                entity.Status = LeadStatus.Qualified;
                entity.UpdatedById = _userInfo.Value.UserId;
                entity.Updated = DateTimeOffset.UtcNow;
                entity.ObjectState = ObjectState.Modified;

                entity.StatusTransitions.Add(new LeadStatusTransition()
                {
                    Status = LeadStatus.Qualified,
                    ObjectState = ObjectState.Added
                });

                var leadResult = Repository.Update(entity, true);

                _logger.LogDebug(GetLogMessage("{0} records added"), leadResult);

                if (leadResult > 0)
                {
                    var individualBonusResult = await _individualBonusIntents.Create(new CreateBonusIntentOptions()
                    {
                        LeadId = leadId
                    });

                    var organizationBonusResult = await _organizationBonusIntents.Create(new CreateBonusIntentOptions()
                    {
                        LeadId = leadId
                    });

                    if (individualBonusResult.Succeeded && organizationBonusResult.Succeeded)
                    {
                        retVal.Succeeded = true;
                        await Task.Run(() =>
                        {
                            RaiseEvent(new LeadQualifiedEvent
                            {
                                LeadId = leadId
                            });
                        });
                    }



                }

            }
            else
            {
                _logger.LogDebug(GetLogMessage("Lead is already qualified"));

            }

            return retVal;
        }
        
    }
}