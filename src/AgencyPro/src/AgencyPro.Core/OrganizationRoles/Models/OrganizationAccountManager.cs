// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.CustomerAccounts.Models;
using AgencyPro.Core.Invoices.Models;
using AgencyPro.Core.Leads.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.Orders.Model;
using AgencyPro.Core.OrganizationPeople.Models;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Organizations.ProviderOrganizations;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.Retainers.Models;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.TimeEntries.Models;

namespace AgencyPro.Core.OrganizationRoles.Models
{
    public class OrganizationAccountManager : AuditableEntity, IOrganizationAccountManager
    {
        public Organization Organization { get; set; }
        
        public AccountManager AccountManager { get; set; }

        public ICollection<ProviderOrganization> DefaultOrganizations { get; set; }

        public ICollection<WorkOrder> WorkOrders { get; set; }
        public ICollection<ProjectInvoice> Invoices { get; set; }

        public OrganizationPerson OrganizationPerson { get; set; }
        public ICollection<TimeEntry> TimeEntries { get; set; }
        public decimal AccountManagerStream { get; set; }

        public ICollection<Project> Projects { get; set; }

        public ICollection<CustomerAccount> Accounts { get; set; }

        public ICollection<Lead> Leads { get; set; }
        public ICollection<Contract> Contracts { get; set; }

        public bool IsDeleted { get; set; }
        public Guid OrganizationId { get; set; }

        public Guid AccountManagerId { get; set; }
        public virtual string ConcurrencyStamp { get; set; }
        public Guid CreatedById { get; set; }
        public Guid UpdatedById { get; set; }
        public ICollection<ProjectRetainerIntent> RetainerIntents { get; set; }
    }
}