// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Common;
using AgencyPro.Core.TimeEntries.Enums;

namespace AgencyPro.Core.TimeEntries.ViewModels
{
    public class TimeMatrixFilters : CommonFilters
    {
        public static readonly TimeMatrixFilters NoFilter = new TimeMatrixFilters();

        public TimeMatrixFilters()
        {
            ApprovalStatus = new TimeStatus[]{};
            TimeType = new int[]{};
        }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? ContractorId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? ContractId { get; set; }
        public Guid? ProjectManagerId { get; set; }
        public TimeStatus[] ApprovalStatus { get; set; }
        public int[] TimeType { get; set; }
        public Guid? AccountManagerId { get; set; }
        public Guid? ProviderOrganizationId { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? CustomerOrganizationId { get; set; }
        public Guid? RecruiterId { get; set; }
        public Guid? RecruiterOrganizationId { get; set; }
        public Guid? MarketerId { get; set; }
        public Guid? MarketerOrganizationId { get; set; }
        public Guid? StoryId { get; set; }
    }
}