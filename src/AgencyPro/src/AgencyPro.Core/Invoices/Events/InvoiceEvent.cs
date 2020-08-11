// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Events;

namespace AgencyPro.Core.Invoices.Events
{
    public abstract class InvoiceEvent : BaseEvent
    {
        public string InvoiceId { get; set; }
    }
}