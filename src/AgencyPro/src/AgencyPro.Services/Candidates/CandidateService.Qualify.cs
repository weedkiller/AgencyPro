// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.BonusIntents.ViewModels;
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
        public async Task<CandidateResult> QualifyCandidate(IProviderAgencyOwner agencyOwner, Guid candidateId,
            CandidateQualifyInput input)
        {
            _logger.LogInformation(GetLogMessage("AO: {0}, CandidateId: {1}"), agencyOwner.OrganizationId, candidateId);

            var retVal = new CandidateResult()
            {
                CandidateId = candidateId
            };

            var candidate = await Repository
                .Queryable()
                .ForAgencyOwner(agencyOwner)
                .FindById(candidateId)
                .FirstAsync();

            if (candidate.Status != CandidateStatus.Qualified)
            {
                
                candidate.InjectFrom(input);
                candidate.ProjectManagerId = input.ProjectManagerId;
                candidate.ProjectManagerOrganizationId = agencyOwner.OrganizationId;
                candidate.ObjectState = ObjectState.Modified;
                candidate.UpdatedById = _userInfo.UserId;
                candidate.Updated = DateTimeOffset.UtcNow;
                candidate.Status = CandidateStatus.Qualified;


                candidate.StatusTransitions.Add(new CandidateStatusTransition()
                {
                    ObjectState = ObjectState.Added,
                    Status = CandidateStatus.Qualified
                });


                var records = Repository.Update(candidate, true);

                _logger.LogDebug(GetLogMessage("{0} results added"), records);

                if (records > 0)
                {

                    var individualBonusResult = await _individualBonusIntents.Create(new CreateBonusIntentOptions()
                    {
                        CandidateId = candidateId
                    });

                    var organizationBonusResult = await _organizationBonusIntents.Create(new CreateBonusIntentOptions()
                    {
                        CandidateId = candidateId
                    });


                    if (individualBonusResult.Succeeded && organizationBonusResult.Succeeded)
                    {
                        retVal.Succeeded = true;
                        await Task.Run(() =>
                        {
                            RaiseEvent(new CandidateQualifiedEvent
                            {
                                CandidateId = candidateId
                            });
                        });
                    }


                    retVal.Succeeded = true;
                    await Task.Run(() =>
                    {
                        RaiseEvent(new CandidateQualifiedEvent
                        {
                            CandidateId = candidateId
                        });
                    });
                }
               

            }
            else
            {
                _logger.LogDebug(GetLogMessage("Candidate is already qualified"));
            }
            
            return retVal;
        }
    }
}