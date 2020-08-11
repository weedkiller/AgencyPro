// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Leads.Enums;

namespace AgencyPro.Core.Leads.Services
{
    public interface ILeadMatrix
    {
        Guid MarketerId { get; set; }
        Guid MarketerOrganizationId { get; set; }
        int Count { get; set; }
        LeadStatus Status { get; set; }
        DateTime Date { get; set; }


    }
}