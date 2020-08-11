// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.FinancialAccounts.Models;

namespace AgencyPro.Core.Cards.ViewModels
{
    public class AccountCardViewModel : IStripeCard
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
        public string Funding { get; set; }
        public string Last4 { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }

    }
}
