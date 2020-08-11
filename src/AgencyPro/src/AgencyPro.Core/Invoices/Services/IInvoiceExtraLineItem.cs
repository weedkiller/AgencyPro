// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Invoices.Services
{
    public interface IInvoiceExtraLineItem
    {
        Guid Id { get; set; }
        Guid InvoiceId { get; set; }
        decimal UnitPrice { get; set; }
        decimal Quantity { get; set; }
        string Description { get; set; }
    }
}