// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;

namespace AgencyPro.Core.Invoices.ViewModels
{
    public class ContractorProjectInvoiceDetailsOutput : ContractorProjectInvoiceOutput
    {
        public ICollection<InvoiceLineOutput> Lines { get; set; }
    }
}