// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Exceptions;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.UserAccount.Services;
using AgencyPro.Middleware.Startup;
using AgencyPro.Middleware.Startup.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AgencyPro.ProjectManager.API
{
    public class Startup : ApiStartupBase, ISupportsAuthentication, ISupportsSwagger
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger)
            : base(configuration, environment, logger)
        {
            Logger.LogInformation("Project Manager API Starting up");

        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddScoped(provider =>
            {
                var request = provider.GetRequiredService<IActionContextAccessor>();
                if (!request.ActionContext.RouteData.Values.ContainsKey("organizationId"))
                    throw new ForbiddenException();

                if (!Guid.TryParse(request.ActionContext.RouteData.Values["organizationId"].ToString(), out var id))
                    throw new ArgumentException("organization id is malformed");

                var service = provider.GetRequiredService<IOrganizationProjectManagerService>();
                var userInfo = provider.GetRequiredService<IUserInfo>();

                return service.GetPrincipal(userInfo.UserId, id).Result;
            });
        }
    }
}