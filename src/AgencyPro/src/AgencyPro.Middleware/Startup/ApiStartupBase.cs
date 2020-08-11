// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Config;
using AgencyPro.Data.EFCore;
using AgencyPro.Middleware.Startup.Interfaces;
using AgencyPro.Services.Account;
using IdentityModel;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using AgencyPro.Core.UserAccount.Models;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace AgencyPro.Middleware.Startup
{
    public abstract partial class ApiStartupBase
    {

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(ApiStartupBase)}.{callerName}] - {message}";
        }

        public bool SupportsIdentityServer => (this is ISupportsIdentityServer);
        public bool SupportsAuthentication => (this is ISupportsAuthentication);
        public bool SupportsAdminRoleAuthentication => (this is ISupportAdminRoleAuthentication);
        public bool SupportsLocalization => (this is ISupportsLocalization);
        public bool SupportsSwagger => (this is ISupportsSwagger);
        public bool SupportsSinglePageApp => (this is ISupportsSinglePageApp);

        protected ILogger Logger;

        protected ApiStartupBase(IConfiguration configuration, IHostingEnvironment environment, ILogger logger)
        {
            Logger = logger;
            Configuration = configuration;
            Environment = environment;
            AppSettings = new AppSettings();
            _connectionString = Configuration.GetConnectionString("DefaultConnection");

        }

        protected AppSettings AppSettings { get; set; }

        protected IHostingEnvironment Environment { get; }
        protected IConfiguration Configuration { get; }
        protected IHttpContextAccessor HttpContextAccessor { get; set; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            // Add functionality to inject IOptions<T>
            services.AddOptions();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Add our Config object so it can be injected
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            appSettingsSection.Bind(AppSettings);

            // *If* you need access to generic IConfiguration this is **required**
            services.AddSingleton(Configuration);

            ConfigureCorsServices(services);
            ConfigureErrorHandlingServices(services);
            ConfigureAntiForgeryServices(services);

            ConfigureMvcServices(services);
            ConfigureSwaggerServices(services);
            ConfigureDatabaseServices(services);
            ConfigureInfrastructureServices(services);
            ConfigureAuthenticationServices(services);

            ConfigureRazor(services);

            if (SupportsAuthentication)
            {
                ConfigureIdentityServices(services);

            }

          

            ConfigureDomainServices(services);
            ConfigureAutoMapperServices(services);

            if (SupportsIdentityServer)
            {
                ConfigureIdentityServerServices(services);
            }
            else
            {
                if (SupportsAuthentication)
                {
                    services  // todo these should be set by DI settings
                        .AddIdentityCore<ApplicationUser>(options =>
                        {
                            options.ClaimsIdentity.RoleClaimType = JwtClaimTypes.Role;
                            options.ClaimsIdentity.UserIdClaimType = JwtClaimTypes.Id;
                            options.ClaimsIdentity.UserNameClaimType = JwtClaimTypes.Name;
                            options.ClaimsIdentity.SecurityStampClaimType = JwtClaimTypes.IssuedAt;

                            options.SignIn.RequireConfirmedEmail = false;
                            options.SignIn.RequireConfirmedPhoneNumber = false;
                            options.User.RequireUniqueEmail = true;
                            options.Lockout.MaxFailedAccessAttempts = 10;
                            options.Password.RequireDigit = false;
                            options.Password.RequiredLength = 4;
                            options.Password.RequiredUniqueChars = 0;
                            options.Password.RequireUppercase = false;
                            options.Password.RequireNonAlphanumeric = false;
                        })
                        .AddEntityFrameworkStores<AppDbContext>()
                        .AddUserManager<UserAccountManager>()
                        .AddClaimsPrincipalFactory<AdditionalUserClaimsPrincipalFactory>();
                }
            }

            if (SupportsSinglePageApp)
            {
                services.AddSpaStaticFiles(configuration =>
                {
                    configuration.RootPath = "ClientApp/dist";
                });
            }
            services.AddApplicationInsightsTelemetry();

            //ConfigureRazorLightServices(services);
            //ConfigureHangfireServices(services);
        }

        public virtual void Configure(
            IApplicationBuilder app,
            IAntiforgery antiForgery,
            IHostingEnvironment env,
            IServiceProvider serviceProvider)
        {

            Logger = (ILogger<ApiStartupBase>)serviceProvider.GetService(typeof(ILogger<ApiStartupBase>));
            Logger.LogInformation(GetLogMessage("Configuring Application"));
            // IdentityModelEventSource.ShowPII = true;



            HttpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();

            ConfigureErrorHandling(app);
            ConfigureStripe(serviceProvider);

            app.Use(async (ctx, next) =>
            {
                if (ctx.Request.IsHttps)
                {
                    await next();
                }
                else
                {
                    var withHttps = "https://" + ctx.Request.Host + ctx.Request.Path;
                    ctx.Response.Redirect(withHttps);
                }
            });

            ConfigureCors(app);

            if (SupportsLocalization)
            {
                // ConfigureLocalization(app);
            }

            if (SupportsAuthentication)
            {
                app.UseAuthentication();
            }


            if (SupportsIdentityServer)
            {
                app.UseIdentityServer();
            }

            ConfigureMvc(app);

            ConfigureAntiForgery(app, antiForgery);

            if (SupportsSwagger)
            {
                ConfigureSwagger(app);
            }

        }
    }
}