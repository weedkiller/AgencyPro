// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Invoices.Services;

namespace AgencyPro.Core.Invoices.Models
{
    public class InvoiceMatrix : IInvoiceMatrix
    {
        public Guid CustomerId { get; set; }
        public Guid CustomerOrganizationId { get; set; }

        public Guid AccountManagerId { get; set; }
        public Guid AccountManagerOrganizationId { get; set; }

        private bool IsOverdue { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal AmountPaid { get; set; }
    }
}