// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.Proposals.Enums;
using AgencyPro.Core.Retainers.Models;
using AgencyPro.Core.Roles.Models;

namespace AgencyPro.Core.Proposals.Models
{
    public class ProposalAcceptance : AuditableEntity
    {
        public Guid Id { get; set; }

        [ForeignKey("Id")] public FixedPriceProposal Proposal { get; set; }

        public DateTimeOffset AcceptedCompletionDate { get; set; }

        public decimal TotalCost { get; set; }

        public int NetTerms { get; set; }

        public decimal? RetainerAmount { get; set; }
        
        public string ProposalBlob { get; set; }
        public decimal CustomerRate { get; set; }
        public string AgreementText { get; set; }
        public ProposalType ProposalType { get; set; }
        public decimal TotalDays { get; set; }
        public decimal Velocity { get; set; }

        public Guid AcceptedBy { get; set; }
        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; }

        public Guid CustomerOrganizationId { get; set; }
        public OrganizationCustomer OrganizationCustomer { get; set; }
    }
}