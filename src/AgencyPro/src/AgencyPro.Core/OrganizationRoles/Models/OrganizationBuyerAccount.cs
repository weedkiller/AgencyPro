// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.ComponentModel.DataAnnotations.Schema;
using AgencyPro.Core.BuyerAccounts.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.Organizations.Models;

namespace AgencyPro.Core.OrganizationRoles.Models
{
    public class OrganizationBuyerAccount : AuditableEntity
    {
        public Guid Id { get; set; }

        [ForeignKey(nameof(Id))]
        public Organization Organization { get; set; }

        [ForeignKey(nameof(BuyerAccountId))]
        public BuyerAccount BuyerAccount { get; set; }

        public string BuyerAccountId { get; set; }
    }
}