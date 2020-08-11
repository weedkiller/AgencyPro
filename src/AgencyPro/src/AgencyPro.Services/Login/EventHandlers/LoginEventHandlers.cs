// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Runtime.CompilerServices;
using AgencyPro.Core.EventHandling;
using AgencyPro.Core.Login.Events;
using AgencyPro.Services.Messaging.Email;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Login.EventHandlers
{
    public class LoginEventHandlers : MultiEventHandler<LoginEvent>,
        IEventHandler<LoginEvent>
    {
        private readonly ILogger<LoginEventHandlers> _logger;

        public LoginEventHandlers(
            ILogger<LoginEventHandlers> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = logger;
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(LoginEventHandlers)}.{callerName}] - {message}";
        }

        public void Handle(LoginEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Login Event Handled"));
        }
    }
}
