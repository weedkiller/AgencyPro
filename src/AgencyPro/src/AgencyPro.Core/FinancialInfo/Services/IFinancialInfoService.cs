// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;
using AgencyPro.Core.FinancialInfo.ViewModels;
using AgencyPro.Core.OrganizationPeople.Services;
using AgencyPro.Core.OrganizationRoles.Services;

namespace AgencyPro.Core.FinancialInfo.Services
{
    public interface IFinancialInfoService
    {
        Task<BuyerFinancialInfo> GetBuyerFinancialInfo(IOrganizationCustomer customer);
        Task<ProviderFinancialInfo> GetProviderFinancialInfo(IOrganizationPerson person);
    }
}
