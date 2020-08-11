// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Roles.ViewModels.Customers;

namespace AgencyPro.Services.Roles.Customers
{
    public partial class CustomerService
    {
        public Task<T> Update<T>(ICustomer principal, CustomerUpdateInput model) where T : CustomerOutput
        {
            throw new NotImplementedException();
        }
    }
}