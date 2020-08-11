// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.PayoutIntents.Models;

namespace AgencyPro.Core.PayoutIntents.Services
{
    public interface IIndividualPayoutIntent
    {
        Guid Id { get; set; }
        Guid PersonId { get; set; }
        Guid OrganizationId { get; set; }
        string InvoiceItemId { get; set; }
        decimal Amount { get; set; }
        CommissionType Type { get; set; }
        string Description { get; set; }
        string InvoiceTransferId { get; set; }
        DateTimeOffset Created { get; set; }
        DateTimeOffset Updated { get; set; }
    }
}