// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.OrganizationRoles.Events.OrganizationAccountManagers;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationAccountManagers;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Services.OrganizationRoles.OrganizationAccountManagers
{
    public partial class OrganizationAccountManagerService
    {
        public async Task<T> Create<T>(OrganizationAccountManagerInput model) where T : OrganizationAccountManagerOutput
        {
            _logger.LogTrace(
                GetLogMessage($@"OrganizationId: {model.OrganizationId}, AccountManagerId: {model.AccountManagerId}"));

            var entity = await Repository.Queryable().IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.OrganizationId == model.OrganizationId && 
                                          x.AccountManagerId == model.AccountManagerId);

            if (entity != null)
            {
                if (entity.IsDeleted)
                {
                    entity.IsDeleted = false;
                    entity.UpdatedById = _userInfo.UserId;
                    entity.Updated = DateTimeOffset.UtcNow;
                    entity.InjectFrom(model);

                    await Repository.UpdateAsync(entity, true);
                }
            }
            else
            {
                entity = new OrganizationAccountManager
                {
                    OrganizationId = model.OrganizationId,
                    AccountManagerId = model.AccountManagerId,
                    AccountManagerStream = model.AccountManagerStream,
                    Created = DateTimeOffset.UtcNow,
                    Updated = DateTimeOffset.UtcNow,
                    UpdatedById = _userInfo.UserId,
                    CreatedById = _userInfo.UserId
                }.InjectFrom(model) as OrganizationAccountManager;

                await Repository.InsertAsync(entity, true);
            }
            
            var output = await GetById<T>(model.AccountManagerId, model.OrganizationId);

            await Task.Run(() =>
            {
                RaiseEvent(new OrganizationAccountManagerCreatedEvent
                {
                    OrganizationAccountManager = output
                });
            });

            return output;

        }

        public Task<T> Create<T>(IAgencyOwner ao, OrganizationAccountManagerInput model)
            where T : OrganizationAccountManagerOutput
        {
            model.OrganizationId = ao.OrganizationId;
            return Create<T>(model);
        }


    }
}