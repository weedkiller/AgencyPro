// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.Leads.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.OrganizationPeople.Models;
using AgencyPro.Core.Organizations.MarketingOrganizations.Models;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.TimeEntries.Models;

namespace AgencyPro.Core.OrganizationRoles.Models
{
    public class OrganizationMarketer : AuditableEntity
    {
        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; }

        public ICollection<MarketingOrganization> OrganizationDefaults { get; set; }
        public ICollection<TimeEntry> TimeEntries { get; set; }

        public Guid MarketerId { get; set; }
        public Marketer Marketer { get; set; }

        public string ReferralCode { get; set; }

        public decimal MarketerStream { get; set; }

        public ICollection<Contract> Contracts { get; set; }
        public ICollection<Customer> Customers { get; set; }
        public ICollection<Lead> Leads { get; set; }
        public OrganizationPerson OrganizationPerson { get; set; }

        public bool IsSystemDefault { get; set; }
        public bool IsDeleted { get; set; }
        public virtual string ConcurrencyStamp { get; set; }


        public Guid CreatedById { get; set; }
        public Guid UpdatedById { get; set; }
        public decimal MarketerBonus { get; set; }
    }
}