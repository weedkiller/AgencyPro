// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Roles.Services;

namespace AgencyPro.Core.Roles.ViewModels.Contractors
{
    public class ContractorOutput : ContractorInput, IContractor
    {
        public virtual string RecruiterName { get; set; }
        public virtual string RecruiterImageUrl { get; set; }
        public virtual string ContractorName { get; set; }
        public virtual string ContractorImageUrl { get; set; }
        public virtual DateTime? LastWorkedUtc { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
        public Guid Id { get; set; }
        public virtual decimal RecruiterStream { get; set; }
        public string DisplayName { get; set; }
    }
}