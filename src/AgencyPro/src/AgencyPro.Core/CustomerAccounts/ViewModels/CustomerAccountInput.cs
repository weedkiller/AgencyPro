// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.CustomerAccounts.ViewModels
{
    public class CustomerAccountInput
    {
        public virtual Guid AccountManagerId { get; set; }
        public virtual Guid AccountManagerOrganizationId { get; set; }

        public virtual Guid CustomerId { get; set; }
        public virtual Guid CustomerOrganizationId { get; set; }

        public bool AutoApproveTimeEntries { get; set; }

        public int? PaymentTermId { get; set; }
        
    }
}