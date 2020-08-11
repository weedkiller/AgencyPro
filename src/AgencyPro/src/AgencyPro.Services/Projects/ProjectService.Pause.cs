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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Projects
{
    public partial class ProjectService
    {
        private async Task<ProjectResult> PauseProject(Project entity)
        {
            _logger.LogInformation(GetLogMessage("Project: {0}"), entity.Id);
            var retVal = new ProjectResult()
            {
                ProjectId = entity.Id
            };

            if (entity.Status != ProjectStatus.Paused)
            {
                entity.UpdatedById = _userInfo.UserId;
                entity.Updated = DateTimeOffset.UtcNow;
                entity.Status = ProjectStatus.Paused;
                entity.ObjectState = ObjectState.Modified;

                entity.StatusTransitions.Add(new ProjectStatusTransition()
                {
                    Status = entity.Status,
                    ObjectState = ObjectState.Added
                });


                foreach (var contract in entity.Contracts)
                {
                    if (contract.Status == ContractStatus.Active)
                    {
                        contract.Status = ContractStatus.Paused;
                        contract.ObjectState = ObjectState.Modified;
                        contract.Updated = DateTimeOffset.UtcNow;
                        contract.UpdatedById = _userInfo.UserId;
                        contract.StatusTransitions.Add(new ContractStatusTransition()
                        {
                            ObjectState = ObjectState.Added,
                            Status = ContractStatus.Paused
                        });
                    }
                }

                var records = Repository.InsertOrUpdateGraph(entity, true);

                _logger.LogDebug(GetLogMessage("{0} records updated"), records);
                if (records > 0)
                {
                    retVal.Succeeded = true;
                    await Task.Run(() => RaiseEvent(new ProjectPausedEvent()
                    {
                        ProjectId = entity.Id
                    }));
                }
            }
            else
            {
                _logger.LogDebug(GetLogMessage("The project is already paused"));
                retVal.Succeeded = true;
            }

            return retVal;
        }

        public async Task<ProjectResult> PauseProject(IOrganizationCustomer cu, Guid projectId)
        {
            _logger.LogInformation(GetLogMessage("Cu: {0}; ProjectId: {1}"), cu.OrganizationId, projectId);

            var entity = Repository
                .Queryable()
                .Include(x => x.Contracts)

                .ForOrganizationCustomer(cu)
                .FirstOrDefault(x => x.Id == projectId);

            return await PauseProject(entity);
        }

        public async Task<ProjectResult> PauseProject(IProviderAgencyOwner ao, Guid projectId)
        {

            _logger.LogInformation(GetLogMessage("AO: {0}; ProjectId: {1}"), ao.OrganizationId, projectId);

            var entity = Repository
                .Queryable()
                .Include(x => x.Contracts)

                .ForAgencyOwner(ao)
                .FirstOrDefault(x => x.Id == projectId);

            return await PauseProject(entity);
        }

        public async Task<ProjectResult> PauseProject(IOrganizationAccountManager am, Guid projectId)
        {
            _logger.LogInformation(GetLogMessage("AM: {0}; ProjectId: {1}"), am.OrganizationId, projectId);

            var entity = Repository
                .Queryable()
                .Include(x => x.Contracts)

                .ForOrganizationAccountManager(am)
                .FirstOrDefault(x => x.Id == projectId);

            return await PauseProject(entity);
        }
    }
}