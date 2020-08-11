// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace AgencyPro.Middleware.Startup
{
    public abstract partial class ApiStartupBase
    {
        protected virtual void ConfigureAuthenticationServices(IServiceCollection services)
        {
            Log.Information(GetLogMessage("Configuring Authentication Services"));

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; 
                   // options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    //options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    //options.DefaultForbidScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    //options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    //options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    //options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    //options.TokenValidationParameters.ActorValidationParameters
                    
                    options.Authority = AppSettings.Urls.Origin;
                    options.RequireHttpsMetadata = true;
                    options.Audience = AppSettings.Scope;

                   

                    options.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = c =>
                        {
                            var logger = c.HttpContext.RequestServices.GetRequiredService<ILogger<ApiStartupBase>>();
                            logger.LogTrace("Authentication Failure");
                            return Task.FromResult(0);
                        },
                        OnTokenValidated = c =>
                        {
                            var logger = c.HttpContext.RequestServices.GetRequiredService<ILogger<ApiStartupBase>>();
                            logger.LogTrace("Authentication Success");
                            return Task.FromResult(0);
                        }
                    };
                });

        }

    }
}