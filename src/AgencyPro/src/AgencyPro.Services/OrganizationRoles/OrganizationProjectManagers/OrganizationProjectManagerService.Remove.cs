// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Projects.Enums;

namespace AgencyPro.Services.OrganizationRoles.OrganizationProjectManagers
{
    public partial class OrganizationProjectManagerService
    {
        public async Task<bool> Remove(IAgencyOwner ao, Guid personId, bool commit = true)
        {
            _logger.LogTrace(
                GetLogMessage(
                    $@"OrganizationId: {ao.OrganizationId}, ProjectManagerId: {personId}"));

            var entity = _personRepository.Queryable()
                .Include(x => x.ProjectManager)
                .ThenInclude(x=>x.Projects)
                .FirstOrDefault(x => x.PersonId == personId && x.OrganizationId == ao.OrganizationId);

            var organization = await _orgService.Repository.Queryable()
                .Include(x=>x.ProviderOrganization)
                .Where(x => x.Id == ao.OrganizationId)
                .Include(x=>x.ProjectManagers)
                .FirstOrDefaultAsync();

            if (entity.ProjectManager.Projects.Count > 0)
            {
                foreach (var project in entity.ProjectManager.Projects)
                {
                    project.ProjectManagerId = organization.ProviderOrganization.DefaultProjectManagerId;

                    project.ObjectState = ObjectState.Modified;
                    project.Updated = DateTimeOffset.UtcNow;
                    project.UpdatedById = _userInfo.UserId;
                    project.Status = ProjectStatus.Inactive;
                }
            }

            entity.ProjectManager.IsDeleted = true;
            entity.ProjectManager.ObjectState = ObjectState.Modified;
            entity.Updated = DateTimeOffset.UtcNow;
            entity.UpdatedById = _userInfo.UserId;

            entity.Updated = DateTimeOffset.UtcNow;
            entity.UpdatedById = _userInfo.UserId;
            entity.ObjectState = ObjectState.Modified;

            int result = _personRepository.InsertOrUpdateGraph(entity, commit);
            return result > 0;
        }
    }
}