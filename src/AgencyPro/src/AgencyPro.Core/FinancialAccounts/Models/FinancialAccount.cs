// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Models;
using System.Collections.Generic;
using AgencyPro.Core.Cards.Models;
using AgencyPro.Core.Charges.Models;
using AgencyPro.Core.FinancialAccounts.Services;
using AgencyPro.Core.Transfers.Models;

namespace AgencyPro.Core.FinancialAccounts.Models
{
    public class FinancialAccount : AuditableEntity, IFinancialAccount
    {
  
        public string RefreshToken { get; set; }

        public string AccessToken { get; set; }

        public string AccountId { get; set; }
        public string StripePublishableKey { get; set; }

        public string AccountType { get; set; }
        public FinancialAccountStatus Status { get; set; }

       // public ICollection<Payment> Payments { get; set; }

        public virtual OrganizationFinancialAccount OrganizationFinancialAccount { get; set; }

        public virtual IndividualFinancialAccount IndividualFinancialAccount { get; set; }
        
        public bool IsDeleted { get; set; }
        public bool ChargesEnabled { get; set; }
        public bool PayoutsEnabled { get; set; }

        public string CardIssuingCapabilityStatus { get; set; }

        public string CardPaymentsCapabilityStatus { get; set; }
        public string TransfersCapabilityStatus { get; set; }

       // public string Blob { get; set; }
        public string DefaultCurrency { get; set; }
        public string MerchantCategoryCode { get; set; }
        public string SupportEmail { get; set; }
        public string SupportPhone { get; set; }

        public ICollection<StripeCharge> DestinationCharges { get; set; }

        public ICollection<StripeTransfer> Transfers { get; set; }
        public ICollection<AccountCard> Cards { get; set; }
    }

}