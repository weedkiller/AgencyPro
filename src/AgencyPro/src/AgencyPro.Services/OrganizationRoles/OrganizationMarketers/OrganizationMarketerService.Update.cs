// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Events.OrganizationMarketers;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationMarketers;
using AgencyPro.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.OrganizationRoles.OrganizationMarketers
{
    public partial class OrganizationMarketerService
    {
        public Task<T> Update<T>(IAgencyOwner agencyOwner,
            OrganizationMarketerInput input)
            where T : OrganizationMarketerOutput
        {
            input.OrganizationId = agencyOwner.OrganizationId;
            return Update<T>(input);
        }

        public Task<T> Update<T>(IOrganizationAccountManager am, OrganizationMarketerInput input) where T : OrganizationMarketerOutput
        {
            throw new NotImplementedException();
        }

        public async Task<T> Update<T>(OrganizationMarketerInput input)
            where T : OrganizationMarketerOutput
        {
            _logger
                .LogTrace(GetLogMessage($@"Marketer Id: {input.MarketerId}, Organization Id: {input.OrganizationId}"));

            var entity = await Repository.Queryable().GetById(input.OrganizationId, input.MarketerId)
                .FirstAsync();

            entity.Updated = DateTimeOffset.UtcNow;
            entity.MarketerStream = input.MarketerStream;
            entity.MarketerBonus = input.MarketerBonus;

            Repository.UpdateAsync(entity, true)
                .Wait();

            var output = await GetById<T>(input.MarketerId, input.OrganizationId);

            await Task.Run(() =>
            {
                RaiseEvent(new OrganizationMarketerUpdatedEvent<T>
                {
                    OrganizationMarketerOutput = output
                });
            });

            return output;
        }
    }
}