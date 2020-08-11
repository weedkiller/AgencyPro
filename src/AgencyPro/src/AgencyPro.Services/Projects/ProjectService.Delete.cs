// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Projects.Events;
using AgencyPro.Core.Projects.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Projects
{
    public partial class ProjectService
    {
        public async Task<ProjectResult> DeleteProject(IProviderAgencyOwner agencyOwner, Guid projectId)
        {
            var retVal = new ProjectResult()
            {
                ProjectId = projectId
            };

            var project = await Repository.Queryable()
                .Include(x=>x.Stories)
                .Include(x=>x.Contracts)
                .ThenInclude(x=>x.TimeEntries)
                .FirstAsync(x => x.Id == projectId);

            project.Updated = DateTimeOffset.UtcNow;
            project.UpdatedById = _userInfo.UserId;
            project.ObjectState = ObjectState.Modified;
            project.IsDeleted = true;

            foreach (var contract in project.Contracts)
            {
                contract.Updated = DateTimeOffset.UtcNow;
                contract.UpdatedById = _userInfo.UserId;
                contract.IsDeleted = true;
                contract.ObjectState = ObjectState.Modified;

                foreach (var e in contract.TimeEntries)
                {
                    e.Updated = DateTimeOffset.UtcNow;
                    e.UpdatedById = _userInfo.UserId;
                    e.IsDeleted = true;
                    e.ObjectState = ObjectState.Modified;
                }
            }

            foreach (var story in project.Stories)
            {
                story.Updated = DateTimeOffset.UtcNow;
                story.IsDeleted = true;
                story.ObjectState = ObjectState.Modified;
            }

            var result = Repository.InsertOrUpdateGraph(project, true);

            _logger.LogDebug(GetLogMessage("{0} results updated"), result);

            if (result > 0)
            {
                retVal.Succeeded = true;

                await Task.Run(() =>
                {
                    RaiseEvent(new ProjectDeletedEvent()
                    {
                        ProjectId = project.Id
                    });
                });
            }

            return retVal;
        }
    }
}