// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Roles.ViewModels.Contractors
{
    public class ContractorInput : ContractorUpdateInput
    {
        public virtual Guid RecruiterId { get; set; }
        public virtual Guid RecruiterOrganizationId { get; set; }

        public virtual Guid PersonId { get; set; }

    }
}