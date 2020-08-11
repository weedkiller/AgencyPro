// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Contracts.Enums;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Projects.Enums;
using AgencyPro.Core.Projects.Events;
using AgencyPro.Core.Projects.Extensions;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.Projects.ViewModels;
using AgencyPro.Core.Stories.Enums;
using AgencyPro.Core.Stories.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Projects
{
    public partial class ProjectService
    {
        private async Task<ProjectResult> EndProject(Project entity)
        {
            _logger.LogInformation(GetLogMessage("Project: {0}"), entity.Id);

            var retVal = new ProjectResult()
            {
                ProjectId = entity.Id
            };

            if (entity.Status != ProjectStatus.Ended)
            {
                _logger.LogDebug(GetLogMessage("preparing to end project"));

                entity.UpdatedById = _userInfo.UserId;
                entity.Updated = DateTimeOffset.UtcNow;
                entity.Status = ProjectStatus.Ended;
                entity.ObjectState = ObjectState.Modified;

                entity.StatusTransitions.Add(new ProjectStatusTransition()
                {
                    Status = entity.Status,
                    ObjectState = ObjectState.Added
                });

                foreach (var contract in entity.Contracts)
                {
                    _logger.LogDebug(GetLogMessage("Ending contract: {0}"), contract.Id);

                    if (contract.Status != ContractStatus.Ended)
                    {
                        contract.Updated = DateTimeOffset.UtcNow;
                        contract.UpdatedById = _userInfo.UserId;
                        contract.Status = ContractStatus.Ended;
                        contract.StatusTransitions.Add(new ContractStatusTransition()
                        {
                            ObjectState = ObjectState.Added,
                            Status = ContractStatus.Ended
                        });
                    }
                }

                foreach (var story in entity.Stories)
                {
                    _logger.LogDebug(GetLogMessage("Ending story: {0}"), story.Id);

                    if (story.Status != StoryStatus.Archived)
                    {
                        story.Updated = DateTimeOffset.UtcNow;
                        story.Status = StoryStatus.Archived;
                        story.StatusTransitions.Add(new StoryStatusTransition()
                        {
                            ObjectState = ObjectState.Added,
                            Status = StoryStatus.Archived
                        });
                    }
                }

                var records = Repository.InsertOrUpdateGraph(entity, true);

                _logger.LogInformation(GetLogMessage("{0} records updated"), records);
                if (records > 0)
                {
                    retVal.Succeeded = true;
                    await Task.Run(() => RaiseEvent(new ProjectEndedEvent()
                    {
                        ProjectId = entity.Id
                    }));
                }
            }
            else
            {
                _logger.LogDebug(GetLogMessage("The project is already ended"));
                retVal.Succeeded = true;
            }

            return retVal;
        }
        public async Task<ProjectResult> EndProject(IProviderAgencyOwner ao, Guid projectId)
        {
            _logger.LogInformation(GetLogMessage("AO: {0}; ProjectId: {1}"), ao.OrganizationId, projectId);

            var entity = Repository
                .Queryable()
                .Include(x=>x.Contracts)
                .Include(x=>x.Stories)
                .ForAgencyOwner(ao)
                .FirstOrDefault(x => x.Id == projectId);

            return await EndProject(entity);
        }

        public async Task<ProjectResult> EndProject(IOrganizationCustomer cu, Guid projectId)
        {
            _logger.LogInformation(GetLogMessage("CU: {0}; ProjectId: {1}"), cu.OrganizationId, projectId);

            var entity = Repository
                .Queryable()
                .Include(x => x.Contracts)
                .Include(x => x.Stories)
                .ForOrganizationCustomer(cu)
                .Include(x => x.Stories)
                .FirstOrDefault(x => x.Id == projectId);

            return await EndProject(entity);
        }

        public async Task<ProjectResult> EndProject(IOrganizationAccountManager am, Guid projectId)
        {
            _logger.LogInformation(GetLogMessage("AM: {0}; ProjectId: {1}"), am.OrganizationId, projectId);

            var entity = Repository
                .Queryable()
                .Include(x=>x.Contracts)
                .Include(x=>x.Stories)
                .ForOrganizationAccountManager(am)
                .FirstOrDefault(x => x.Id == projectId);

            return await EndProject(entity);
        }
    }
}