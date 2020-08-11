// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Runtime.CompilerServices;
using AgencyPro.Core.EventHandling;
using AgencyPro.Core.Stories.Events;
using AgencyPro.Services.Messaging.Email;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Stories.EmailNotifications
{
    public partial class StoryEventHandlers : MultiEventHandler<StoryEvent>,
        IEventHandler<StoryCreatedEvent>,
        IEventHandler<StoryAssignedEvent>,
        IEventHandler<StoryCompletedEvent>,
        IEventHandler<StoryDeletedEvent>,
        IEventHandler<StoryUpdatedEvent>,
        IEventHandler<StoryApprovedEvent>
    {
        private readonly ILogger<StoryEventHandlers> _logger;

        public StoryEventHandlers(
            ILogger<StoryEventHandlers> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = logger;
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(StoryEventHandlers)}.{callerName}] - {message}";
        }
    }
}
