// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core;
using AgencyPro.Core.Candidates.Events;
using AgencyPro.Core.Candidates.Extensions;
using AgencyPro.Core.Candidates.Models;
using AgencyPro.Core.Candidates.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace AgencyPro.Services.Candidates
{
    public partial class CandidateService
    {
        public async Task<CandidateResult> UpdateCandidate(IOrganizationRecruiter re, Guid candidateId, CandidateInput model)
        {
            var entity = await Repository.Queryable()
                .ForOrganizationRecruiter(re)
                .GetById(candidateId).FirstOrDefaultAsync();

            if (entity == null) return null;

            entity.InjectFrom(model);

            return await UpdateCandidate(entity);
        }

        public async Task<CandidateResult> UpdateCandidate(IProviderAgencyOwner ao, Guid candidateId, CandidateInput model)
        {
            var entity = await Repository.Queryable().ForAgencyOwner(ao)
                .FirstOrDefaultAsync(n => n.Id == candidateId);
            if (entity == null) return null;

            entity.InjectFrom(model);

            return await UpdateCandidate(entity);
        }

        public async Task<CandidateResult> UpdateCandidate(IOrganizationProjectManager pm, Guid candidateId, CandidateInput model)
        {
            var entity = await Repository.Queryable().ForOrganizationProjectManager(pm)
                .FirstOrDefaultAsync(n => n.Id == candidateId);
            if (entity == null) return null;

            entity.InjectFrom(model);

            return await UpdateCandidate(entity);
        }

        private async Task<CandidateResult> UpdateCandidate([NotNull] Candidate candidate)
        {
            _logger.LogInformation(GetLogMessage($@"Updating Candidate: {candidate.Id}"));

            var retVal = new CandidateResult
            {
                CandidateId = candidate.Id
            };

            var records = await Repository.UpdateAsync(candidate, true);
            if (records > 0)
            {
                retVal.Succeeded = true;
                await Task.Run(() =>
                {
                    RaiseEvent(new CandidateUpdatedEvent
                    {
                        CandidateId = candidate.Id
                    });
                });
            }
           

            return retVal;
        }
    }
}