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
        private void ProjectEndedSendProjectManagerEmail(Guid projectId)
        {
            _logger.LogDebug(GetLogMessage("Project: {0}"), projectId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var project = context.Projects
                    .Where(x => x.Id == projectId)
                    .ProjectTo<ProjectManagerProjectEmail>(ProjectionMapping)
                    .First();

                project.Initialize(Settings);

                Send(TemplateTypes.ProjectManagerProjectEnded, project,
                    $@"[{project.ProjectManagerOrganizationName} : Project Management] A project has ended");

                AddProjectNotification(context, "Project was ended", project);

            }

        }

        private void ProjectEndedSendAccountManagerEmail(Guid projectId)
        {
            _logger.LogDebug(GetLogMessage("Project: {0}"), projectId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var project = context.Projects
                    .Where(x => x.Id == projectId)
                    .ProjectTo<AccountManagerProjectEmail>(ProjectionMapping)
                    .First();

                project.Initialize(Settings);

                Send(TemplateTypes.AccountManagerProjectEnded, project,
                    $@"[{project.AccountManagerOrganizationName} : Account Management] A project has ended");

                AddProjectNotification(context, "Project was ended", project);

            }

        }

        private void ProjectEndedSendAgencyOwnerEmail(Guid projectId)
        {
            _logger.LogDebug(GetLogMessage("Project: {0}"), projectId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var project = context.Projects
                    .Where(x => x.Id == projectId)
                    .ProjectTo<ProviderAgencyOwnerProjectEmail>(ProjectionMapping)
                    .First();

                project.Initialize(Settings);

                Send(TemplateTypes.AgencyOwnerProjectEnded, project,
                    $@"[{project.ProjectManagerOrganizationName}] A project has ended");

                AddProjectNotification(context, "Project was ended", project);

            }

        }

        public void Handle(ProjectEndedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Ending Project: {0}"), evt.ProjectId);
            
            Parallel.Invoke(new List<Action>
            {
                () => ProjectEndedSendProjectManagerEmail(evt.ProjectId),
                () => ProjectEndedSendAccountManagerEmail(evt.ProjectId),
                () => ProjectEndedSendAgencyOwnerEmail(evt.ProjectId)
            }.ToArray());
        }
    }
}