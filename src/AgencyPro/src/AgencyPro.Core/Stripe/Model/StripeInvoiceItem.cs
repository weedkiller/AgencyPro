// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.BuyerAccounts.Models;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.PayoutIntents.Models;
using AgencyPro.Core.TimeEntries.Models;

namespace AgencyPro.Core.Stripe.Model
{
    public class StripeInvoiceItem : AuditableEntity, IStripeInvoiceItem
    {
        public StripeInvoiceItem()
        {

            this.TimeEntries = new List<TimeEntry>();
            this.InvoiceLines = new List<StripeInvoiceLine>();
            this.IndividualPayoutIntents = new List<IndividualPayoutIntent>();
            this.OrganizationPayoutIntents = new List<OrganizationPayoutIntent>();
        }

        public string Id { get; set; }
        public decimal Amount { get; set; }

        public string CustomerId { get; set; }
        public BuyerAccount Customer { get; set; }

        public string Description { get; set; }

        public string InvoiceId { get; set; }
        public StripeInvoice Invoice { get; set; }
        
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime? PeriodStart { get; set; }
        public DateTime? PeriodEnd { get; set; }

        public ICollection<TimeEntry> TimeEntries { get; set; }
        public ICollection<StripeInvoiceLine> InvoiceLines { get; set; }
        public ICollection<IndividualPayoutIntent> IndividualPayoutIntents { get; set; }
        public ICollection<OrganizationPayoutIntent> OrganizationPayoutIntents { get; set; }


        public Guid? ContractId { get; set; }
        public Contract Contract { get; set; }
    }
}