// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AgencyPro.Core.BuyerAccounts.Services;
using AgencyPro.Core.Cards.Models;
using AgencyPro.Core.Charges.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.Stripe.Model;

namespace AgencyPro.Core.BuyerAccounts.Models
{
    public class BuyerAccount : AuditableEntity, IBuyerAccount
    {
        private ICollection<StripeInvoiceItem> _items;

        public decimal Balance { get; set; }
        public bool Delinquent { get; set; }

        public string Id { get; set; }
        
        public virtual OrganizationBuyerAccount OrganizationBuyerAccount { get; set; }
        public virtual IndividualBuyerAccount IndividualBuyerAccount { get; set; }
        
        public virtual ICollection<StripeInvoiceItem> InvoiceItems
        {
            get => _items ?? (_items = new Collection<StripeInvoiceItem>());
            set => _items = value;
        }



        private ICollection<StripeSource> _sources;

        public virtual ICollection<StripeSource> PaymentSources
        {
            get => _sources ?? (_sources = new Collection<StripeSource>());
            set => _sources = value;
        }



        private ICollection<StripeInvoice> _invoices;

        public virtual ICollection<StripeInvoice> Invoices
        {
            get => _invoices ?? (_invoices = new Collection<StripeInvoice>());
            set => _invoices = value;
        }
        
        private ICollection<StripeCharge> _charges;

        public virtual ICollection<StripeCharge> Charges
        {
            get => _charges ?? (_charges = new Collection<StripeCharge>());
            set => _charges = value;
        }

        public ICollection<StripeCheckoutSession> CheckoutSessions { get; set; }
       

        private ICollection<CustomerCard> _cards;

        public virtual ICollection<CustomerCard> Cards
        {
            get => _cards ?? (_cards = new Collection<CustomerCard>());
            set => _cards = value;
        }

        public bool IsDeleted { get; set; }
    }
}