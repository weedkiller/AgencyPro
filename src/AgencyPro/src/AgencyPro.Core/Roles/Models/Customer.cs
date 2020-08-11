// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Models;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.People.Models;
using AgencyPro.Core.Roles.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AgencyPro.Core.Cards.Models;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.CustomerAccounts.Models;
using AgencyPro.Core.Invoices.Models;
using AgencyPro.Core.Orders.Model;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.Proposals.Models;
using AgencyPro.Core.Retainers.Models;
using AgencyPro.Core.TimeEntries.Models;

namespace AgencyPro.Core.Roles.Models
{
    public class Customer : AuditableEntity, ICustomer
    {
        [ForeignKey("Id")] public Person Person { get; set; }

        public OrganizationMarketer OrganizationMarketer { get; set; }

        public ICollection<OrganizationCustomer> OrganizationCustomers { get; set; }
        public IndividualBuyerAccount BuyerAccount { get; set; }

        public ICollection<Organization> OwnedAgencies { get; set; }
        public ICollection<ProjectInvoice> Invoices { get; set; }
        public ICollection<ProposalAcceptance> ProposalsAccepted { get; set; }

        public ICollection<TimeEntry> ProviderTimeEntries { get; set; }
        public ICollection<TimeEntry> MarketingTimeEntries { get; set; }
        public ICollection<TimeEntry> RecruitingTimeEntries { get; set; }

        public ICollection<TimeEntry> BuyerTimeEntries { get; set; }

        public ICollection<Project> Projects { get; set; }
        public ICollection<CustomerAccount> CustomerAccounts { get; set; }
        public ICollection<CustomerCard> Cards { get; set; }
        public ICollection<WorkOrder> WorkOrders { get; set; }
        public ICollection<Contract> Contracts { get; set; }

        public Guid Id { get; set; }

        public Guid MarketerId { get; set; }
        public Guid MarketerOrganizationId { get; set; }
        public ICollection<ProjectRetainerIntent> RetainerIntents { get; set; }

    }
}