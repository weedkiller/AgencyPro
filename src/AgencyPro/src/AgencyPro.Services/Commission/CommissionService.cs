// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Commission.Services;
using AgencyPro.Core.Commission.ViewModels;
using AgencyPro.Core.OrganizationPeople.Services;
using AgencyPro.Core.TimeEntries.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.BonusIntents.Models;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.OrganizationPeople.Models;
using AgencyPro.Core.PayoutIntents.Models;
using AgencyPro.Core.TimeEntries.Enums;

namespace AgencyPro.Services.Commission
{
    public class CommissionService : Service<TimeEntry>, ICommissionService
    {
        private readonly IRepositoryAsync<OrganizationPayoutIntent> _organizationPayoutIntents;
        private readonly IRepositoryAsync<OrganizationBonusIntent> _organizationBonuses;
        private readonly IRepositoryAsync<OrganizationPerson> _organizationPeople;
       

        public CommissionService(
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _organizationPayoutIntents = UnitOfWork.RepositoryAsync<OrganizationPayoutIntent>();
            _organizationPeople = UnitOfWork.RepositoryAsync<OrganizationPerson>();
            _organizationBonuses = UnitOfWork.RepositoryAsync<OrganizationBonusIntent>();
        }

        public async Task<CommissionOutput> GetCommission(IOrganizationPerson person)
        {
          

            var entries = Repository.Queryable()
                .Include(x=>x.InvoiceItem)
                .Where(x => (x.AccountManagerId == person.PersonId && x.ProviderOrganizationId == person.OrganizationId)
                          || (x.ProjectManagerId == person.PersonId && x.ProviderOrganizationId == person.OrganizationId)
                          || (x.MarketingAgencyOwnerId == person.PersonId && x.MarketingOrganizationId == person.OrganizationId)
                          || (x.RecruitingAgencyOwnerId == person.PersonId && x.RecruitingOrganizationId == person.OrganizationId)
                          || (x.ProviderAgencyOwnerId == person.PersonId && x.ProviderOrganizationId == person.OrganizationId)
                          || (x.ContractorId == person.PersonId && x.ProviderOrganizationId == person.OrganizationId)
                          || (x.RecruiterId == person.PersonId && x.RecruitingOrganizationId == person.OrganizationId)
                          || (x.MarketerId == person.PersonId && x.MarketingOrganizationId == person.OrganizationId))
                .ToListAsync();


            //var individual = await _organizationPeople.Queryable()
            //    .Include(x=>x.)

            var op = await _organizationPeople.Queryable().Where(x =>
                    x.OrganizationId == person.OrganizationId && x.PersonId == person.PersonId)
                .Include(x=>x.BonusIntents)
                .Include(x=>x.Payouts)
                .Include(x=>x.Organization)
                .ThenInclude(x=>x.BonusIntents)
                .FirstOrDefaultAsync();
            


            var individualPayoutIntents = op.Payouts.ToList();
            var individualBonuses = op.BonusIntents.ToList();

            var organizationPayoutIntents = _organizationPayoutIntents.Queryable()
                .Include(x => x.Organization)
                .Where(x => x.OrganizationId == person.OrganizationId && x.Organization.CustomerId == person.PersonId)
                .ToListAsync();

            var organizationBonusIntents = _organizationBonuses.Queryable()
                .Include(x => x.Organization)
                .Where(x => x.OrganizationId == person.OrganizationId && x.Organization.CustomerId == person.PersonId)
                .ToListAsync();

            var stream = GetStreamOutput(person, entries.Result, organizationPayoutIntents.Result,
                individualPayoutIntents);

            var bonus = GetBonusOutput(individualBonuses, organizationBonusIntents.Result);
            
            var output = new CommissionOutput(bonus.Result, stream.Result);
            
            return output;
        }

