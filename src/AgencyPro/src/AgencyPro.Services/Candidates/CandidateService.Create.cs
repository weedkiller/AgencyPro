// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Agreements.Models;
using AgencyPro.Core.Candidates.Enums;
using AgencyPro.Core.Candidates.Events;
using AgencyPro.Core.Candidates.Models;
using AgencyPro.Core.Candidates.ViewModels;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyPro.Services.Candidates
{
    public partial class CandidateService
    {

        private async Task<CandidateResult> CreateCandidate(IOrganizationRecruiter re, Guid providerOrganizationId, CandidateInput input)
        {
            _logger.LogInformation(GetLogMessage("RE: {0}"), re.OrganizationId);
            var retVal = new CandidateResult();

            var isExternal = providerOrganizationId != re.OrganizationId;

            _logger.LogDebug(GetLogMessage("External Lead: {0}"), isExternal);

            var recruiter = await _recruiterRepository.Queryable()
                .Include(x=>x.Recruiter)
                .ThenInclude(x=>x.Person)
                .Where(x => x.RecruiterId == re.RecruiterId && x.OrganizationId == re.OrganizationId)
                .FirstAsync();

            var recruiterBonus = recruiter.RecruiterBonus;
            var recruitingAgencyBonus = 0m;
            var recruitingAgencyStream = 0m;
            var recruiterStream = recruiter.RecruiterStream;


            _logger.LogDebug(
                GetLogMessage(
                    $@"Recruiter Found: {recruiter.Recruiter.Person.DisplayName}"));

            if (isExternal)
            {
                var recruitingAgreement = await _recruitingAgreements.Queryable()
                    .Where(x => x.ProviderOrganizationId == providerOrganizationId &&
                                x.RecruitingOrganizationId == re.OrganizationId)
                    .FirstOrDefaultAsync();


                if (recruitingAgreement == null)
                {
                    retVal.ErrorMessage = "Recruiting agreement doesn't exist between recruiting and provider organization";
                    return retVal;
                }

                if (recruitingAgreement.Status != AgreementStatus.Approved)
                {
                    retVal.ErrorMessage = "Recruiting agreement is not approved";
                    return retVal;
                }

                _logger.LogTrace(
                    GetLogMessage(
                        $@"Recruiting Agreement found to be valid"));


                recruiterBonus = recruitingAgreement.RecruiterBonus;
                recruitingAgencyBonus = recruitingAgreement.RecruitingAgencyBonus;
                recruitingAgencyStream = recruitingAgreement.RecruitingAgencyStream;
                recruiterStream = recruitingAgreement.RecruiterStream;

            }

            var candidate = new Candidate
            {
                Iso2 = input.Iso2,
                ProvinceState = input.ProvinceState,
                RecruiterStream = recruiterStream,
                RecruiterBonus = recruiterBonus,
                RecruitingAgencyStream = recruitingAgencyStream,
                RecruitingAgencyBonus = recruitingAgencyBonus,
                ProviderOrganizationId = providerOrganizationId,
                RecruiterOrganizationId = re.OrganizationId,
                RecruiterId = re.RecruiterId,
                UpdatedById = _userInfo.UserId,
                CreatedById = _userInfo.UserId,
                ObjectState = ObjectState.Added,
                Status = CandidateStatus.New,
                StatusTransitions = new List<CandidateStatusTransition>()
                {
                    new CandidateStatusTransition()
                    {
                        Status = CandidateStatus.New,
                        ObjectState = ObjectState.Added
                    }
                }
            }.InjectFrom(input) as Candidate;

            var candidateResult = Repository.Insert(candidate, true);

            _logger.LogDebug(GetLogMessage("{0} records updated in database."), candidateResult);

            if (candidateResult > 0)
            {
                retVal.Succeeded = true;
                retVal.CandidateId = candidate.Id;

                await Task.Run(() =>
                {
                    RaiseEvent(new CandidateCreatedEvent
                    {
                        CandidateId = candidate.Id
                    });
                });
            }

            return retVal;
        }

        public Task<CandidateResult> CreateInternalCandidate(IOrganizationRecruiter re, CandidateInput model)
        {
            _logger.LogInformation(GetLogMessage("For Email: {email}; with code: {code}"), model.EmailAddress,
                model.ReferralCode);

            return CreateCandidate(re, re.OrganizationId, model);
        }


        public Task<CandidateResult> CreateExternalCandidate(IOrganizationRecruiter re, Guid providerOrganizationId, CandidateInput model)
        {
            _logger.LogInformation(GetLogMessage("For Email: {email}; with code: {code}"), model.EmailAddress,
                model.ReferralCode);

            return CreateCandidate(re, providerOrganizationId, model);

        }

    }
}