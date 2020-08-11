// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AgencyPro.Middleware.Startup
{
    public abstract partial class ApiStartupBase
    {
        public virtual void ConfigureCorsServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                );
            });
        }

        public virtual void ConfigureCors(IApplicationBuilder app)
        {
            app.UseCors("CorsPolicy");
        }
    }
}