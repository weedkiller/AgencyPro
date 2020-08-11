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
        private static string GetPausedSubjectLine(string organizationName, string department, string project)
        {
            return $"[{organizationName} : {department}] Project '{project}' has been paused";
        }

        private void ProjectPausedSendProjectManagerEmail(Guid projectId)
        {
            _logger.LogDebug(GetLogMessage("Project: {0}"), projectId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var project = context.Projects
                    .Where(x => x.Id == projectId)
                    .ProjectTo<ProjectManagerProjectEmail>(ProjectionMapping)
                    .First();

                project.Initialize(Settings);

                Send(TemplateTypes.ProjectManagerProjectPaused, 
                    project, GetPausedSubjectLine(project.ProjectManagerOrganizationName, "Project Management", project.Name));

                AddProjectNotification(context, "Project was paused", project);

            }
        }

        private void ProjectPausedSendAccountManagerEmail(Guid projectId)
        {
            _logger.LogDebug(GetLogMessage("Project: {0}"), projectId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var project = context.Projects
                    .Where(x => x.Id == projectId)
                    .ProjectTo<AccountManagerProjectEmail>(ProjectionMapping)
                    .First();

                project.Initialize(Settings);

                Send(TemplateTypes.AccountManagerProjectPaused, project, GetPausedSubjectLine(project.ProjectManagerOrganizationName, "Account Management", project.Name));

                AddProjectNotification(context, "Project was paused", project);

            }


        }

        private void ProjectPausedSendAgencyOwnerEmail(Guid projectId)
        {
            _logger.LogDebug(GetLogMessage("Project: {0}"), projectId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var project = context.Projects
                    .Where(x => x.Id == projectId)
                    .ProjectTo<ProviderAgencyOwnerProjectEmail>(ProjectionMapping)
                    .First();

                project.Initialize(Settings);

                Send(TemplateTypes.AgencyOwnerProjectPaused, project, $"[{project.ProjectManagerOrganizationName}] Project Paused");

                AddProjectNotification(context, "Project was paused", project);

            }

        }

        public void Handle(ProjectPausedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Pausing Project: {0}"), evt.ProjectId);

            Parallel.Invoke(new List<Action>
            {
                () => ProjectPausedSendProjectManagerEmail(evt.ProjectId),
                () => ProjectPausedSendAccountManagerEmail(evt.ProjectId),
                () => ProjectPausedSendAgencyOwnerEmail(evt.ProjectId)
            }.ToArray());
        }
    }
}