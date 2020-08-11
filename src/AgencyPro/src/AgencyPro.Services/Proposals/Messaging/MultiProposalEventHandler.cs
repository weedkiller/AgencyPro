// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Runtime.CompilerServices;
using AgencyPro.Core.EventHandling;
using AgencyPro.Services.Messaging.Email;
using AgencyPro.Core.Proposals.Events;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Proposals.Messaging
{
    public partial class MultiProposalEventHandler : MultiEventHandler<ProposalEvent>,
        IEventHandler<ProposalCreatedEvent>,
        IEventHandler<ProposalSentEvent>,
        IEventHandler<ProposalAcceptedEvent>,
        IEventHandler<ProposalRejectedEvent>
    {
        private readonly ILogger<MultiProposalEventHandler> _logger;

        public MultiProposalEventHandler(
            ILogger<MultiProposalEventHandler> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = logger;
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(MultiProposalEventHandler)}.{callerName}] - {message}";
        }
    }
}
