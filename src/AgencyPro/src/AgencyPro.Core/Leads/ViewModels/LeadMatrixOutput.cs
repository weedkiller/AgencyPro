// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Leads.Enums;
using AgencyPro.Core.Leads.Services;

namespace AgencyPro.Core.Leads.ViewModels
{
    public class LeadMatrixOutput : ILeadMatrix
    {
        public virtual Guid MarketerId { get; set; }
        public virtual Guid MarketerOrganizationId { get; set; }
        public int Count { get; set; }
        public virtual LeadStatus Status { get; set; }
        public DateTime Date { get; set; }
    }
}
