// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Events;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationCustomers;

namespace AgencyPro.Core.OrganizationRoles.Events.OrganizationCustomers
{
    public abstract class OrganizationCustomerEvent : BaseEvent
    {
        public OrganizationCustomerOutput OrganizationCustomer { get; set; }
    }
}