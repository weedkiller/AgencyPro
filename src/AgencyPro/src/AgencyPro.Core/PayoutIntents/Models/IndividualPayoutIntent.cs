// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Models;
using AgencyPro.Core.OrganizationPeople.Models;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.PayoutIntents.Services;
using AgencyPro.Core.People.Models;
using AgencyPro.Core.Stripe.Model;

namespace AgencyPro.Core.PayoutIntents.Models
{
    public class IndividualPayoutIntent : AuditableEntity, IIndividualPayoutIntent
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }
        public Person Person { get; set; }

        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; }

        public OrganizationPerson OrganizationPerson { get; set; }

        public string InvoiceId { get; set; }
        public StripeInvoice Invoice { get; set; }

        public string InvoiceItemId { get; set; }
        public StripeInvoiceItem InvoiceItem { get; set; }

        public decimal Amount { get; set; }
        public CommissionType Type { get; set; }

        public string Description { get; set; }
        
        public InvoiceTransfer InvoiceTransfer { get; set; }
        public string InvoiceTransferId { get; set; }
    }
}