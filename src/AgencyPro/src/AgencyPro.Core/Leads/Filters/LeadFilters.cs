// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Leads.Enums;

namespace AgencyPro.Core.Leads.Filters
{
    public class LeadFilters
    {
        public static readonly LeadFilters NoFilter = new LeadFilters();

        public LeadStatus[] Statuses { get; set; }

        public Guid? AccountManagerId { get; set; }
        public Guid? AccountManagerOrganizationId { get; set; }
        public Guid? MarketerId { get; set; }
        public Guid? MarketerOrganizationId { get; set; }
    }
}