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
        public void Handle(TimeEntryApprovedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Handling event: {event}"), evt);

            Parallel.Invoke(new List<Action>
            {
                () => TimeEntryApprovedSendAccountManagerEmail(evt.TimeEntryId),
                () => TimeEntryApprovedSendProjectManagerEmail(evt.TimeEntryId),
                () => TimeEntryApprovedSendContractorEmail(evt.TimeEntryId),
                () => TimeEntryApprovedSendAgencyOwnerEmail(evt.TimeEntryId),
                () => TimeEntryApprovedSendMarketingAgencyOwnerEmail(evt.TimeEntryId),
                () => TimeEntryApprovedSendRecruitingAgencyOwnerEmail(evt.TimeEntryId),
                () => TimeEntryApprovedSendRecruiterEmail(evt.TimeEntryId),
                () => TimeEntryApprovedSendMarketerEmail(evt.TimeEntryId)
            }.ToArray());
        }

        private void TimeEntryApprovedSendProjectManagerEmail(Guid timeEntryId)
        {
            _logger.LogInformation(GetLogMessage("Time Entry: {0}"), timeEntryId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var entity = context.TimeEntries
                    .Where(x => x.Id == timeEntryId)
                    .ProjectTo<ProjectManagerTimeEntryEmail>(ProjectionMapping)
                    .First();


                entity.Initialize(Settings);

                Send(TemplateTypes.ProjectManagerTimeEntryApproved, entity,
                    $@"[{entity.ContractorOrganizationName}] Time entry approved");
            }
            

        }

        private void TimeEntryApprovedSendAccountManagerEmail(Guid timeEntryId)
        {
            _logger.LogInformation(GetLogMessage("Time Entry: {0}"), timeEntryId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var entity = context.TimeEntries
                    .Where(x => x.Id == timeEntryId)
                    .ProjectTo<AccountManagerTimeEntryEmail>(ProjectionMapping)
                    .First();


                entity.Initialize(Settings);

                Send(TemplateTypes.AccountManagerTimeEntryApproved, entity,
                    $@"[{entity.ContractorOrganizationName}] Time entry approved");
            }
           

        }

        private void TimeEntryApprovedSendRecruiterEmail(Guid timeEntryId)
        {
            _logger.LogInformation(GetLogMessage("Time Entry: {0}"), timeEntryId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var entity = context.TimeEntries
                    .Where(x => x.Id == timeEntryId)
                    .ProjectTo<RecruiterTimeEntryEmail>(ProjectionMapping)
                    .First();

                entity.Initialize(Settings);

                Send(TemplateTypes.RecruiterTimeEntryApproved, entity,
                    $@"[{entity.RecruitingOrganizationName}] Time entry approved");
            }
            
        }

        private void TimeEntryApprovedSendMarketerEmail(Guid timeEntryId)
        {
            _logger.LogInformation(GetLogMessage("Time Entry: {0}"), timeEntryId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var entity = context.TimeEntries
                    .Where(x => x.Id == timeEntryId)
                    .ProjectTo<MarketerTimeEntryEmail>(ProjectionMapping)
                    .First();

                entity.Initialize(Settings);

                Send(TemplateTypes.MarketerTimeEntryApproved, entity,
                    $@"[{entity.MarketingOrganizationName}] Time entry approved");
            }
            

        }

        private void TimeEntryApprovedSendContractorEmail(Guid timeEntryId)
        {
            _logger.LogInformation(GetLogMessage("Time Entry: {0}"), timeEntryId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var entity = context.TimeEntries
                    .Where(x => x.Id == timeEntryId)
                    .ProjectTo<ContractorTimeEntryEmail>(ProjectionMapping)
                    .First();

                entity.Initialize(Settings);

                Send(TemplateTypes.ContractorTimeEntryApproved, entity,
                    $@"[{entity.ContractorOrganizationName}] Time entry approved");
            }
            
        }

        private void TimeEntryApprovedSendAgencyOwnerEmail(Guid timeEntryId)
        {
            _logger.LogInformation(GetLogMessage("Time Entry: {0}"), timeEntryId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var entity = context.TimeEntries
                    .Where(x => x.Id == timeEntryId)
                    .ProjectTo<AgencyOwnerTimeEntryEmail>(ProjectionMapping)
                    .First();

                entity.Initialize(Settings);

                Send(TemplateTypes.AgencyOwnerTimeEntryApproved, entity,
                    $@"[{entity.ContractorOrganizationName}] Time entry approved");
            }

            
        }

        private void TimeEntryApprovedSendRecruitingAgencyOwnerEmail(Guid timeEntryId)
        {
            _logger.LogInformation(GetLogMessage("Time Entry: {0}"), timeEntryId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var entity = context.TimeEntries
                    .Where(x => x.Id == timeEntryId)
                    .ProjectTo<RecruitingAgencyOwnerTimeEntryEmail>(ProjectionMapping)
                    .First();

                entity.Initialize(Settings);

                if (entity.IsRecruitingExternal)
                {

                    Send(TemplateTypes.RecruitingAgencyOwnerTimeEntryApproved, entity,
                        $@"[{entity.RecruitingOrganizationName}] Time entry approved");
                }
            }

            

        }

        private void TimeEntryApprovedSendMarketingAgencyOwnerEmail(Guid timeEntryId)
        {
            _logger.LogInformation(GetLogMessage("Time Entry: {0}"), timeEntryId);
            using (var context = new AppDbContext(DbContextOptions))
            {

                var entity = context.TimeEntries
                    .Where(x => x.Id == timeEntryId)
                    .ProjectTo<MarketingAgencyOwnerTimeEntryEmail>(ProjectionMapping)
                    .First();

                entity.Initialize(Settings);

                if (entity.IsMarketingExternal)
                {
                    Send(TemplateTypes.MarketingAgencyOwnerTimeEntryApproved, entity,
                        $@"[{entity.MarketingOrganizationName}] Time entry approved");
                }
            }


        }
    }
}