// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Middleware.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AgencyPro.Middleware.Startup
{
    public abstract partial class ApiStartupBase
    {
        public void ConfigureSignalRServices(IServiceCollection services)
        {
            services.AddSignalR();
        }

        public void ConfigureSignalR(IApplicationBuilder app)
        {
            app.UseSignalR(routes =>
            {
                //routes.MapHub<AgencyOwnerHub>("/AgencyOwnerHub");
            });
        }
    }
}