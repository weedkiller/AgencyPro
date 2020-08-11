// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stripe;

namespace AgencyPro.Middleware.Startup
{
    public abstract partial class ApiStartupBase
    {
        private void ConfigureStripe(IServiceProvider serviceProvider)
        {
            Logger.LogInformation(GetLogMessage("Configuring Stripe"));

            var options = (IOptions<AppSettings>)serviceProvider.GetRequiredService(typeof(IOptions<AppSettings>));

            StripeConfiguration.ApiKey = options.Value.Stripe.SecretApiKey;

        }
    }
}