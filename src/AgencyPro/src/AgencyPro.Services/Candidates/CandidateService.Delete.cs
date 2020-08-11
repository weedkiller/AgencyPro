// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Candidates.Events;
using AgencyPro.Core.Candidates.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Candidates
{
    public partial class CandidateService
    {

        private async Task<CandidateResult> DeleteCandidate(Guid candidateId)
        {
            _logger.LogInformation(GetLogMessage("Candidate: {0}"), candidateId);

            var candidate = await Repository.FirstOrDefaultAsync(x => x.Id == candidateId);
            candidate.IsDeleted = true;
            var retVal = new CandidateResult()
            {
                CandidateId = candidateId
            };
            var records = await Repository.UpdateAsync(candidate, true);

            if (records > 0)
            {
                retVal.Succeeded = true;
                await Task.Run(() =>
                {
                    RaiseEvent(new CandidateDeletedEvent
                    {
                        CandidateId = candidateId
                    });
                });
            }

            return retVal;
        }

        public async Task<CandidateResult> DeleteCandidate(IProviderAgencyOwner ao, Guid candidateId)
        {
            return await DeleteCandidate(candidateId);
        }

        public async Task<CandidateResult> DeleteCandidate(IOrganizationRecruiter re, Guid candidateId)
        {
            return await DeleteCandidate(candidateId);

        }

        public async Task<CandidateResult> DeleteCandidate(IOrganizationProjectManager pm, Guid candidateId)
        {
            return await DeleteCandidate(candidateId);

        }
    }
}