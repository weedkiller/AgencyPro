// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Leads.ViewModels
{
    public class LeadMatrixFilters
    {
        public static readonly LeadMatrixFilters NoFilter = new LeadMatrixFilters();

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Guid? MarketerId { get; set; }
        public Guid? MarketerOrganizationId { get; set; }

    }
}