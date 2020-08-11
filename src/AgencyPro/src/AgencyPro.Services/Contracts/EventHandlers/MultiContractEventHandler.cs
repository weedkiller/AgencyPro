// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Runtime.CompilerServices;
using AgencyPro.Core.Contracts.Events;
using AgencyPro.Core.EventHandling;
using AgencyPro.Services.Messaging.Email;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Contracts.EventHandlers
{
    public partial class MultiContractEventHandler : MultiEventHandler<ContractEvent>,
        IEventHandler<ContractApprovedEvent>,
        IEventHandler<ContractEndedEvent>,
        IEventHandler<ContractPausedEvent>,
        IEventHandler<ContractRestartedEvent>,
        IEventHandler<ContractCreatedEvent>
    {
        private readonly ILogger<MultiContractEventHandler> _logger;

        public MultiContractEventHandler(
            ILogger<MultiContractEventHandler> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = logger;
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(MultiContractEventHandler)}.{callerName}] - {message}";
        }
    }
}