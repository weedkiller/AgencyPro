// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.CustomerAccounts.Filters
{
    public class CustomerAccountFilters
    {
        public static readonly CustomerAccountFilters NoFilter = new CustomerAccountFilters();

        public Guid? CustomerId { get; set; }
        public Guid? CustomerOrganizationId { get; set; }
        public Guid? AccountManagerId { get; set; }
        public Guid? AccountManagerOrganizationId { get; set; }
        public int? Number { get; set; }
    }
}