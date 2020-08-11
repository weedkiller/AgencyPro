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
        private void ProjectRestartedSendAgencyOwnerEmail(Guid projectId)
        {
            _logger.LogInformation(GetLogMessage("Project Id: {0}"), projectId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var project = context.Projects
                    .Where(x => x.Id == projectId)
                    .ProjectTo<ProviderAgencyOwnerProjectEmail>(ProjectionMapping)
                    .First();

                project.Initialize(Settings);

                Send(TemplateTypes.AgencyOwnerProjectRestarted, project,
                    $@"[{project.ProjectManagerOrganizationName}] A project was restarted");

                AddProjectNotification(context, "Project was restarted", project);

            }
        }

        public void Handle(ProjectRestartedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Project restarted event triggered"));

            Parallel.Invoke(new List<Action>
            {
                () => ProjectRestartedSendAgencyOwnerEmail(evt.ProjectId),
                () => ProjectRestartedSendAccountManagerEmail(evt.ProjectId),
                () => ProjectRestartedSendProjectManagerEmail(evt.ProjectId),
               // () => ProjectRestartedCustomerEmail(evt.ProjectId),
            }.ToArray());

        }
        

        private void ProjectRestartedSendProjectManagerEmail(Guid projectId)
        {
            _logger.LogInformation(GetLogMessage("Project restarted event triggered"));
            using (var context = new AppDbContext(DbContextOptions))
            {
                var project = context.Projects
                    .Where(x => x.Id == projectId)
                    .ProjectTo<ProjectManagerProjectEmail>(ProjectionMapping)
                    .First();

                project.Initialize(Settings);

                Send(TemplateTypes.ProjectManagerProjectRestarted, project,
                    $@"[{project.ProjectManagerOrganizationName}] A project was restarted");

                AddProjectNotification(context, "Project was restarted", project);

            }
        }

        private void ProjectRestartedSendAccountManagerEmail(Guid projectId)
        {
            _logger.LogInformation(GetLogMessage("Project restarted event triggered"));
            using (var context = new AppDbContext(DbContextOptions))
            {
                var project = context.Projects
                    .Where(x => x.Id == projectId)
                    .ProjectTo<AccountManagerProjectEmail>(ProjectionMapping)
                    .First();

                project.Initialize(Settings);

                Send(TemplateTypes.AccountManagerProjectRestarted, project,
                    $@"[{project.ProjectManagerOrganizationName}] A project was restarted");

                AddProjectNotification(context, "Project was restarted", project);

            }
        }
    }
}