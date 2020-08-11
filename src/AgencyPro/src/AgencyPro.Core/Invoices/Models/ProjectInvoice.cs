// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.CustomerAccounts.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Stripe.Model;

namespace AgencyPro.Core.Invoices.Models
{
    public class ProjectInvoice : AuditableEntity
    {
        public Project Project { get; set; }
        
        public Guid ProjectId { get; set; }
      
        public string RefNo { get; set; }
       
        public string InvoiceId { get; set; }

        public StripeInvoice Invoice { get; set; }

        public Guid AccountManagerId { get; set; }
        public AccountManager AccountManager { get; set; }
        public Guid ProviderOrganizationId { get; set; }
        public Organization ProviderOrganization { get; set; }
        public OrganizationAccountManager OrganizationAccountManager { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Guid BuyerOrganizationId { get; set; }
        public Organization BuyerOrganization { get; set; }
        public OrganizationCustomer OrganizationCustomer { get; set; }

        public CustomerAccount CustomerAccount { get; set; }


        public Guid ProjectManagerId { get; set; }
        public ProjectManager ProjectManager { get; set; }
        public OrganizationProjectManager OrganizationProjectManager { get; set; }
        
    }
}