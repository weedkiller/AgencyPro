// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Extensions;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Projects.Events;
using AgencyPro.Core.Projects.Extensions;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.Projects.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace AgencyPro.Services.Projects
{
    public partial class ProjectService
    {
        public async Task<ProjectResult> UpdateProject(IProviderAgencyOwner ao, Guid projectId, UpdateProjectInput model)
        {
            var entity = await Repository.Queryable().ForAgencyOwner(ao).FindById(projectId).FirstOrDefaultAsync();

            entity.InjectFrom<NullableInjection>(model);

            return await UpdateProject(entity);
        }

        public async Task<ProjectResult> UpdateProject(IOrganizationAccountManager am, Guid projectId,
            UpdateProjectInput model)
        {
            var entity = await Repository.Queryable()
                .ForOrganizationAccountManager(am)
                .FindById(projectId)
                .FirstOrDefaultAsync();

            entity.InjectFrom<NullableInjection>(model);

            return await UpdateProject(entity);
        }

        public async Task<ProjectResult> UpdateProject(IOrganizationProjectManager pm, Guid projectId,
            UpdateProjectInput model)
        {
            var entity = await Repository.Queryable()
                .ForOrganizationProjectManager(pm)
                .FindById(projectId)
                .FirstOrDefaultAsync();
            entity.Updated = DateTimeOffset.UtcNow;
            
            entity.InjectFrom<NullableInjection>(model);

            return await UpdateProject(entity);
        }

        private async Task<ProjectResult> UpdateProject(Project project)
        {
            project.Updated = DateTimeOffset.UtcNow;
            project.UpdatedById = _userInfo.UserId;

           var result = await Repository.UpdateAsync(project, true);
            _logger.LogDebug(GetLogMessage("{0} records updated"), result);
           if (result > 0)
           {

               await Task.Run(() =>
               {
                   RaiseEvent(new ProjectUpdatedEvent { ProjectId = project.Id });
               });

            }

            return new ProjectResult()
            {
                ProjectId = project.Id,
                Succeeded = result>0
            };
        }
    }
}