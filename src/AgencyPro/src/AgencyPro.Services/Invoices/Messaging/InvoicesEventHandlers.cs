// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Runtime.CompilerServices;
using AgencyPro.Core.EventHandling;
using AgencyPro.Core.Invoices.Events;
using AgencyPro.Services.Messaging.Email;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Invoices.Messaging
{
    public partial class InvoicesEventHandlers : MultiEventHandler<InvoiceEvent>,
        IEventHandler<InvoicePaidEvent>,
        IEventHandler<InvoiceFinalizedEvent>,
        IEventHandler<InvoiceCreatedEvent>,
        IEventHandler<InvoiceVoidedEvent>,
        IEventHandler<InvoiceViewedEvent>,
        IEventHandler<InvoiceUpdatedEvent>
    {
        private readonly ILogger<InvoicesEventHandlers> _logger;

        public InvoicesEventHandlers(
            ILogger<InvoicesEventHandlers> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = logger;
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(InvoicesEventHandlers)}.{callerName}] - {message}";
        }
    }
}
