// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Roles.Services
{
    public interface ICustomer
    {
        Guid Id { get; set; }
        Guid MarketerId { get; set; }
        Guid MarketerOrganizationId { get; set; }
        DateTimeOffset Created { get; set; }
        DateTimeOffset Updated { get; set; }
    }
}