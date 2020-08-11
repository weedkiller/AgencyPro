// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Models;

namespace AgencyPro.Core.Plans.Models
{
    public class StripePlan : BaseObjectState
    {
        public int Id { get; set; }
        public string UniqueId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string Interval { get; set; }
        public bool IsActive { get; set; }
        public string StripeId { get; set; }
        public string StripeBlob { get; set; }
        public string ProductId { get; set; }
        public int TrialPeriodDays { get; set; }

    }
}
