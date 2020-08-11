// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.Invoices.Enums
{
    public enum InvoiceStatus : byte
    {
        Draft = 0,
        Sent = 1,
        Paid = 2,
        PartialPayment = 3,
        Deleted = 4
    }
}