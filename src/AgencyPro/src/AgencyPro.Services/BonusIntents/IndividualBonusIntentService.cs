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
using AgencyPro.Core.OrganizationPeople.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.BonusIntents
{
    public class IndividualBonusIntentService : Service<IndividualBonusIntent>, IIndividualBonusIntentService
    {
        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(IndividualBonusIntentService)}.{callerName}] - {message}";
        }
      

        private readonly ILogger<IndividualBonusIntentService> _logger;

        public Task<List<IndividualBonusIntentOutput>> GetBonusIntents(IOrganizationPerson person)
        {
            return Repository.Queryable()
                .Where(x => x.OrganizationId == person.OrganizationId && x.PersonId == person.PersonId)
                .ProjectTo<IndividualBonusIntentOutput>(ProjectionMapping)
                .ToListAsync();
        }

        private async Task<BonusResult> CreateLeadBonus(Guid leadId)
        {
            var retVal = new BonusResult();
            
            var lead = await _leads
                .Queryable()
                .Include(x=>x.IndividualBonusIntent)
                .Where(x => x.Id == leadId && x.IndividualBonusIntent == null)
                .FirstAsync();
            
            Guid? personId = lead.MarketerId;
            Guid? organizationId = lead.MarketerOrganizationId;
            var individualAmount = lead.MarketerBonus;
            var bonusType = BonusType.LeadQualificationBonus;
            
            var intent = new IndividualBonusIntent()
            {
                PersonId = personId.Value,
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
                .Include(x=>x.IndividualBonusIntent)
                .First(x => x.Id == candidateId && x.IndividualBonusIntent == null);

            Guid? personId = candidate.RecruiterId;
            Guid? organizationId = candidate.RecruiterOrganizationId;
            var individualAmount = candidate.RecruiterBonus;
            var bonusType = BonusType.CandidateQualificationBonus;

            var intent = new IndividualBonusIntent()
            {
                PersonId = personId.Value,
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

        public Task<List<IndividualBonusIntentOutput>> GetPending(IOrganizationPerson person, BonusFilters filters)
        {
            return Repository.Queryable()
                .Where(x => x.OrganizationId == person.OrganizationId && x.PersonId == person.PersonId && x.TransferId == null)
                .ProjectTo<IndividualBonusIntentOutput>(ProjectionMapping)
                .ToListAsync();
        }

        private readonly IRepositoryAsync<Lead> _leads;
        private readonly IRepositoryAsync<Candidate> _candidates;

        public IndividualBonusIntentService(
            ILogger<IndividualBonusIntentService> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = logger;

            _leads = UnitOfWork.RepositoryAsync<Lead>();
            _candidates = UnitOfWork.RepositoryAsync<Candidate>();
        }
    }
}
