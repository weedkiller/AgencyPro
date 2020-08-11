// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Orders.Model;
using System;

namespace AgencyPro.Core.Orders.Services
{
    public interface IWorkOrder
    {
        Guid Id { get; set; }

        int BuyerNumber { get; set; }
        int ProviderNumber { get; set; }

        string Description { get; set; }
        OrderStatus Status { get; set; }

        Guid AccountManagerId { get; set; }
        Guid AccountManagerOrganizationId { get; set; }

        Guid CustomerId { get; set; }
        Guid CustomerOrganizationId { get; set; }
        DateTimeOffset? ProviderResponseTime { get; set; }
    }
}