// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationCustomers
{
    public class OrganizationCustomerInput
    {
        public virtual Guid CustomerId { get; set; }
        public virtual Guid OrganizationId { get; set; }
    }
}