// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Linq;
using AgencyPro.Core.Charges.ViewModels;
using AgencyPro.Core.PayoutIntents.ViewModels;
using AgencyPro.Core.Transfers.ViewModels;

namespace AgencyPro.Core.Invoices.ViewModels
{
    public class AccountManagerProjectInvoiceDetailsOutput : AccountManagerProjectInvoiceOutput
    {
        public decimal AmountTransferred
        {
            get { return Transfers.Sum(x => x.Amount + x.AmountReversed); }
        }

        public decimal TotalCommission
        {
            get { return IndividualPayoutIntents.Sum(x => x.Amount) + OrganizationPayoutIntents.Sum(x=>x.Amount); }
        }

        public decimal AwaitingTransfer => TotalCommission - AmountTransferred;

        public int PayoutsIncomplete
        {
            get { return IndividualPayoutIntents.Count(x => !x.IsComplete); }
        }

        public int PayoutsWithoutFinancialAccounts
        {
            get { return IndividualPayoutIntents.Count(x => !x.PayoutsEnabled); }
        }

        public ICollection<InvoiceLineOutput> Lines { get; set; }
        public ICollection<InvoiceItemOutput> Items { get; set; }
        public ICollection<IndividualPayoutIntentOutput> IndividualPayoutIntents { get; set; }
        public ICollection<OrganizationPayoutIntentOutput> OrganizationPayoutIntents { get; set; }
        public ICollection<TransferOutput> Transfers { get; set; }
        public ICollection<ChargeOutput> Charges { get; set; }

    }
}