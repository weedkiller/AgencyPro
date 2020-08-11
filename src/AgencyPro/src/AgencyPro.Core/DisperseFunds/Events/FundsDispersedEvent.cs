// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Events;

namespace AgencyPro.Core.DisperseFunds.Events
{
    public class FundsDispersedEvent : BaseEvent
    {
        public string InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public int TotalTransfersMade { get; set; }
    }
}
