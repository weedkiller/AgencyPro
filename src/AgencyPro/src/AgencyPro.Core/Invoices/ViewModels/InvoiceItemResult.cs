// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.ViewModels;

namespace AgencyPro.Core.Invoices.ViewModels
{
    public class InvoiceItemResult : BaseResult
    {
        public string InvoiceItemId { get; set; }
        public int TimeEntriesUpdated { get; set; }
        public Guid ContractId { get; set; }
    }
}