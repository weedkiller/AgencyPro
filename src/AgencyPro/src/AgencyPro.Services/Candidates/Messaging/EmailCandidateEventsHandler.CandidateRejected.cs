// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Candidates.Emails;
using AgencyPro.Core.Candidates.Events;
using AgencyPro.Core.EmailSending.Services;
using AgencyPro.Core.Templates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Data.EFCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Candidates.Messaging
{
    public partial class MultiCandidateEventsHandler
    {
        private void CandidateRejectedRecruitingAgencyEmail(Guid candidateId)
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
                    Send(TemplateTypes.RecruitingAgencyOwnerCandidateRejected, candidate,
                        $@"[{candidate.RecruiterOrganizationName}] A candidate was rejected");
                }
                else
                {
                    _logger.LogDebug(GetLogMessage("Mail not required"));
                }
            }


        }

        private void CandidateRejectedRecruiterEmail(Guid candidateId)
        {
            _logger.LogInformation(GetLogMessage("CandidateId: {0}"), candidateId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var candidate = context.Candidates
                    .Where(x => x.Id == candidateId)
                    .ProjectTo<RecruiterCandidateEmail>(ProjectionMapping)
                    .First();

                candidate.Initialize(Settings);

                Send(TemplateTypes.RecruiterCandidateRejected, candidate,
                    $@"[{candidate.RecruiterOrganizationName}] A candidate was rejected");
            }

         
        }

        private void CandidateRejectedAgencyOwnerEmail(Guid candidateId)
        {
            _logger.LogInformation(GetLogMessage("CandidateId: {0}"), candidateId);


            using (var context = new AppDbContext(DbContextOptions))
            {
                var candidate = context.Candidates
                    .Where(x => x.Id == candidateId)
                    .ProjectTo<AgencyOwnerCandidateEmail>(ProjectionMapping)
                    .First();

                candidate.Initialize(Settings);

                Send(TemplateTypes.AgencyOwnerCandidateRejected, candidate,
                    $@"[{candidate.ProviderOrganizationName}] A candidate was rejected");
            }
            
        }




        public void Handle(CandidateRejectedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Candidate Rejected Event Triggered"));

            Parallel.Invoke(new List<Action>
            {
                () => CandidateRejectedRecruitingAgencyEmail(evt.CandidateId),
                () => CandidateRejectedRecruiterEmail(evt.CandidateId),
                () => CandidateRejectedAgencyOwnerEmail(evt.CandidateId)
            }.ToArray());
        }
    }
}