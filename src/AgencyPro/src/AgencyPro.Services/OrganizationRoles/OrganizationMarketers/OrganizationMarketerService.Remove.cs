// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyPro.Services.OrganizationRoles.OrganizationMarketers
{
    public partial class OrganizationMarketerService
    {
        public async Task<bool> Remove(IAgencyOwner ao, Guid personId, bool commit = true)
        {
            _logger.LogTrace(
               GetLogMessage($@"Marketer: {personId}, Organization: {ao.OrganizationId}"));

            var entity = _personRepository.Queryable()
                .Include(x => x.Marketer)
                .ThenInclude(x => x.Leads)
                .FirstOrDefault(x => x.PersonId == personId && x.OrganizationId == ao.OrganizationId);

            var organization = await _orgService.Repository.Queryable()
                .Include(x => x.MarketingOrganization)
                .Include(x => x.Marketers)
                .Where(x => x.Id == ao.OrganizationId)
                .FirstAsync();

            if (entity.Marketer.Leads.Count > 0)
            {
                foreach (var lead in entity.Marketer.Leads)
                {
                    lead.MarketerId = organization.MarketingOrganization.DefaultMarketerId;
                    lead.ObjectState = ObjectState.Modified;
                    lead.UpdatedById = _userInfo.UserId;
                    lead.Updated = DateTimeOffset.UtcNow;
                }
            }

            entity.Marketer.IsDeleted = true;
            entity.Marketer.UpdatedById = _userInfo.UserId;
            entity.Marketer.ObjectState = ObjectState.Modified;


            entity.Updated = DateTimeOffset.UtcNow;
            entity.UpdatedById = _userInfo.UserId;
            entity.ObjectState = ObjectState.Modified;

            int result = _personRepository.InsertOrUpdateGraph(entity, true);
            return result > 0;

            //await Task.Run(() =>
            //{
            //    RaiseEvent(new OrganizationMarketerRemovedEvent<OrganizationMarketerOutput>
            //    {
            //        OrganizationMarketerOutput = output
            //    });
            //});
        }

    }
}