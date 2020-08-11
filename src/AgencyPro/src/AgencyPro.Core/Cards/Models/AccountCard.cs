// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.FinancialAccounts.Models;
using AgencyPro.Core.Models;

namespace AgencyPro.Core.Cards.Models
{
    public class AccountCard : AuditableEntity
    {
        public string Id { get; set; }

        public string AccountId { get; set; }
        public FinancialAccount FinancialAccount { get; set; }

        public StripeCard StripeCard { get; set; }
        public bool IsDeleted { get; set; }

        public string Status { get; set; }
        public string Type { get; set; }

    }
}