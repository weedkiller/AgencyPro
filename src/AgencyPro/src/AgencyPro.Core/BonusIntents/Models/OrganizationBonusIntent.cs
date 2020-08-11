// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.BonusIntents.Services;
using AgencyPro.Core.Candidates.Models;
using AgencyPro.Core.Leads.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.PayoutIntents.Models;

namespace AgencyPro.Core.BonusIntents.Models
{
    public class OrganizationBonusIntent : AuditableEntity, IOrganizationBonusIntent
    {
        public Guid Id { get; set; }

        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public BonusType BonusType { get; set; }

        public BonusTransfer BonusTransfer { get; set; }
        public string TransferId { get; set; }

        public decimal Amount { get; set; }
        public string Description { get; set; }

        public Guid? LeadId { get; set; }
        public Lead Lead { get; set; }
        public Guid? CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}