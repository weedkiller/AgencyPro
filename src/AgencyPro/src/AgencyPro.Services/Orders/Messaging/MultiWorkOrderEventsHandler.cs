// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Runtime.CompilerServices;
using AgencyPro.Core.EventHandling;
using AgencyPro.Core.Orders.Events;
using AgencyPro.Services.Messaging.Email;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Orders.Messaging
{
    public partial class MultiWorkOrderEventsHandler : MultiEventHandler<WorkOrderEvent>,
        IEventHandler<WorkOrderCreatedEvent>,
        IEventHandler<WorkOrderAcceptedEvent>,
        IEventHandler<WorkOrderRejectedEvent>
    {
        private readonly ILogger<MultiWorkOrderEventsHandler> _logger;

        public MultiWorkOrderEventsHandler(
            ILogger<MultiWorkOrderEventsHandler> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = logger;
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(MultiWorkOrderEventsHandler)}.{callerName}] - {message}";
        }
    }
}
