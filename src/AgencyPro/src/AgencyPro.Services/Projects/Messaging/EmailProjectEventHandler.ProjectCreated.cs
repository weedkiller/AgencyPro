// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.EmailSending.Services;
using AgencyPro.Core.Projects.Emails;
using AgencyPro.Core.Projects.Events;
using AgencyPro.Core.Templates.Models;
using AgencyPro.Data.EFCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Projects.Messaging
{
    public partial class MultiProjectEventHandler
    {
        private void ProjectCreatedSendProjectManagerEmail(Guid projectId)
        {
            _logger.LogInformation(GetLogMessage("Project: {0}"), projectId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var project = context.Projects
                    .Where(x => x.Id == projectId)
                    .ProjectTo<ProjectManagerProjectEmail>(ProjectionMapping)
                    .First();


                project.Initialize(Settings);

                Send(TemplateTypes.ProjectManagerProjectCreated, project,
                    $@"[{project.ProjectManagerOrganizationName} : Project Management] You have been assigned a new Project: {project.Name}");

                AddProjectNotification(context, "Project was created", project);

            }

        }

        private void ProjectCreatedSendAccountManagerEmail(Guid projectId)
        {
            _logger.LogInformation(GetLogMessage("Project: {0}"), projectId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var project = context.Projects
                    .Where(x => x.Id == projectId)
                    .ProjectTo<AccountManagerProjectEmail>(ProjectionMapping)
                    .First();


                project.Initialize(Settings);

                Send(TemplateTypes.AccountManagerProjectCreated, project,
                    $@"[{project.ProjectManagerOrganizationName} : Account Management] A new project was created for your account");

                AddProjectNotification(context, "Project was created", project);

            }

        }

        private void ProjectCreatedSendAgencyOwnerEmail(Guid projectId)
        {
            _logger.LogInformation(GetLogMessage("Project: {0}"), projectId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var project = context.Projects
                    .Where(x => x.Id == projectId)
                    .ProjectTo<ProviderAgencyOwnerProjectEmail>(ProjectionMapping)
                    .First();

                project.Initialize(Settings);

                Send(TemplateTypes.AgencyOwnerProjectCreated, project,
                    $@"[{project.ProjectManagerOrganizationName}] A new project was created in your organization");

                AddProjectNotification(context, "Project was created", project);

            }

        }

        public void Handle(ProjectCreatedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Project Created Event Triggered"));

            Parallel.Invoke(new List<Action>
            {
                () => ProjectCreatedSendProjectManagerEmail(evt.ProjectId),
                () => ProjectCreatedSendAccountManagerEmail(evt.ProjectId),
                () => ProjectCreatedSendAgencyOwnerEmail(evt.ProjectId)
            }.ToArray());
        }
    }
}