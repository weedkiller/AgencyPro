// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Runtime.CompilerServices;
using AgencyPro.Core.Agreements.Events;
using AgencyPro.Core.EventHandling;
using AgencyPro.Services.Messaging.Email;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Agreements.Messaging
{
    public partial class MultiMarketingAgreementEventHandler : MultiEventHandler<MarketingAgreementEvent>,
        IEventHandler<MarketingAgreementCreated>,
        IEventHandler<MarketingAgreementAccepted>
    {
        private readonly ILogger<MultiMarketingAgreementEventHandler> _logger;

        public MultiMarketingAgreementEventHandler(
            ILogger<MultiMarketingAgreementEventHandler> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = logger;
        }


        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(MultiMarketingAgreementEventHandler)}.{callerName}] - {message}";
        }
    }
}
