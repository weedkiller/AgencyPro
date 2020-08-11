// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Runtime.CompilerServices;
using AgencyPro.Core.EventHandling;
using AgencyPro.Core.Leads.Events;
using AgencyPro.Services.Messaging.Email;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Leads.EventHandlers
{
    public partial class MultiLeadEventHandler : MultiEventHandler<LeadEvent>,
        IEventHandler<LeadCreatedEvent>,
        IEventHandler<LeadQualifiedEvent>,
        IEventHandler<LeadPromotedEvent>,
        IEventHandler<LeadRejectedEvent>
    {
        private readonly ILogger<MultiLeadEventHandler> _logger;
      
        public MultiLeadEventHandler(
            IServiceProvider serviceProvider, ILogger<MultiLeadEventHandler> logger)
            : base(serviceProvider)
        {
            _logger = logger;


        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(MultiLeadEventHandler)}.{callerName}] - {message}";
        }
    }
}