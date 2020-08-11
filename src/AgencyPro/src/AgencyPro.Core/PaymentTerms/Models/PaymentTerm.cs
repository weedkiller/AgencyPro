// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.CustomerAccounts.Models;
using AgencyPro.Core.Models;

namespace AgencyPro.Core.PaymentTerms.Models
{
    public class PaymentTerm : BaseObjectState
    {
        public string Name { get; set; }
        public int NetValue { get; set; }
        public int PaymentTermId { get; set; }

        public ICollection<CategoryPaymentTerm> CategoryPaymentTerms { get; set; }
        public ICollection<OrganizationPaymentTerm> OrganizationPaymentTerms { get; set; }

        public ICollection<CustomerAccount> CustomerAccounts { get; set; }
    }
}