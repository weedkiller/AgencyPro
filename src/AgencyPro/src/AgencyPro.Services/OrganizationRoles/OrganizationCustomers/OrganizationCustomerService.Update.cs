// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationCustomers;

namespace AgencyPro.Services.OrganizationRoles.OrganizationCustomers
{
    public partial class OrganizationCustomerService
    {
        public Task<T> Update<T>(IAgencyOwner ao, OrganizationCustomerInput input) where T : OrganizationCustomerOutput
        {
            throw new NotImplementedException();
        }

        public Task<T> Update<T>(IOrganizationAccountManager am, OrganizationCustomerInput input) where T : OrganizationCustomerOutput
        {
            throw new NotImplementedException();
        }

        public Task<T> Update<T>(OrganizationCustomerInput model) where T : OrganizationCustomerOutput
        {
            throw new NotImplementedException();
        }
    }
}