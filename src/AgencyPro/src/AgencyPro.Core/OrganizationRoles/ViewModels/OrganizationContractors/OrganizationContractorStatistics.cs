// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.TimeEntries.Enums;
using System.Collections.Generic;

namespace AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors
{
    public class OrganizationContractorStatistics : OrganizationContractorOutput
    {
        public virtual int TotalContracts { get; set; }
        public virtual int TotalStories { get; set; }

        public int MaxBillableHours { get; set; }
        public int MaxWeeklyHours { get; set; }

        public int AvailableHours => MaxWeeklyHours - MaxBillableHours;

        public Dictionary<TimeStatus, decimal> TimeEntryHours { get; set; }
        public Dictionary<TimeStatus, decimal> TimeEntryEarnings { get; set; }
    }
}