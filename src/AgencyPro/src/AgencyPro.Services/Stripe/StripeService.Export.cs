// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Services.Stripe
{
    public partial class StripeService
    {
        

        //public void ConfigureCompanyAccounts()
        //{
        //    var customers = _organiationCustomerRepository.Queryable()
        //        .ToList();

        //    foreach (var organization in customers)
        //    {
        //        SyncCustomer(organization.OrganizationId, organization.CustomerId);
        //    }
        //}

        //public async Task<int> ExportCustomers(Guid organizationId)
        //{
        //    _logger.LogInformation(GetLogMessage("Exporting Customers for Org: {0}"), organizationId);

        //    var accounts = _customerAccountRepository.Queryable()
        //        .Include(x => x.ProviderOrganization)
        //        .Include(x => x.BuyerOrganization)
        //        .ThenInclude(x => x.OrganizationBuyerAccount)
        //        .Where(x => x.BuyerOrganization.OrganizationBuyerAccount == null && x.ProviderOrganization.Id == organizationId)
        //        .ToList();

        //    _logger.LogInformation("Customers to export: " + accounts.Count);


        //    var totals = 0;
        //    foreach (var account in accounts)
        //    {
        //        var result = await PushCustomer(account.CustomerOrganizationId, account.CustomerId);
        //        totals += result;
        //    }

        //    return totals;
        //}

        //public async Task<int> ExportCustomers()
        //{
        //    var accounts = _customerAccountRepository.Queryable()
        //        .Include(x => x.BuyerOrganization)
        //        .ThenInclude(x => x.OrganizationBuyerAccount)
        //        .Where(x => x.BuyerOrganization.OrganizationBuyerAccount == null)
        //        .ToList();

        //    var totals = 0;
        //    foreach (var account in accounts)
        //    {
        //        var result = await PushCustomer(account.CustomerOrganizationId, account.CustomerId);
        //        totals += result;
        //    }

        //    return totals;
        //}

       
    }
}