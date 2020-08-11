// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Models;
using AgencyPro.Core.Organizations.Models;

namespace AgencyPro.Core.FinancialAccounts.Models
{
    public class OrganizationFinancialAccount : AuditableEntity
    {
        public Guid Id { get; set; }
        
        public FinancialAccount FinancialAccount { get; set; }
        public Organization Organization { get; set; }

        public string FinancialAccountId { get; set; }


    }
}