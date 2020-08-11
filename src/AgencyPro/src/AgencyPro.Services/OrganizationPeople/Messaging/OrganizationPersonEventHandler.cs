// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Runtime.CompilerServices;
using AgencyPro.Core.EventHandling;
using AgencyPro.Core.OrganizationPeople.Events;
using AgencyPro.Services.Messaging.Email;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.OrganizationPeople.Messaging
{
    public partial class OrganizationPersonEventHandler : MultiEventHandler<OrganizationPersonEvent>, 
        IEventHandler<OrganizationPersonCreatedEvent>
    {
        private readonly ILogger<OrganizationPersonEventHandler> _logger;

        public OrganizationPersonEventHandler(
            ILogger<OrganizationPersonEventHandler> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = logger;
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(OrganizationPersonEventHandler)}.{callerName}] - {message}";
        }
    }
}