        private Task<BonusOutput> GetBonusOutput(List<IndividualBonusIntent> individualBonuses, List<OrganizationBonusIntent> organizationBonusees)
        {
            var bonusOutput = new BonusOutput
            {
                MarketerBonus =
                {
                    [TimeStatus.PendingPayout] = individualBonuses.Where(x => x.TransferId == null && x.LeadId.HasValue)
                        .Sum(x => x.Amount),
                    [TimeStatus.Dispersed] = individualBonuses.Where(x => x.TransferId != null && x.LeadId.HasValue)
                        .Sum(x => x.Amount)
                },
                MarketingAgencyBonus =
                {
                    [TimeStatus.PendingPayout] = organizationBonusees
                        .Where(x => x.TransferId == null && x.LeadId.HasValue).Sum(x => x.Amount),
                    [TimeStatus.Dispersed] = organizationBonusees
                        .Where(x => x.TransferId != null && x.LeadId.HasValue).Sum(x => x.Amount),
                },
                RecruiterBonus =
                {
                    [TimeStatus.PendingPayout] = individualBonuses.Where(x => x.TransferId == null && x.CandidateId.HasValue)
                        .Sum(x => x.Amount),
                    [TimeStatus.Dispersed] = individualBonuses
                        .Where(x => x.TransferId != null && x.CandidateId.HasValue).Sum(x => x.Amount)
                },
                RecruitingAgencyBonus =
                {
                    [TimeStatus.PendingPayout] = organizationBonusees
                        .Where(x => x.TransferId == null && x.CandidateId.HasValue).Sum(x => x.Amount),
                    [TimeStatus.Dispersed] = organizationBonusees
                        .Where(x => x.TransferId != null && x.CandidateId.HasValue).Sum(x => x.Amount)
                }
            };
            
            return Task.FromResult(bonusOutput);
        }

