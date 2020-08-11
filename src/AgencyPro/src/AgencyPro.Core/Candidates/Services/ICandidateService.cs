// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Candidates.Models;
using AgencyPro.Core.Candidates.ViewModels;
using AgencyPro.Core.Common;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Services;

namespace AgencyPro.Core.Candidates.Services
{
    public interface ICandidateService : IService<Candidate>
    {
        Task<CandidateResult> CreateInternalCandidate(IOrganizationRecruiter re, CandidateInput input);

        Task<CandidateResult> CreateExternalCandidate(IOrganizationRecruiter re, Guid providerOrganizationId,
            CandidateInput model);

        Task<PackedList<T>> GetActiveCandidates<T>(IProviderAgencyOwner ao, CommonFilters filters)
            where T : AgencyOwnerCandidateOutput;

        Task<PackedList<T>> GetCandidates<T>(IOrganizationRecruiter re, CommonFilters filters)
            where T : RecruiterCandidateOutput;

        Task<PackedList<T>> GetCandidates<T>(IOrganizationProjectManager pm, CommonFilters filters)
            where T : ProjectManagerCandidateOutput;

        Task<T> GetCandidate<T>(Guid id) where T : CandidateOutput;

        Task<CandidateResult> RejectCandidate(IProviderAgencyOwner ao, Guid candidateId,
            CandidateRejectionInput input);

        Task<CandidateResult> RejectCandidate(IOrganizationProjectManager pm, Guid candidateId, CandidateRejectionInput input);
        
        Task<T> GetCandidate<T>(IOrganizationRecruiter re, Guid candidateId)
            where T : RecruiterCandidateOutput;

        Task<T> GetCandidate<T>(IOrganizationProjectManager pm, Guid candidateId)
            where T : ProjectManagerCandidateOutput;

        Task<T> GetCandidate<T>(IProviderAgencyOwner cu, Guid candidateId)
            where T : AgencyOwnerCandidateOutput;

        Task<CandidateResult> UpdateCandidate(IOrganizationRecruiter re, Guid candidateId, CandidateInput model);
        Task<CandidateResult> UpdateCandidate(IProviderAgencyOwner ao, Guid candidateId, CandidateInput model);
        Task<CandidateResult> UpdateCandidate(IOrganizationProjectManager co, Guid candidateId, CandidateInput model);
        Task<CandidateResult> QualifyCandidate(IProviderAgencyOwner ao, Guid candidateId, CandidateQualifyInput input);

        Task<CandidateResult> DeleteCandidate(IProviderAgencyOwner ao, Guid candidateId);
        Task<CandidateResult> DeleteCandidate(IOrganizationRecruiter re, Guid candidateId);
        Task<CandidateResult> DeleteCandidate(IOrganizationProjectManager pm, Guid candidateId);

        Task<CandidatePromotionResult> Promote(
            IOrganizationProjectManager pm,
            Guid candidateId,
            CandidateAcceptInput model);


    }
}