// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Events.OrganizationMarketers;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationMarketers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace AgencyPro.Services.OrganizationRoles.OrganizationMarketers
{
    public partial class OrganizationMarketerService
    {
        public async Task<T> Create<T>(OrganizationMarketerInput model)
            where T : OrganizationMarketerOutput
        {
            _logger.LogInformation(GetLogMessage($@"Marketer: {model.MarketerId}, Organization: {model.OrganizationId}"));

            var entity = await Repository.Queryable().IgnoreQueryFilters()
                .FirstOrDefaultAsync(x =>
                    x.MarketerId == model.MarketerId && 
                    x.OrganizationId == model.OrganizationId);

            if (entity != null)
            {
                _logger.LogDebug(GetLogMessage("Existing marketer found, deleted: {0}"), entity.IsDeleted);

                if (entity.IsDeleted)
                {
                    _logger.LogDebug(GetLogMessage("un-deleting org_marketer"));

                    entity.IsDeleted = false;
                    entity.UpdatedById = _userInfo.UserId;
                    entity.Updated = DateTimeOffset.UtcNow;
                    entity.MarketerStream = model.MarketerStream;
                    entity.MarketerBonus = model.MarketerBonus;

                    entity.InjectFrom(model);

                    var marketerOutput = await Repository.UpdateAsync(entity, true);

                    _logger.LogDebug(GetLogMessage("{0} records updated in database"), marketerOutput);
                }
            }
            else
            {
                _logger.LogDebug(GetLogMessage("No marketer found, creating..."));

                entity = new OrganizationMarketer()
                {
                    UpdatedById = _userInfo.UserId,
                    CreatedById = _userInfo.UserId,
                }.InjectFrom(model) as OrganizationMarketer;

                var marketerOutput = await Repository.InsertAsync(entity, true);

                _logger.LogDebug(GetLogMessage("{0} records updated in database"), marketerOutput);
            }

            var output = await GetById<T>(model.MarketerId, model.OrganizationId);

            await Task.Run(() =>
            {
                RaiseEvent(new OrganizationMarketerCreatedEvent<T>
                {
                    OrganizationMarketerOutput = output
                });
            });
            return output;
        }

        public Task<T> Create<T>(IAgencyOwner ao, OrganizationMarketerInput input)
            where T : OrganizationMarketerOutput
        {
            _logger.LogInformation(GetLogMessage("ao creating org marketer"));

            input.OrganizationId = ao.OrganizationId;
            return Create<T>(input);
        }
    }
}