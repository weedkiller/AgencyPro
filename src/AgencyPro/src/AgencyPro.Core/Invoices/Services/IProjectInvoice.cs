// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Stripe.Services;

namespace AgencyPro.Core.Invoices.Services
{
    public interface IProjectInvoice : IInvoice
    {
        Guid ProjectId { get; set; }
     
        DateTimeOffset Created { get; set; }
        DateTimeOffset Updated { get; set; }
        string RefNo { get; set; }
    }
}