// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Middleware.Extensions;
using AgencyPro.Middleware.Startup;
using AgencyPro.Middleware.Startup.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AgencyPro.AgencyOwner.API
{
    public class Startup : ApiStartupBase, ISupportsAuthentication, ISupportsSwagger
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger)
            : base(configuration, environment, logger)
        {
           logger.LogInformation("Agency Owner API Starting up");
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddScoped(provider => provider.GetAgencyOwner());
            services.AddScoped(provider => provider.GetMarketingAgencyOwner());
            services.AddScoped(provider => provider.GetProviderAgencyOwner());
            services.AddScoped(provider => provider.GetRecruitingAgencyOwner());
        }
    }
}