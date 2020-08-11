// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.UserAccount.Services;
using AgencyPro.Middleware.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace AgencyPro.Middleware.Startup
{
    public abstract partial class ApiStartupBase
    {
        public void ConfigureIdentityServices(IServiceCollection services)
        {
            Log.Information(GetLogMessage("Configuring IdentityServices"));

            //services.AddTransient<IUserValidator<ApplicationUser>, UserAccountValidator>();
            //services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>,
            //    AdditionalUserClaimsPrincipalFactory>();

            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.ClaimsIdentity.RoleClaimType = JwtClaimTypes.Role;
            //    options.ClaimsIdentity.UserIdClaimType = JwtClaimTypes.Id;
            //    options.ClaimsIdentity.UserNameClaimType = JwtClaimTypes.Name;
            //    options.ClaimsIdentity.SecurityStampClaimType = JwtClaimTypes.IssuedAt;

            //    options.SignIn.RequireConfirmedEmail = false;
            //    options.SignIn.RequireConfirmedPhoneNumber = false;
            //    options.User.RequireUniqueEmail = true;

            //    options.Lockout.MaxFailedAccessAttempts = 10;
            //    options.Password.RequireDigit = true;

            //});

            //services
            //    .AddIdentity<ApplicationUser, Role>()
            //    //.AddDefaultUI()
            //    .AddEntityFrameworkStores<AppDbContext>()
            //    .AddUserManager<UserAccountManager>()
            //    .AddSignInManager<UserAccountSignInManager>()
            //    .AddClaimsPrincipalFactory<AdditionalUserClaimsPrincipalFactory>()
            //    .AddDefaultTokenProviders();

            //services.AddScoped<SignInManager<ApplicationUser>, UserAccountSignInManager>();
            //services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, AdditionalUserClaimsPrincipalFactory>();
            //services.AddScoped<UserManager<ApplicationUser>, UserAccountManager>();



            services.AddScoped(typeof(IUserInfo),
                s => new UserIdentityInfo(s.GetService<IHttpContextAccessor>().HttpContext.User));
        }

        public void ConfigureIdentity(IApplicationBuilder builder)
        {
            builder.UseAuthentication();
        }
    }
}