        private Task<StreamOutput> GetStreamOutput(
            IOrganizationPerson person, List<TimeEntry> entries, List<OrganizationPayoutIntent> organizationPayoutIntents, List<IndividualPayoutIntent> individualPayouts )
        {
            var streamOutput = new StreamOutput
            {

                ProviderAgencyStream = entries.Where(x =>
                        x.ProviderAgencyOwnerId == person.PersonId && x.ProviderOrganizationId == person.OrganizationId)
                    .GroupBy(x => x.Status).ToDictionary(x => x.Key, x => x.Sum(y => y.TotalAgencyStream)),
                MarketingAgencyStream = entries.Where(x =>
                        x.MarketingAgencyOwnerId == person.PersonId && x.MarketingOrganizationId == person.OrganizationId)
                    .GroupBy(x => x.Status).ToDictionary(x => x.Key, x => x.Sum(y => y.TotalMarketingAgencyStream)),
                RecruitingAgencyStream = entries.Where(x =>
                        x.RecruitingAgencyOwnerId == person.PersonId && x.RecruitingOrganizationId == person.OrganizationId)
                    .GroupBy(x => x.Status).ToDictionary(x => x.Key, x => x.Sum(y => y.TotalRecruitingAgencyStream)),
                MarketerStream = entries.Where(x =>
                        x.MarketerId == person.PersonId && x.MarketingOrganizationId == person.OrganizationId)
                    .GroupBy(x => x.Status).ToDictionary(x => x.Key, x => x.Sum(y => y.TotalMarketerStream)),
                RecruiterStream = entries.Where(x =>
                        x.RecruiterId == person.PersonId && x.RecruitingOrganizationId == person.OrganizationId)
                    .GroupBy(x => x.Status).ToDictionary(x => x.Key, x => x.Sum(y => y.TotalRecruiterStream)),
                ContractorStream = entries.Where(x =>
                        x.ContractorId == person.PersonId && x.ProviderOrganizationId == person.OrganizationId)
                    .GroupBy(x => x.Status).ToDictionary(x => x.Key, x => x.Sum(y => y.TotalContractorStream)),
                ProjectManagerStream = entries.Where(x =>
                        x.ProjectManagerId == person.PersonId && x.ProviderOrganizationId == person.OrganizationId)
                    .GroupBy(x => x.Status).ToDictionary(x => x.Key, x => x.Sum(y => y.TotalProjectManagerStream)),
                AccountManagerStream = entries.Where(x =>
                    x.AccountManagerId == person.PersonId && x.ProviderOrganizationId == person.OrganizationId)
                    .GroupBy(x => x.Status).ToDictionary(x => x.Key, x => x.Sum(y => y.TotalAccountManagerStream))
            };

            streamOutput.ProviderAgencyStream[TimeStatus.Dispersed] = organizationPayoutIntents
                .Where(x => x.Type == CommissionType.ProviderAgencyStream && x.InvoiceTransferId != null)
                .Sum(x => x.Amount);

            streamOutput.ProviderAgencyStream[TimeStatus.PendingPayout] = organizationPayoutIntents
                .Where(x => x.Type == CommissionType.ProviderAgencyStream && x.InvoiceTransferId == null)
                .Sum(x => x.Amount);


            streamOutput.AccountManagerStream[TimeStatus.Dispersed] = individualPayouts
                .Where(x => x.Type == CommissionType.AccountManagerStream && x.InvoiceTransferId != null)
                .Sum(x => x.Amount);

            streamOutput.AccountManagerStream[TimeStatus.PendingPayout] = individualPayouts
                .Where(x => x.Type == CommissionType.AccountManagerStream && x.InvoiceTransferId == null)
                .Sum(x => x.Amount);

            streamOutput.ProjectManagerStream[TimeStatus.Dispersed] = individualPayouts
                .Where(x => x.Type == CommissionType.ProjectManagerStream && x.InvoiceTransferId != null)
                .Sum(x => x.Amount);

            streamOutput.ProjectManagerStream[TimeStatus.PendingPayout] = individualPayouts
                .Where(x => x.Type == CommissionType.ProjectManagerStream && x.InvoiceTransferId == null)
                .Sum(x => x.Amount);

            streamOutput.ContractorStream[TimeStatus.PendingPayout] = individualPayouts
                .Where(x => x.Type == CommissionType.ContractorStream && x.InvoiceTransferId == null)
                .Sum(x => x.Amount);

            streamOutput.ContractorStream[TimeStatus.Dispersed] = individualPayouts
                .Where(x => x.Type == CommissionType.ContractorStream && x.InvoiceTransferId != null)
                .Sum(x => x.Amount);



            streamOutput.RecruitingAgencyStream[TimeStatus.Dispersed] = organizationPayoutIntents
                .Where(x => x.Type == CommissionType.RecruitingAgencyStream && x.InvoiceTransferId != null)
                .Sum(x => x.Amount);

            streamOutput.RecruitingAgencyStream[TimeStatus.PendingPayout] = organizationPayoutIntents
                .Where(x => x.Type == CommissionType.RecruitingAgencyStream && x.InvoiceTransferId == null)
                .Sum(x => x.Amount);


            streamOutput.RecruiterStream[TimeStatus.Dispersed] = individualPayouts
                .Where(x => x.Type == CommissionType.RecruiterStream && x.InvoiceTransferId != null)
                .Sum(x => x.Amount);

            streamOutput.RecruiterStream[TimeStatus.PendingPayout] = individualPayouts
                .Where(x => x.Type == CommissionType.RecruiterStream && x.InvoiceTransferId == null)
                .Sum(x => x.Amount);


            streamOutput.MarketingAgencyStream[TimeStatus.Dispersed] = organizationPayoutIntents
                .Where(x => x.Type == CommissionType.MarketingAgencyStream && x.InvoiceTransferId != null)
                .Sum(x => x.Amount);

            streamOutput.MarketingAgencyStream[TimeStatus.PendingPayout] = organizationPayoutIntents
                .Where(x => x.Type == CommissionType.MarketingAgencyStream && x.InvoiceTransferId == null)
                .Sum(x => x.Amount);

            streamOutput.MarketerStream[TimeStatus.Dispersed] = individualPayouts
                .Where(x => x.Type == CommissionType.MarketerStream && x.InvoiceTransferId != null)
                .Sum(x => x.Amount);

            streamOutput.MarketerStream[TimeStatus.PendingPayout] = individualPayouts
                .Where(x => x.Type == CommissionType.MarketerStream && x.InvoiceTransferId == null)
                .Sum(x => x.Amount);

            return Task.FromResult(streamOutput);
        }
    }
}
