// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.FinancialAccounts.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.PayoutIntents.Models;
using AgencyPro.Core.Transfers.Services;

namespace AgencyPro.Core.Transfers.Models
{
    public class StripeTransfer : AuditableEntity, IStripeTransfer
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountReversed { get; set; }
        public string Description { get; set; }

        public string DestinationId { get; set; }
        public FinancialAccount DestinationAccount { get; set; }

        public string DestinationPaymentId { get; set; }
        public bool IsDeleted { get; set; }
        public InvoiceTransfer InvoiceTransfer { get; set; }
        public BonusTransfer BonusTransfer { get; set; }
    }
}