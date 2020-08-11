// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AgencyPro.Core.Candidates.Models;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.Invoices.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.People.Models;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.TimeEntries.Models;

namespace AgencyPro.Core.Roles.Models
{
    public class ProjectManager : AuditableEntity, IProjectManager
    {
        [ForeignKey("Id")] public Person Person { get; set; }

        public ICollection<OrganizationProjectManager> OrganizationProjectManagers { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<Candidate> Candidates { get; set; }
        public ICollection<Contract> Contracts { get; set; }
        public ICollection<TimeEntry> TimeEntries { get; set; }
        public ICollection<ProjectInvoice> Invoices { get; set; }

        public Guid Id { get; set; }
    }
}