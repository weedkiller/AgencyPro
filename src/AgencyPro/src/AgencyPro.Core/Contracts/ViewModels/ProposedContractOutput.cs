// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Contracts.ViewModels
{

    public class ProposedContractOutput
    {
        public virtual Guid Id { get; set; }
        public virtual decimal CustomerRate { get; set; }
        public virtual decimal MaxCustomerWeekly { get; set; }
        public virtual string ContractorName { get; set; }
        public virtual string ContractorOrganizationName { get; set; }
        public virtual string ContractorImageUrl { get; set; }
        public virtual string ContractorOrganizationImageUrl { get; set; }
    }
}