// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;
using AgencyPro.Core.CustomerBalance.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;

namespace AgencyPro.Core.CustomerBalance.Services
{
    public interface ICustomerBalanceService
    {
        Task<CustomerBalanceOutput> GetBalance(IOrganizationCustomer customer);
    }
}
