// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Services.Messaging.Email;
using System;
using System.Runtime.CompilerServices;
using AgencyPro.Core.EventHandling;
using AgencyPro.Core.Projects.Events;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Projects.Messaging
{
    public partial class MultiProjectEventHandler : MultiEventHandler<ProjectEvent>,
        IEventHandler<ProjectCreatedEvent>,
        IEventHandler<ProjectDeletedEvent>,
        IEventHandler<ProjectEndedEvent>,
        IEventHandler<ProjectRestartedEvent>
    {

        private readonly ILogger<MultiProjectEventHandler> _logger;

        public MultiProjectEventHandler(
            ILogger<MultiProjectEventHandler> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = logger;

        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(MultiProjectEventHandler)}.{callerName}] - {message}";
        }
    }
}
