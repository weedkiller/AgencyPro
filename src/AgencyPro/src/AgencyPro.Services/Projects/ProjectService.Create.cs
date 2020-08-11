// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.CustomerAccounts.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationProjectManagers;
using AgencyPro.Core.Projects.Events;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.Projects.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Projects.Enums;

namespace AgencyPro.Services.Projects
{
    public partial class ProjectService
    {
        public Task<ProjectResult> CreateProject(IProviderAgencyOwner ao, ProjectInput input)
        {
            input.ProjectManagerOrganizationId = ao.OrganizationId;
            return CreateProject(input);
        }

        public Task<ProjectResult> CreateProject(IOrganizationAccountManager am, ProjectInput input)
        {
            input.ProjectManagerOrganizationId = am.OrganizationId;
            return CreateProject(input);
        }

        public Task<ProjectResult> CreateProject(IOrganizationProjectManager pm, ProjectInput input)
        {
            input.ProjectManagerOrganizationId = pm.OrganizationId;
            input.ProjectManagerId = pm.OrganizationId;

            return CreateProject(input);
        }

        private async Task<ProjectResult> CreateProject(ProjectInput input)
        {
            _logger.LogInformation(GetLogMessage("Creating Project: {@input}"), input);
            var retVal = new ProjectResult();
            
            var candidate = await Repository.Queryable().Where(x =>
                x.Abbreviation == input.Abbreviation &&
                x.ProjectManagerOrganizationId == input.ProjectManagerOrganizationId 
                || x.ProjectManagerOrganizationId == input.ProjectManagerOrganizationId && x.Name == input.Name)
                .FirstOrDefaultAsync();

            if (candidate != null)
            {
                retVal.ErrorMessage = "Project with same name or abbreviation already exists";
                return retVal;
            }

            var pm = _pmService.GetById<OrganizationProjectManagerOutput>(input.ProjectManagerId,
                input.ProjectManagerOrganizationId);

            var acct = _accountService.GetAccount<CustomerAccountOutput>(input.ProjectManagerOrganizationId,
                input.AccountNumber);

            await Task.WhenAll(pm, acct);

            if (acct.Result == null)
            {
                retVal.ErrorMessage = "No customer account found";
                return retVal;
            }

            _logger.LogDebug(GetLogMessage("Preparing to create project"));

            var project = new Project
            {
                Name = input.Name,
                CustomerId = acct.Result.CustomerId,
                CustomerOrganizationId = acct.Result.CustomerOrganizationId,
                ProjectManagerId = pm.Result.ProjectManagerId,
                ProjectManagerOrganizationId = pm.Result.OrganizationId,
                AccountManagerId = acct.Result.AccountManagerId,
                AccountManagerOrganizationId = acct.Result.AccountManagerOrganizationId,
                UpdatedById = _userInfo.UserId,
                CreatedById = _userInfo.UserId,
                Status = ProjectStatus.Pending,
                Abbreviation = input.Abbreviation,
                AutoApproveTimeEntries = acct.Result.AutoApproveTimeEntries
            }.InjectFrom(input) as Project;

            project.StatusTransitions.Add(new ProjectStatusTransition()
            {
                Status = project.Status,
                ObjectState = ObjectState.Added
            });


            var records = await Repository.InsertAsync(project, true);

            _logger.LogDebug(GetLogMessage("{0} Records Updated"), records);

            if (records > 0)
            {
                retVal.Succeeded = true;
                retVal.ProjectId = project.Id;
                await Task.Run(() =>
                {
                    RaiseEvent(new ProjectCreatedEvent
                    {
                        ProjectId = project.Id
                    });
                });
            }

            return retVal;
        }
    }
}