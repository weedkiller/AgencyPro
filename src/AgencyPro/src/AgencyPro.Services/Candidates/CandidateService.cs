// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Agreements.Models;
using AgencyPro.Core.BonusIntents.Services;
using AgencyPro.Core.Candidates.Enums;
using AgencyPro.Core.Candidates.Extensions;
using AgencyPro.Core.Candidates.Models;
using AgencyPro.Core.Candidates.Services;
using AgencyPro.Core.Candidates.ViewModels;
using AgencyPro.Core.Common;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.Extensions;
using AgencyPro.Core.OrganizationPeople.Services;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.UserAccount.Models;
using AgencyPro.Core.UserAccount.Services;
using AgencyPro.Services.Candidates.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Candidates
{
    public partial class CandidateService : Service<Candidate>, ICandidateService
    {
        private readonly ILogger<CandidateService> _logger;
        private readonly IUserInfo _userInfo;
        private readonly IRepositoryAsync<RecruitingAgreement> _recruitingAgreements;
        private readonly IOrganizationPersonService _organizationPersonService;
        private readonly IIndividualBonusIntentService _individualBonusIntents;
        private readonly IOrganizationBonusIntentService _organizationBonusIntents;
        private readonly IRepositoryAsync<OrganizationRecruiter> _recruiterRepository;
        private readonly IRepositoryAsync<ApplicationUser> _applicationUsers;

        public CandidateService(
            IRepositoryAsync<RecruitingAgreement> recruitingAgreements,
            IOrganizationPersonService organizationPersonService,
            IIndividualBonusIntentService individualBonusIntents,
            IOrganizationBonusIntentService organizationBonusIntents,
            ILogger<CandidateService> logger,
            IUserInfo userInfo,
            MultiCandidateEventsHandler events,
            IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            _recruitingAgreements = recruitingAgreements;
            _organizationPersonService = organizationPersonService;
            _individualBonusIntents = individualBonusIntents;
            _organizationBonusIntents = organizationBonusIntents;
            _recruiterRepository = UnitOfWork.RepositoryAsync<OrganizationRecruiter>();
            _applicationUsers = UnitOfWork.RepositoryAsync<ApplicationUser>();
            _logger = logger;
            _userInfo = userInfo;

            AddEventHandler(events);
        }

        public Task<PackedList<T>> GetActiveCandidates<T>(IProviderAgencyOwner agencyOwner, CommonFilters filters)
            where T : AgencyOwnerCandidateOutput
        {
            return Repository.Queryable()
                .ForAgencyOwner(agencyOwner)
                .Where(x => x.Status == CandidateStatus.New)
                .OrderByDescending(x => x.Updated)
                .PaginateProjection<Candidate, T>(filters, ProjectionMapping);
        }

        public Task<PackedList<T>> GetCandidates<T>(IOrganizationRecruiter organizationRecruiter, CommonFilters filters)
            where T : RecruiterCandidateOutput
        {
            return Repository.Queryable()
                .ForOrganizationRecruiter(organizationRecruiter)
                .OrderByDescending(x => x.Updated)
                .PaginateProjection<Candidate, T>(filters, ProjectionMapping);
        }

        public Task<T> GetCandidate<T>(IOrganizationRecruiter organizationRecruiter, Guid candidateId)
            where T : RecruiterCandidateOutput
        {
            return Repository.Queryable()
                .ForOrganizationRecruiter(organizationRecruiter)
                .FindById(candidateId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetCandidate<T>(IOrganizationProjectManager pm, Guid candidateId)
            where T : ProjectManagerCandidateOutput
        {
            return Repository.Queryable()
                .ForOrganizationProjectManager(pm)
                .FindById(candidateId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetCandidate<T>(IProviderAgencyOwner ao, Guid candidateId)
            where T : AgencyOwnerCandidateOutput
        {
            return Repository.Queryable()
                .ForAgencyOwner(ao)
                .FindById(candidateId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }


        public Task<PackedList<T>> GetCandidates<T>(IOrganizationProjectManager organizationProjectManager, CommonFilters filters)
            where T : ProjectManagerCandidateOutput
        {
            return Repository.Queryable()
                .ForOrganizationProjectManager(organizationProjectManager)
                .Where(x => x.Status == CandidateStatus.Qualified)
                .OrderByDescending(x => x.Updated)
                .PaginateProjection<Candidate, T>(filters, ProjectionMapping);
        }

        public Task<T> GetCandidate<T>(Guid id)
            where T : CandidateOutput
        {
            return Repository.Queryable()
                .FindById(id)
                .ProjectTo<T>(ProjectionMapping)
                .FirstAsync();
        }


        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[CandidateService.{callerName}] - {message}";
        }
    }
}