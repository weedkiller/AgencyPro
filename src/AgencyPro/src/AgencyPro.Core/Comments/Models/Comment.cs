// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Candidates.Models;
using AgencyPro.Core.Comments.Services;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.CustomerAccounts.Models;
using AgencyPro.Core.Leads.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.OrganizationPeople.Models;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.Stories.Models;

namespace AgencyPro.Core.Comments.Models
{
    public class Comment : AuditableEntity, IComment
    {
        public Guid Id { get; set; }
        public string Body { get; set; }

        public bool Internal { get; set; }

        public Guid? StoryId { get; set; }
        public Story Story { get; set; }
        public Guid? ProjectId { get; set; }
        public Project Project { get; set; }
        public Guid? ContractId { get; set; }
        public Contract Contract { get; set; }

        public Guid? LeadId { get; set; }
        public Lead Lead { get; set; }

        public Guid? CandidateId { get; set; }
        public Candidate Candidate { get; set; }

        public Guid CreatedById { get; set; }
        public Guid UpdatedById { get; set; }


        public Guid? AccountManagerId { get; set; }
        public Guid? AccountManagerOrganizationId { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? CustomerOrganizationId { get; set; }

        public CustomerAccount CustomerAccount { get; set; }

        /// <summary>
        /// This is the organization of the person creating the comment
        /// </summary>
        public Guid OrganizationId { get; set; }

        public OrganizationPerson Creator { get; set; }

        public bool IsDeleted { get; set; }
    }
}
