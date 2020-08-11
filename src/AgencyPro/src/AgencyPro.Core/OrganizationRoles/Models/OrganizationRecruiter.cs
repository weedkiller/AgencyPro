// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.Candidates.Models;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.OrganizationPeople.Models;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Organizations.RecruitingOrganizations.Models;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.TimeEntries.Models;

namespace AgencyPro.Core.OrganizationRoles.Models
{
    public class OrganizationRecruiter : AuditableEntity, IOrganizationRecruiter
    {
        public Organization Organization { get; set; }
        public Recruiter Recruiter { get; set; }

        public OrganizationPerson OrganizationPerson { get; set; }

        public decimal RecruiterStream { get; set; }

        public ICollection<Contract> Contracts { get; set; }
        public ICollection<TimeEntry> TimeEntries { get; set; }

        public ICollection<Contractor> Contractors { get; set; }

        public ICollection<Candidate> Candidates { get; set; }
        public bool IsSystemDefault { get; set; }

        public bool IsDeleted { get; set; }
        public Guid OrganizationId { get; set; }

        public Guid RecruiterId { get; set; }
        public string ConcurrencyStamp { get; set; }


        public Guid CreatedById { get; set; }
        public Guid UpdatedById { get; set; }
        public ICollection<RecruitingOrganization> RecruitingOrganizationDefaults { get; set; }
        public decimal RecruiterBonus { get; set; }

    }
}