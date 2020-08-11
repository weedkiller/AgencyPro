// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Events;
using AgencyPro.Core.Roles.ViewModels.Customers;

namespace AgencyPro.Core.Roles.Events
{
    public abstract class CustomerEvent<T> : BaseEvent where T : CustomerOutput
    {
        public T Customer { get; set; }
    }
}