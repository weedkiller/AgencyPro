// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.CustomerAccounts.Models;
using AgencyPro.Core.Invoices.Models;
using AgencyPro.Core.Leads.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.Orders.Model;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.People.Models;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.Retainers.Models;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.TimeEntries.Models;

namespace AgencyPro.Core.Roles.Models
{
    public class AccountManager : AuditableEntity, IAccountManager
    {
        [ForeignKey("Id")] public Person Person { get; set; }

        public ICollection<OrganizationAccountManager> OrganizationAccountManagers { get; set; }
        public ICollection<WorkOrder> WorkOrders { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<CustomerAccount> CustomerAccounts { get; set; }
        public ICollection<Lead> Leads { get; set; }
        public ICollection<Contract> Contracts { get; set; }
        public ICollection<TimeEntry> TimeEntries { get; set; }
        public ICollection<ProjectInvoice> Invoices { get; set; }
        public Guid Id { get; set; }
        public ICollection<ProjectRetainerIntent> RetainerIntents { get; set; }
    }
}