// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Services.Ledger
{
    //public class LedgerService : ILedgerService
    //{
    //    private readonly PrimaryContext _primaryContext;

    //    public LedgerService(PrimaryContext primaryContext)
    //    {
    //        _primaryContext = primaryContext;
    //    }

    //    private IQueryable<PaymentDistribution> PaymentDistributions => _primaryContext
    //                .PaymentDistributions
    //                .Include(x => x.Contract);

    //    public async Task<List<PaymentDistribution>> GetDistributions(OrganizationPerson organizationPerson, PaymentDistributionFilters filters)
    //    {

    //    }

    //    public async Task<LedgerBalance> GetLedgerBalance(OrganizationPerson op)
    //    {
    //        var distributions = await PaymentDistributions
    //            .Where(x => x.OrganizationPerson == op)
    //            .ToListAsync();

    //        var lb = new LedgerBalance(distributions
    //            .Where(x => x.DistributionType == DistributionType.Debit)
    //            .Sum(x => Math.Abs(x.Amount)), distributions
    //            .Where(x => x.DistributionType == DistributionType.Credit)
    //            .Sum(x => Math.Abs(x.Amount)));

    //        return lb;
    //    }

    //    public async Task<LedgerBalance> GetLedgerBalance(Person person)
    //    {
    //        var distributions = await PaymentDistributions
    //            .Where(x => x.PersonId == person.Id)
    //            .ToListAsync();

    //        var lb = new LedgerBalance(distributions
    //            .Where(x => x.DistributionType == DistributionType.Debit)
    //            .Sum(x => Math.Abs(x.Amount)), distributions
    //                .Where(x => x.DistributionType == DistributionType.Credit)
    //            .Sum(x => Math.Abs(x.Amount)));

    //        return lb;
    //    }

    //    public async Task<List<PaymentDistribution>> GetDistributions(Person person, PaymentDistributionFilters filters)
    //    {
    //        var distributions = await _primaryContext.PaymentDistributions
    //            .Where(x => x.PersonId == person.Id)
    //            .Where(LinqQueryBuilder.FromFilter(filters))
    //            .ToListAsync();

    //        return distributions;
    //    }
    //}
}