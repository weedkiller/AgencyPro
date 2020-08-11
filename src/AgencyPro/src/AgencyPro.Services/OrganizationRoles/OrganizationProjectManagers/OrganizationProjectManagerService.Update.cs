// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Events.OrganizationProjectManagers;
using AgencyPro.Core.OrganizationRoles.Extensions;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationProjectManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace AgencyPro.Services.OrganizationRoles.OrganizationProjectManagers
{
    public partial class OrganizationProjectManagerService
    {
        public Task<T> Update<T>(IAgencyOwner ao, OrganizationProjectManagerInput input)
            where T : OrganizationProjectManagerOutput
        {
            input.OrganizationId = ao.OrganizationId;
            return Update<T>(input);
        }

        public Task<T> Update<T>(IOrganizationAccountManager am, OrganizationProjectManagerInput input) where T : OrganizationProjectManagerOutput
        {
            input.OrganizationId = am.OrganizationId;
            return Update<T>(input);
        }

        private async Task<T> Update<T>(OrganizationProjectManagerInput model)
            where T : OrganizationProjectManagerOutput
        {
            _logger.LogTrace(
                GetLogMessage(
                    $@"OrganizationId: {model.OrganizationId}, ProjectManagerId: {model.ProjectManagerId}"));

            // todo: use firstAsync instead of firstOrDefaultAsync() -rod
            var entity = await Repository.Queryable()
                .FindById(model.ProjectManagerId, model.OrganizationId)
                .FirstAsync();

            entity.InjectFrom(model);
            entity.Updated = DateTimeOffset.UtcNow;

            await Repository.UpdateAsync(entity, true);

            var result = await GetById<T>(model.ProjectManagerId, model.OrganizationId);

            await Task.Run(() =>
            {
                // todo: i like this way of doing it as opposed to generics -rod
                RaiseEvent(new OrganizationProjectManagerUpdatedEvent
                {
                    OrganizationProjectManager = result
                });
            });

            return result;
        }
        
    }
}