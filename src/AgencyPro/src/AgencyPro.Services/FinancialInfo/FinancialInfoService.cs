// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.FinancialInfo.Services;
using AgencyPro.Core.FinancialInfo.ViewModels;
using AgencyPro.Core.OrganizationPeople.Models;
using AgencyPro.Core.OrganizationPeople.Services;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.TimeEntries.Services;

namespace AgencyPro.Services.FinancialInfo
{
    public class FinancialInfoService : Service<OrganizationPerson>, IFinancialInfoService
    {
        private readonly ITimeMatrixService _timeMatrixService;

        public Task<BuyerFinancialInfo> GetBuyerFinancialInfo(
            IOrganizationCustomer customer)
        {
            throw new NotImplementedException();
        }

        public Task<ProviderFinancialInfo> GetProviderFinancialInfo(IOrganizationPerson person)
        {
            //var matrix = await _timeMatrixService.GetResults<>()
            throw new NotImplementedException();

        }

        public FinancialInfoService(
            IServiceProvider serviceProvider, 
            ITimeMatrixService timeMatrixService
        ) : base(serviceProvider)
        {
            _timeMatrixService = timeMatrixService;
            throw new NotImplementedException();
        }
    }
}
