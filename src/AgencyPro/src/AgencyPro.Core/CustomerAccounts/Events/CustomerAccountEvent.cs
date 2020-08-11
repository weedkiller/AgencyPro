// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Events;

namespace AgencyPro.Core.CustomerAccounts.Events
{
    public abstract class CustomerAccountEvent : BaseEvent
    {
       public Guid CustomerOrganizationId { get; set; }
       public Guid AccountManagerOrganizationId { get; set; }
       public Guid AccountManagerId { get; set; }
       public Guid CustomerId { get; set; }
    }
}