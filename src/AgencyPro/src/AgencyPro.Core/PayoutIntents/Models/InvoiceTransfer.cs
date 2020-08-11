// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Models;
using AgencyPro.Core.Stripe.Model;
using AgencyPro.Core.Transfers.Models;

namespace AgencyPro.Core.PayoutIntents.Models
{
    public class InvoiceTransfer : BaseObjectState
    {
        public string TransferId { get; set; }
        public StripeTransfer Transfer { get; set; }

        public string InvoiceId { get; set; }
        public StripeInvoice Invoice { get; set; }
        
        public ICollection<IndividualPayoutIntent> IndividualPayoutIntents { get; set; }
        public ICollection<OrganizationPayoutIntent> OrganizationPayoutIntents { get; set; }
    }
}