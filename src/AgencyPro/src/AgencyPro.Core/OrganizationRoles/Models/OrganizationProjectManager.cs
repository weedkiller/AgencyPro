// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.Candidates.Models;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.Invoices.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.OrganizationPeople.Models;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Organizations.ProviderOrganizations;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.TimeEntries.Models;

namespace AgencyPro.Core.OrganizationRoles.Models
{
    public class OrganizationProjectManager : AuditableEntity, IOrganizationProjectManager
    {
        public Organization Organization { get; set; }
        public ProjectManager ProjectManager { get; set; }

        public OrganizationPerson OrganizationPerson { get; set; }

        public ICollection<ProviderOrganization> DefaultOrganizations { get; set; }
        public ICollection<Contract> Contracts { get; set; }
        public ICollection<TimeEntry> TimeEntries { get; set; }
        public ICollection<ProjectInvoice> Invoices { get; set; }

        public decimal ProjectManagerStream { get; set; }

        public ICollection<Project> Projects { get; set; }

        public ICollection<Candidate> Candidates { get; set; }

        public bool IsDeleted { get; set; }
        public Guid OrganizationId { get; set; }

        public Guid ProjectManagerId { get; set; }
        public string ConcurrencyStamp { get; set; }


        public Guid CreatedById { get; set; }
        public Guid UpdatedById { get; set; }

    }
}