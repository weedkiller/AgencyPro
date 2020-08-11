// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.Levels;
using AgencyPro.Core.Models;
using AgencyPro.Core.OrganizationPeople.Models;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Organizations.ProviderOrganizations;
using AgencyPro.Core.Positions.Models;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Stories.Models;
using AgencyPro.Core.TimeEntries.Models;

namespace AgencyPro.Core.OrganizationRoles.Models
{
    public class OrganizationContractor : AuditableEntity
    {
        public Guid OrganizationId { get; set; }
        public Guid ContractorId { get; set; }

        public decimal ContractorStream { get; set; }

        public OrganizationPerson OrganizationPerson { get; set; }

        public ICollection<ProviderOrganization> DefaultOrganizations { get; set; }
        public Organization Organization { get; set; }
        public Contractor Contractor { get; set; }

        public bool IsFeatured { get; set; }
        public string Biography { get; set; }
        public string PortfolioMediaUrl { get; set; }

        public ICollection<TimeEntry> TimeEntries { get; set; }
        public ICollection<Contract> Contracts { get; set; }
        public ICollection<Story> Stories { get; set; }

        public bool IsDeleted { get; set; }
        public virtual string ConcurrencyStamp { get; set; }


        public Guid CreatedById { get; set; }
        public Guid UpdatedById { get; set; }

        public int? LevelId { get; set; }
        public Level Level { get; set; }
        public int? PositionId { get; set; }
        public Position Position { get; set; }

        public bool AutoApproveTimeEntries { get; set; }
    }
}