// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Invoices.ViewModels
{
    public class InvoiceInput
    {
        public virtual Guid ProjectId { get; set; }
        
        public virtual Guid[] ContractIds { get; set; }

        public virtual bool IncludeAllContracts { get; set; }
        
        public string RefNo { get; set; }
    }
}