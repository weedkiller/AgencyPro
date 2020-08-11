// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.EmailSending.Services;
using AgencyPro.Core.Templates.Models;
using AgencyPro.Core.TimeEntries.Emails;
using AgencyPro.Core.TimeEntries.Events;
using AgencyPro.Data.EFCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.TimeEntries.EventHandlers
{
    public partial class TimeEntryEventHandlers
    {
        private void TimeEntryLoggedSendContractorEmail(Guid timeEntryId)
        {
            _logger.LogInformation(GetLogMessage("Time Entry: {0}"), timeEntryId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var entity = context.TimeEntries
                    .Where(x => x.Id == timeEntryId)
                    .ProjectTo<ContractorTimeEntryEmail>(ProjectionMapping)
                    .First();

                entity.Initialize(Settings);

                Send(TemplateTypes.ContractorTimeEntryCreated, entity,
                    $@"[{entity.ContractorOrganizationName}] Time Entry For: {entity.ContractorName} ({entity.TotalHours}/hrs)");
            }

            
        }

        private void TimeEntryLoggedSendProjectManagerEmail(Guid timeEntryId)
        {
            _logger.LogInformation(GetLogMessage("Time Entry: {0}"), timeEntryId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var entity = context.TimeEntries
                    .Where(x => x.Id == timeEntryId)
                    .ProjectTo<ProjectManagerTimeEntryEmail>(ProjectionMapping)
                    .First();

                entity.Initialize(Settings);

                Send(TemplateTypes.ProjectManagerTimeEntryCreated, entity,
                    $@"[{entity.ContractorOrganizationName}] Time entry ready for approval");
            }
        }

        private void TimeEntryLoggedSendAccountManagerEmail(Guid timeEntryId)
        {
            _logger.LogInformation(GetLogMessage("Time Entry: {0}"), timeEntryId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var entity = context.TimeEntries
                    .Where(x => x.Id == timeEntryId)
                    .ProjectTo<AccountManagerTimeEntryEmail>(ProjectionMapping)
                    .First();

                entity.Initialize(Settings);

                Send(TemplateTypes.AccountManagerTimeEntryCreated, entity,
                    $@"[{entity.ContractorOrganizationName}] Time Entry For: {entity.ContractorName} ({entity.TotalHours}/hrs)");
            }
        }

        private void TimeEntryLoggedSendAgencyOwnerEmail(Guid timeEntryId)
        {
            _logger.LogInformation(GetLogMessage("Time Entry: {0}"), timeEntryId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var entity = context.TimeEntries
                    .Where(x => x.Id == timeEntryId)
                    .ProjectTo<AgencyOwnerTimeEntryEmail>(ProjectionMapping)
                    .First();

                entity.Initialize(Settings);

                Send(TemplateTypes.AgencyOwnerTimeEntryCreated, entity,
                    $@"[{entity.ContractorOrganizationName}] Time Entry For: {entity.ContractorName} ({entity.TotalHours}/hrs)");
            }
        }

     
        public void Handle(TimeEntryLoggedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Handling event: {event}"), evt);
            
            Parallel.Invoke(new List<Action>
            {
                () => TimeEntryLoggedSendAgencyOwnerEmail(evt.TimeEntryId),
                () => TimeEntryLoggedSendAccountManagerEmail(evt.TimeEntryId),
                () => TimeEntryLoggedSendProjectManagerEmail(evt.TimeEntryId),
                () => TimeEntryLoggedSendContractorEmail(evt.TimeEntryId)
            }.ToArray());
        }
    }
}