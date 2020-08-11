// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AgencyPro.Middleware.Startup
{
    public abstract partial class ApiStartupBase
    {
        public void ConfigureAntiForgeryServices(IServiceCollection services)
        {
            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
        }

        public void ConfigureAntiForgery(IApplicationBuilder app, IAntiforgery antiForgery)
        {
            app.Use(next => context =>
            {
                if (!string.Equals(context.Request.Path.Value, "/", StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals(context.Request.Path.Value, "/index.html", StringComparison.OrdinalIgnoreCase))
                    return next(context);

                var tokens = antiForgery.GetAndStoreTokens(context);
                context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken,
                    new CookieOptions {HttpOnly = false});
                return next(context);
            });
        }
    }
}