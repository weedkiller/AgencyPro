// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Middleware.Extensions;
using AgencyPro.Middleware.Startup;
using AgencyPro.Middleware.Startup.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Marketer.API
{
    public class Startup : ApiStartupBase, ISupportsAuthentication, ISupportsSwagger
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger)
            : base(configuration, environment, logger)
        {
            Logger.LogInformation("Marketer API Starting up");

        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddScoped(provider => provider.GetOrganizationMarketer());
            services.AddScoped(provider => provider.GetMarketer());
        }
    }
}