// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.UserAccount.Models;
using AgencyPro.Data.EFCore;
using AgencyPro.Services.Account;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AgencyPro.Middleware.Startup
{
    public abstract partial class ApiStartupBase
    {

        protected void ConfigureIdentityServerServices(IServiceCollection services)
        {

            //var useLocalCertStore = Convert.ToBoolean(Configuration["UseLocalCertStore"]);
            //var certificateThumbprint = Configuration["CertificateThumbprint"];
            //services.Configure<StsConfig>(Configuration.GetSection("StsConfig"));
            //services.Configure<AgencyPro.Core.Config.EmailSettings>(Configuration.GetSection("EmailSettings"));


            //X509Certificate2 cert;

            //if (Environment.IsProduction() || Environment.IsEnvironment("Testing") || Environment.IsEnvironment("Staging"))
            //{
            //    if (useLocalCertStore)
            //    {
            //        using (X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine))
            //        {
            //            store.Open(OpenFlags.ReadOnly);
            //            var certs = store.Certificates.Find(X509FindType.FindByThumbprint, certificateThumbprint, false);
            //            cert = certs[0];
            //            store.Close();
            //        }
            //    }
            //    else
            //    {
            //        // Azure deployment, will be used if deployed to Azure
            //        var vaultConfigSection = Configuration.GetSection("Vault");
            //        var keyVaultService = new KeyVaultCertificateService(vaultConfigSection["Url"], vaultConfigSection["ClientId"], vaultConfigSection["ClientSecret"]);
            //        cert = keyVaultService.GetCertificateFromKeyVault(vaultConfigSection["CertificateName"]);
            //    }
            //}
            //else
            //{
            //    cert = new X509Certificate2(Path.Combine(Environment.ContentRootPath, "damienbodserver.pfx"), "");
            //}

            // services.AddScoped<IProfileService, UserAccountService>();

            //services.AddTransient<IUserValidator<ApplicationUser>, UserAccountValidator>();

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>,
                AdditionalUserClaimsPrincipalFactory>();

            services
                .AddIdentity<ApplicationUser, Role>(options =>
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
                .AddSignInManager<UserAccountSignInManager>()
                .AddClaimsPrincipalFactory<AdditionalUserClaimsPrincipalFactory>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer(options =>
                {
                   
                    options.IssuerUri = AppSettings.Urls.Origin;

                    //options.UserInteraction.LoginUrl = "";
                    //options.UserInteraction.ErrorUrl = "";

                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;


                })
                //.AddSigningCredential(cert)
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients(AppSettings))
                .AddProfileService<IdentityWithAdditionalClaimsProfileService>()
                .AddAspNetIdentity<ApplicationUser>();

        


        }
    }
}