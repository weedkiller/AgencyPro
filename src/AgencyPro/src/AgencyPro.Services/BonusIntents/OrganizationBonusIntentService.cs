// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.BonusIntents.Models;
using AgencyPro.Core.BonusIntents.Services;
using AgencyPro.Core.BonusIntents.ViewModels;
using AgencyPro.Core.Candidates.Models;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.Leads.Models;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.BonusIntents
{
    public class OrganizationBonusIntentService : Service<OrganizationBonusIntent>, IOrganizationBonusIntentService
    {
        private readonly ILogger<OrganizationBonusIntentService> _logger;

        public Task<List<OrganizationBonusIntentOutput>> GetBonusIntents(IAgencyOwner person)
        {
            return Repository
                .Queryable()
                .Include(x => x.Organization)
                .Where(x => x.OrganizationId == person.OrganizationId && x.Organization.CustomerId == person.CustomerId)
                .ProjectTo<OrganizationBonusIntentOutput>(ProjectionMapping)
                .ToListAsync();
        }


        private async Task<BonusResult> CreateLeadBonus(Guid leadId)
        {
            var retVal = new BonusResult();

            var lead = await _leads
                .Queryable()
                .Include(x => x.OrganizationBonusIntent)
                .Where(x => x.Id == leadId && x.OrganizationBonusIntent == null)
                .FirstAsync();

            Guid? organizationId = lead.MarketerOrganizationId;
            var individualAmount = lead.MarketingAgencyBonus;
            var bonusType = BonusType.LeadQualificationBonus;

            var intent = new OrganizationBonusIntent()
            {
                OrganizationId = organizationId.Value,
                LeadId = leadId,
                Amount = individualAmount,
                BonusType = bonusType,
                ObjectState = ObjectState.Added,
            };

            var records = await Repository.InsertAsync(intent, true);

            _logger.LogDebug(GetLogMessage("{0} records updated"), records);

            if (records > 0)
            {
                retVal.BonusId = intent.Id;
                retVal.Succeeded = true;
            }

            return await Task.FromResult(retVal);

        }

        private async Task<BonusResult> CreateCandidateBonus(Guid candidateId)
        {
            var retVal = new BonusResult();

            var candidate = _candidates
                .Queryable()
                .Include(x => x.OrganizationBonusIntent)
                .First(x => x.Id == candidateId && x.OrganizationBonusIntent == null);

            Guid? organizationId = candidate.RecruiterOrganizationId;
            var individualAmount = candidate.RecruitingAgencyBonus;
            var bonusType = BonusType.CandidateQualificationBonus;

            var intent = new OrganizationBonusIntent()
            {
                OrganizationId = organizationId.Value,
                CandidateId = candidateId,
                Amount = individualAmount,
                BonusType = bonusType,
                ObjectState = ObjectState.Added,
            };

            var records = await Repository.InsertAsync(intent, true);

            _logger.LogDebug(GetLogMessage("{0} records updated"), records);

            if (records > 0)
            {
                retVal.BonusId = intent.Id;
                retVal.Succeeded = true;

            }

            return await Task.FromResult(retVal);
        }


        public async Task<BonusResult> Create(CreateBonusIntentOptions options)
        {
            _logger.LogInformation(GetLogMessage("Creating Bonus: {@options}"), options);

            BonusResult retVal = new BonusResult();

            if (options.CandidateId.HasValue)
            {
                return await CreateCandidateBonus(options.CandidateId.Value);
            }

            if (options.LeadId.HasValue)
            {
                return await CreateLeadBonus(options.LeadId.Value);
            }

            retVal.ErrorMessage = "LeadID or MarketerId must be specified";
            return retVal;
        }

        private readonly IRepositoryAsync<Lead> _leads;
        private readonly IRepositoryAsync<Candidate> _candidates;
        public OrganizationBonusIntentService(
            ILogger<OrganizationBonusIntentService> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = logger;

            _leads = UnitOfWork.RepositoryAsync<Lead>();
            _candidates = UnitOfWork.RepositoryAsync<Candidate>();
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(OrganizationBonusIntentService)}.{callerName}] - {message}";
        }


    }
}