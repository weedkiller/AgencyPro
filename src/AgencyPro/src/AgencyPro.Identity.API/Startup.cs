// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Middleware.Startup;
using AgencyPro.Middleware.Startup.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.UserAccount.Services;
using AgencyPro.Middleware.Identity;

namespace AgencyPro.Identity.API
{
    public class Startup : ApiStartupBase, ISupportsIdentityServer, ISupportsLocalization
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger)
            : base(configuration, environment, logger)
        {
            Logger.LogInformation("Identity API Starting up");
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services.AddScoped(typeof(IUserInfo), s =>
            {
                var user = new UserIdentityInfo(s.GetService<IHttpContextAccessor>().HttpContext.User);
                return user;
            });

            services.AddScoped(provider =>
            {
                var svc = provider.GetRequiredService<ICustomerService>();
                var userInfo = provider.GetRequiredService<IUserInfo>();

                return svc.GetPrincipal(userInfo.UserId).Result as ICustomer;
            });
        }
    }
}