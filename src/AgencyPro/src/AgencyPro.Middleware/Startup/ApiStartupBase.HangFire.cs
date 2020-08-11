// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace AgencyPro.Middleware.Startup
{
    public abstract partial class ApiStartupBase
    {
        private void ConfigureHangfireServices(IServiceCollection services)
        {
            services.AddHangfire(x => x.UseSqlServerStorage(_connectionString));
        }
    }
}