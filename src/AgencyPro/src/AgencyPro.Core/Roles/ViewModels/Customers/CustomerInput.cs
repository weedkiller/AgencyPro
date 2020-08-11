// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AgencyPro.Core.Roles.ViewModels.Customers
{
    public class CustomerInput : CustomerUpdateInput
    {
        [BindRequired] public virtual Guid PersonId { get; set; }

        [BindRequired] public virtual Guid MarketerId { get; set; }

        [BindRequired] public virtual Guid MarketerOrganizationId { get; set; }
    }
}