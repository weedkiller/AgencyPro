// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Roles.ViewModels.Customers;

namespace AgencyPro.Core.Roles.Events
{
    public class CustomerCreatedEvent<T> : CustomerEvent<T> where T : CustomerOutput
    {
        // marketer
    }
}