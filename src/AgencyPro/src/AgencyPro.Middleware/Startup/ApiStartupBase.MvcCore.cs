// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Constants;
using AgencyPro.Middleware.ApiExceptions;
using AgencyPro.Middleware.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace AgencyPro.Middleware.Startup
{
    public abstract partial class ApiStartupBase
    {
        private void ConfigureMvc(IApplicationBuilder app)
        {
            Log.Information(GetLogMessage("Configuring MVC Pipeline"));

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
                
            });
            app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = context =>
                {
                    if (context.Context.Response.Headers["feature-policy"].Count == 0)
                    {
                        var featurePolicy =
                            "accelerometer 'none'; camera 'none'; geolocation 'none'; gyroscope 'none'; magnetometer 'none'; microphone 'none'; payment 'none'; usb 'none'";

                        context.Context.Response.Headers["feature-policy"] = featurePolicy;
                    }

                    if (context.Context.Response.Headers["X-Content-Security-Policy"].Count == 0)
                    {
                        var csp =
                            "script-src 'self';style-src 'self';img-src 'self' data:;font-src 'self';frame-ancestors 'self';block-all-mixed-content";
                        // IE
                        context.Context.Response.Headers["X-Content-Security-Policy"] = csp;
                    }
                }
            });

            // Add MVC to the request pipeline.
            //app.UseDefaultFiles();
            // Add MVC to the request pipeline.
           

        }

        protected virtual void ConfigureFilters(FilterCollection filters)
        {
            Log.Information(GetLogMessage("Configuring Filters"));

            var policyBuilder = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                // .AuthenticationSchemes()
                .RequireScope(AppSettings.Scope);

            if (SupportsAdminRoleAuthentication)
            {
                policyBuilder.RequireRole(RoleNames.AdminRole);
            }

            if (this.SupportsAuthentication)
            {
                filters.Add(new AuthorizeFilter(policyBuilder.Build()));
            }
           
            filters.Add(new SecurityHeadersAttribute());
            filters.Add(typeof(ModelStateFeatureFilter));
            filters.Add(typeof(ValidateModelAttribute));
        }

        private void ConfigureMvcServices(IServiceCollection services)
        {
            // services.AddCustomHeaders();

            Log.Information(GetLogMessage("Configuring MVC Services"));
            services.AddMvc(options =>
                {
                    //http://www.dotnetcurry.com/aspnet/1314/aspnet-core-globalization-localization
                    //options.Conventions.Add(new HybridModelBinderApplicationModelConvention());

                    options.Conventions.Add(new NameSpaceVersionRoutingConvention());


                    ConfigureFilters(options.Filters);
                })
                .AddJsonOptions(opts =>
                {
                    opts.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
                    //opts.SerializerSettings.DateFormatString = 
                    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    opts.SerializerSettings.Formatting = Formatting.Indented;
                    opts.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
           
        }
    }
}