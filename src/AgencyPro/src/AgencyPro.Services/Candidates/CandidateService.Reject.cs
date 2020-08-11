// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core;
using AgencyPro.Core.Candidates.Enums;
using AgencyPro.Core.Candidates.Events;
using AgencyPro.Core.Candidates.Extensions;
using AgencyPro.Core.Candidates.Models;
using AgencyPro.Core.Candidates.ViewModels;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace AgencyPro.Services.Candidates
{
    public partial class CandidateService
    {
        public async Task<CandidateResult> RejectCandidate(IOrganizationProjectManager pm, Guid candidateId,
            CandidateRejectionInput input)
        {
            var entity = await Repository.Queryable()
                .ForOrganizationProjectManager(pm)
                .FindById(candidateId)
                .FirstOrDefaultAsync();

            if (entity == null) return null;

            entity.InjectFrom(input);

            return await RejectCandidate(entity);
        }

        public async Task<CandidateResult> RejectCandidate(IProviderAgencyOwner agencyOwner, Guid candidateId,
            CandidateRejectionInput input)
        {
            var entity = await Repository.Queryable()
                .ForAgencyOwner(agencyOwner)
                .FindById(candidateId)
                .FirstOrDefaultAsync();

            if (entity == null) return null;

            entity.InjectFrom(input);

            return await RejectCandidate(entity);
        }

        private async Task<CandidateResult> RejectCandidate([NotNull] Candidate entity)
        {
            _logger.LogInformation(GetLogMessage($@"Rejecting Candidate: {entity.Id}"));
            
            var retVal = new CandidateResult();

            entity.ObjectState = ObjectState.Modified;
            entity.Status = CandidateStatus.Rejected;

            entity.StatusTransitions.Add(new CandidateStatusTransition()
            {
                ObjectState = ObjectState.Added,
                Status = CandidateStatus.Rejected
            });


            var leadResult = Repository.InsertOrUpdateGraph(entity, true);
            _logger.LogDebug(GetLogMessage("{0} results added"), leadResult);

            if (leadResult > 0)
            {
                retVal.Succeeded = true;
                await Task.Run(() =>
                {
                    RaiseEvent(new CandidateRejectedEvent
                    {
                        CandidateId = entity.Id
                    });
                });

            }

            return retVal;
        }
    }
}