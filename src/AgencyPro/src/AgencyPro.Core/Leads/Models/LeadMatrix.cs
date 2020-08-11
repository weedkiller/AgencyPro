// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Leads.Enums;
using AgencyPro.Core.Leads.Services;
using AgencyPro.Core.OrganizationRoles.Models;

namespace AgencyPro.Core.Leads.Models
{
    public class LeadMatrix : ILeadMatrix
    {
        public OrganizationMarketer OrganizationMarketer { get; set; }

        public Guid MarketerId { get; set; }
        public Guid MarketerOrganizationId { get; set; }
        public int Count { get; set; }
        public LeadStatus Status { get; set; }
        public DateTime Date { get; set; }
    }
}