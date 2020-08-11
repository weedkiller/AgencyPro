// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Common;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Roles.ViewModels.Customers;
using AgencyPro.Core.Services;

namespace AgencyPro.Core.Roles.Services
{
    public interface ICustomerService :
        IService<Customer>,
        IRoleService<CustomerInput, CustomerUpdateInput, CustomerOutput, ICustomer>
    {
        Task<ICustomer> GetPrincipal(Guid customerId);
        Task<PackedList<T>> GetList<T>(
            IOrganizationMarketer ma, CommonFilters filters) where T : MarketerCustomerOutput;

    }
}