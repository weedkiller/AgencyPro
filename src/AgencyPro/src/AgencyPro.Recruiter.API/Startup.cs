// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Net;
using AgencyPro.Core.Exceptions;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Middleware.Extensions;
using AgencyPro.Middleware.Startup;
using AgencyPro.Middleware.Startup.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Recruiter.API
{
    public class Startup : ApiStartupBase, ISupportsAuthentication, ISupportsSwagger
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger) 
            : base(configuration, environment, logger)
        {
            Logger.LogInformation("Recruiter API Starting up");
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddScoped(provider => provider.GetRecruiter());
            services.AddScoped(provider => provider.GetOrganizationRecruiter());
        }
    }
}