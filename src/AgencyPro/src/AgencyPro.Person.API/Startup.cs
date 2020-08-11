// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Middleware.Extensions;
using AgencyPro.Middleware.Startup;
using AgencyPro.Middleware.Startup.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Person.API
{
    public class Startup : ApiStartupBase, ISupportsAuthentication, ISupportsSwagger
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger)
            : base(configuration, environment, logger)
        {
            logger.LogInformation("Person API Starting up");
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            // todo: pattern variation
            services.AddScoped(provider => provider.GetContractor());
            services.AddScoped(provider => provider.GetAccountManager());
            services.AddScoped(provider => provider.GetProjectManager());
            services.AddScoped(provider => provider.GetRecruiter());
            services.AddScoped(provider => provider.GetCustomer());
            services.AddScoped(provider => provider.GetMarketer());

            services.AddScoped(provider => provider.GetPerson());
            services.AddScoped(provider => provider.GetOrganizationPerson());
        }
    }
}