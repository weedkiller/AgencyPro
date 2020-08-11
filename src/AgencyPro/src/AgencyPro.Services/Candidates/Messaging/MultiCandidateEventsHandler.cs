// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Runtime.CompilerServices;
using AgencyPro.Core.Candidates.Events;
using AgencyPro.Core.EventHandling;
using AgencyPro.Services.Messaging.Email;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Candidates.Messaging
{
    public partial class MultiCandidateEventsHandler : MultiEventHandler<CandidateEvent>,
        IEventHandler<CandidateQualifiedEvent>,
        IEventHandler<CandidateCreatedEvent>,
        IEventHandler<CandidatePromotedEvent>,
        IEventHandler<CandidateRejectedEvent>
    {
        private readonly ILogger<MultiCandidateEventsHandler> _logger;

        public MultiCandidateEventsHandler(
            ILogger<MultiCandidateEventsHandler> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = logger;
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(MultiCandidateEventsHandler)}.{callerName}] - {message}";
        }
    }
}