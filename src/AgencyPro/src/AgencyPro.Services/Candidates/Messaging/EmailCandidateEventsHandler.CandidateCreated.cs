// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Candidates.Emails;
using AgencyPro.Core.Candidates.Events;
using AgencyPro.Core.EmailSending.Services;
using AgencyPro.Core.Templates.Models;
using AgencyPro.Data.EFCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Candidates.Messaging
{
    public partial class MultiCandidateEventsHandler
    {
        private void CandidateCreatedRecruiterEmail(Guid candidateId)
        {
            _logger.LogInformation(GetLogMessage("CandidateId: {0}"), candidateId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var candidate = context.Candidates
                    .Where(x => x.Id == candidateId)
                    .ProjectTo<RecruiterCandidateEmail>(ProjectionMapping)
                    .First();

                candidate.Initialize(Settings);

                Send(TemplateTypes.RecruiterCandidateCreated, candidate,
                    $@"[{candidate.RecruiterOrganizationName}] Your candidate was submitted");
            }


           
        }

        private void CandidateCreatedAgencyOwnerEmail(Guid candidateId)
        {
            _logger.LogInformation(GetLogMessage("CandidateId: {0}"), candidateId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var candidate = context.Candidates
                    .Where(x => x.Id == candidateId)
                    .ProjectTo<AgencyOwnerCandidateEmail>(ProjectionMapping)
                    .First();

                candidate.Initialize(Settings);

                Send(TemplateTypes.AgencyOwnerCandidateCreated, candidate,
                    $@"[{candidate.ProviderOrganizationName}] A new candidate is ready for your approval");
            }


        
        }

        private void CandidateCreatedRecruitingAgencyOwnerEmail(Guid candidateId)
        {
            _logger.LogInformation(GetLogMessage("CandidateId: {0}"), candidateId);

            using (var context = new AppDbContext(DbContextOptions))
            {

                var candidate = context.Candidates
                    .Where(x => x.Id == candidateId)
                    .ProjectTo<RecruitingAgencyOwnerCandidateEmail>(ProjectionMapping)
                    .First();

                candidate.Initialize(Settings);

                if (candidate.IsExternal)
                {
                    Send(TemplateTypes.RecruitingAgencyOwnerCandidateCreated, candidate,
                        $@"[{candidate.ProviderOrganizationName} : Staffing] A candidate has been created on behalf of your company");
                }
                else
                {
                    _logger.LogDebug(GetLogMessage("Mail not required"));
                }
            }


        }

        public void Handle(CandidateCreatedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Candidate Created Event Triggered"));
            
            Parallel.Invoke(new ParallelOptions(), new List<Action>
            {
                () => CandidateCreatedAgencyOwnerEmail(evt.CandidateId),
                () => CandidateCreatedRecruitingAgencyOwnerEmail(evt.CandidateId),
                () => CandidateCreatedRecruiterEmail(evt.CandidateId)
            }.ToArray());
        }
    }
}