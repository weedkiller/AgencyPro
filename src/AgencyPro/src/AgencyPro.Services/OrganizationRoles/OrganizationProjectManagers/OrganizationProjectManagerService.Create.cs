// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.OrganizationRoles.Events.OrganizationProjectManagers;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationProjectManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyPro.Services.OrganizationRoles.OrganizationProjectManagers
{
    public partial class OrganizationProjectManagerService
    {
        public async Task<T> Create<T>(OrganizationProjectManagerInput model) where T : OrganizationProjectManagerOutput
        {
            _logger.LogTrace(
                GetLogMessage($@"OrganizationId: {model.OrganizationId}, ProjectManagerId: {model.ProjectManagerId}"));

            var entity = await Repository.Queryable().IgnoreQueryFilters().Where(x =>
                    x.OrganizationId == model.OrganizationId && x.ProjectManagerId == model.ProjectManagerId)
                .FirstOrDefaultAsync();

            if (entity != null)
            {
                if (entity.IsDeleted)
                {
                    entity.IsDeleted = false;
                    entity.UpdatedById = _userInfo.UserId;
                    entity.Updated = DateTimeOffset.UtcNow;
                    entity.CreatedById = _userInfo.UserId;
                    entity.Created =DateTimeOffset.UtcNow;
                    
                    entity.InjectFrom(model);

                    await Repository.UpdateAsync(entity, true);
                }
            }
            else
            {
                entity = new OrganizationProjectManager
                {
                    OrganizationId = model.OrganizationId,
                    ProjectManagerId = model.ProjectManagerId,
                    ProjectManagerStream = model.ProjectManagerStream,
                    Updated = DateTimeOffset.UtcNow,
                    UpdatedById = _userInfo.UserId,
                    Created = DateTimeOffset.UtcNow,
                    CreatedById = _userInfo.UserId
                };

                await Repository.InsertAsync(entity, true);
            }


            var output = await GetById<T>(model.ProjectManagerId, model.OrganizationId);

            await Task.Run(() =>
            {
                RaiseEvent(new OrganizationProjectManagerCreatedEvent { OrganizationProjectManager = output });
            });

            return output;

        }

        public Task<T> Create<T>(IAgencyOwner ao, OrganizationProjectManagerInput input)
            where T : OrganizationProjectManagerOutput
        {
            input.OrganizationId = ao.OrganizationId;
            return Create<T>(input);
        }
    }
}