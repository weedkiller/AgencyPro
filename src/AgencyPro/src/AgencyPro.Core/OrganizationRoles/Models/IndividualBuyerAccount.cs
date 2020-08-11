// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.BuyerAccounts.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.Roles.Models;

namespace AgencyPro.Core.OrganizationRoles.Models
{
    public class IndividualBuyerAccount : AuditableEntity
    {
        public Guid Id { get; set; }

        public Customer Customer { get; set; }

        public BuyerAccount BuyerAccount { get; set; }

        public string BuyerAccountId { get; set; }
    }
}