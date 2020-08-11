// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Candidates.Emails;
using AgencyPro.Core.Candidates.Events;
using AgencyPro.Core.EmailSending.Services;
using AgencyPro.Core.Templates.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Data.EFCore;

namespace AgencyPro.Services.Candidates.Messaging
{
    public partial class MultiCandidateEventsHandler
    {
        private void CandidatePromotedRecruiterEmail(Guid candidateId)
        {
            _logger.LogInformation(GetLogMessage("CandidateId: {0}"), candidateId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var candidate = context.Candidates
                    .Where(x => x.Id == candidateId)
                    .ProjectTo<RecruiterCandidateEmail>(ProjectionMapping)
                    .First();

                candidate.Initialize(Settings);

                Send(TemplateTypes.RecruiterCandidatePromoted, candidate,
                    $@"[{candidate.RecruiterOrganizationName} : Recruiting] Your candidate has been promoted.");
            }

          
        }

        private void CandidatePromotedAgencyOwnerEmail(Guid candidateId)
        {
            _logger.LogInformation(GetLogMessage("CandidateId: {0}"), candidateId);


            using (var context = new AppDbContext(DbContextOptions))
            {
                var candidate = context.Candidates
                    .Where(x => x.Id == candidateId)
                    .ProjectTo<AgencyOwnerCandidateEmail>(ProjectionMapping)
                    .First();

                candidate.Initialize(Settings);

                Send(TemplateTypes.AgencyOwnerCandidatePromoted, candidate,
                    $@"[{candidate.RecruiterOrganizationName}] A candidate in your organization has been promoted.");
            }

           
        }

        private void CandidatePromotedRecruitingAgencyOwnerEmail(Guid candidateId)
        {
            _logger.LogInformation(GetLogMessage("CandidateId: {0}"), candidateId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var candidate = context.Candidates
                    .Where(x => x.Id == candidateId)
                    .ProjectTo<RecruitingAgencyOwnerCandidateEmail>(ProjectionMapping)
                    .First();

                if (candidate.IsExternal)
                {
                    candidate.Initialize(Settings);

                    Send(TemplateTypes.RecruitingAgencyOwnerCandidatePromoted, candidate,
                        $@"[{candidate.RecruiterOrganizationName}] Your candidate has been promoted.");
                }
                else
                {
                    _logger.LogDebug(GetLogMessage("Mail not required"));
                }
            }

            

        }

        public void Handle(CandidatePromotedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Candidate Promoted Event Triggered"));
            
            var tasks = new List<Action>
            {
                () => CandidatePromotedRecruiterEmail(evt.CandidateId),
                () => CandidatePromotedRecruitingAgencyOwnerEmail(evt.CandidateId),
                () => CandidatePromotedAgencyOwnerEmail(evt.CandidateId)
            };

            Parallel.Invoke(new ParallelOptions(), tasks.ToArray());
            
        }
    }
}