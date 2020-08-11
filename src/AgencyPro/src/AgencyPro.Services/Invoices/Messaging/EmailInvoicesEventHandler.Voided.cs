// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Invoices.Events;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Invoices.Messaging
{
    public partial class InvoicesEventHandlers
    {
        public void Handle(InvoiceVoidedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Invoice voided event triggered"));
        }
    }
}