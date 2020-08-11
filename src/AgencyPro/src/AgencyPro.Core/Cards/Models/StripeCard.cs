// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.FinancialAccounts.Models;
using AgencyPro.Core.Models;

namespace AgencyPro.Core.Cards.Models
{
    public class StripeCard : AuditableEntity, IStripeCard
    {
        public string Id { get; set; }
        public string AddressCity { get; set; }
        public string AddressCountry { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
      
        public string Brand { get; set; }
        public string Country { get; set; }
        public string CvcCheck { get; set; }
        public string DynamicLast4 { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public string Fingerprint { get; set; }
        public string Funding { get; set; }
        public string Last4 { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
       
        public CustomerCard CustomerCard { get; set; }
        public AccountCard AccountCard { get; set; }
    }
}