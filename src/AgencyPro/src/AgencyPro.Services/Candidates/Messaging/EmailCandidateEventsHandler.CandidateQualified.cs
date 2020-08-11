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

        private void CandidateQualifiedRecruiterEmail(Guid candidateId)
        {
            _logger.LogInformation(GetLogMessage("CandidateId: {0}"), candidateId);

            using (var context = new AppDbContext(DbContextOptions))
            {

                var candidate = context.Candidates
                    .Where(x => x.Id == candidateId)
                    .ProjectTo<RecruiterCandidateEmail>(ProjectionMapping)
                    .First();

                candidate.Initialize(Settings);

                Send(TemplateTypes.RecruiterCandidateQualified, candidate,
                    $@"[{candidate.RecruiterOrganizationName} : Recruiting] Your candidate has been qualified.");
            }


        }

        private void CandidateQualifiedRecruitingAgencyEmail(Guid candidateId)
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

                    Send(TemplateTypes.RecruitingAgencyOwnerCandidateQualified, candidate,
                        $@"[{candidate.RecruiterOrganizationName}] Your candidate has been qualified.");
                }
                else
                {
                    _logger.LogDebug(GetLogMessage("Mail not required"));
                }
            }

           
        }

        private void CandidateQualifiedProjectManagerEmail(Guid candidateId)
        {
            _logger.LogInformation(GetLogMessage("CandidateId: {0}"), candidateId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var candidate = context.Candidates
                    .Where(x => x.Id == candidateId)
                    .ProjectTo<ProjectManagerCandidateEmail>(ProjectionMapping)
                    .First();

                if (candidate.IsExternal)
                {

                    candidate.Initialize(Settings);

                    Send(TemplateTypes.ProjectManagerCandidateQualified, candidate,
                        $@"[{candidate.RecruiterOrganizationName}] Candidate has been assigned to you");
                }
                else
                {
                    _logger.LogDebug(GetLogMessage("Mail not required"));
                }
            }

           
        }


        public void Handle(CandidateQualifiedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Candidate Qualified Event Triggered"));


           var tasks = new List<Action>
           {
               () => CandidateQualifiedRecruiterEmail(evt.CandidateId),
               () => CandidateQualifiedRecruitingAgencyEmail(evt.CandidateId),
               () => CandidateQualifiedProjectManagerEmail(evt.CandidateId)
           };

           Parallel.Invoke(new ParallelOptions(), tasks.ToArray());

        }
    }
}