// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Models;
using AgencyPro.Core.People.Models;

namespace AgencyPro.Core.FinancialAccounts.Models
{
    public class IndividualFinancialAccount : AuditableEntity
    {
        public Guid Id { get; set; }

        public string FinancialAccountId { get; set; }
       
        public FinancialAccount FinancialAccount { get; set; }
        
        public Person Person { get; set; }
    }
}