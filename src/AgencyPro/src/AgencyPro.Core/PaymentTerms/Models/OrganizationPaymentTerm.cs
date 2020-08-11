// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Models;
using AgencyPro.Core.Organizations.Models;

namespace AgencyPro.Core.PaymentTerms.Models
{
    public class OrganizationPaymentTerm : AuditableEntity
    {
        public Guid OrganizationId { get; set; }
        public int PaymentTermId { get; set; }

        public bool IsDefault { get; set; }

        public Organization Organization { get; set; }
        public PaymentTerm PaymentTerm { get; set; }
    }
}