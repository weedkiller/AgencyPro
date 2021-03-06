﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.CustomerAccounts.Models;
using AgencyPro.Core.Invoices.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.OrganizationPeople.Models;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.Proposals.Models;
using AgencyPro.Core.Retainers.Models;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.TimeEntries.Models;

namespace AgencyPro.Core.OrganizationRoles.Models
{
    public class OrganizationCustomer : AuditableEntity, IOrganizationCustomer
    {
        public Guid OrganizationId { get; set; }
        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; }

        public Organization Organization { get; set; }
        public OrganizationPerson OrganizationPerson { get; set; }
        public ICollection<Contract> Contracts { get; set; }

        public ICollection<CustomerAccount> Accounts { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<TimeEntry> TimeEntries { get; set; }
        public ICollection<ProjectInvoice> Invoices { get; set; }
        //public ICollection<OrganizationPayoutIntent> PayoutIntents { get; set; }
        public ICollection<ProposalAcceptance> ProposalsAccepted { get; set; }


        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
        public virtual string ConcurrencyStamp { get; set; }

        public Guid CreatedById { get; set; }
        public Guid UpdatedById { get; set; }
        public ICollection<ProjectRetainerIntent> RetainerIntents { get; set; }
    }
}