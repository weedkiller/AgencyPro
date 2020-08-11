// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.People.Models;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Skills.Models;
using AgencyPro.Core.Stories.Models;
using AgencyPro.Core.TimeEntries.Models;

namespace AgencyPro.Core.Roles.Models
{
    public class Contractor : AuditableEntity, IContractor
    {
        [ForeignKey("Id")] public Person Person { get; set; }

        public OrganizationRecruiter OrganizationRecruiter { get; set; }
        public ICollection<Contract> Contracts { get; set; }
        public ICollection<Story> Stories { get; set; }
        public ICollection<TimeEntry> TimeEntries { get; set; }
        public ICollection<OrganizationContractor> OrganizationContractors { get; set; }
        public Guid Id { get; set; }

        public Guid RecruiterId { get; set; }
        public Recruiter Recruiter { get; set; }
        public Guid RecruiterOrganizationId { get; set; }

        public bool IsAvailable { get; set; }
        
        public int HoursAvailable { get; set; }

        public DateTime? LastWorkedUtc { get; set; }

        public ICollection<ContractorSkill> ContractorSkills { get; set; }
    }
}