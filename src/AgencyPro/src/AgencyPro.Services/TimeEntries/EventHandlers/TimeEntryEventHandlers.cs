// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Runtime.CompilerServices;
using AgencyPro.Core.EventHandling;
using AgencyPro.Core.TimeEntries.Events;
using AgencyPro.Services.Messaging.Email;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.TimeEntries.EventHandlers
{
    public partial class TimeEntryEventHandlers : MultiEventHandler<TimeEntryEvent>,
        IEventHandler<TimeEntryLoggedEvent>,
        IEventHandler<TimeEntryApprovedEvent>
    {
        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(TimeEntryEventHandlers)}.{callerName}] - {message}";
        }

        private readonly ILogger<TimeEntryEventHandlers> _logger;
    
        public TimeEntryEventHandlers(
            ILogger<TimeEntryEventHandlers> logger, 
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = logger;

        }
    }
}
