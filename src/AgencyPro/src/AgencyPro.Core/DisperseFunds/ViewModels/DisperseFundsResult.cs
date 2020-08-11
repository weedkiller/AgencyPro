// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.ViewModels;

namespace AgencyPro.Core.DisperseFunds.ViewModels
{
    public class DisperseFundsResult : BaseResult
    {
        public string InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public string TransferId { get; set; }
        public int TotalTransfersMade { get; set; }
    }
}
