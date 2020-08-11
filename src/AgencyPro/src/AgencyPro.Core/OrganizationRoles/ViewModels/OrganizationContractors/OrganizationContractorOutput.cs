// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.People.Enums;

namespace AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors
{
    public class OrganizationContractorOutput : OrganizationContractorInput
    {
        public virtual string PublicDisplayName { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual PersonStatus Status { get; set; }
        public virtual string ImageUrl { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string Email { get; set; }
        public virtual Guid RecruiterId { get; set; }
        public virtual Guid RecruiterOrganizationId { get; set; }
        public virtual string RecruiterName { get; set; }
        public virtual string RecruiterOrganizationName { get; set; }
        public virtual string RecruiterImageUrl { get; set; }
        public virtual string RecruiterOrganizationImageUrl { get; set; }
    }
}