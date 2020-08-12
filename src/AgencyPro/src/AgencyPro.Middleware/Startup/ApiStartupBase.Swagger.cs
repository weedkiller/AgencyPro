// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Collections.Generic;
using System.IO;

namespace AgencyPro.Middleware.Startup
{
    public abstract partial class ApiStartupBase
    {
        private void ConfigureSwaggerServices(IServiceCollection services)
        {
            var identityConfig = Configuration.GetSection("Identity");

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("oauth2", new OAuth2Scheme()
                {
                    Flow = "implicit",
                    AuthorizationUrl = identityConfig["Url"] + "/connect/authorize",
                    Scopes = new Dictionary<string, string> {
                        { AppSettings.Scope, AppSettings.Information.Name }
                    }
                });

                //c.OperationFilter<AuthorizeCheckOperationFilter>(AppSettings.Scope);

                c.SwaggerDoc("v1", new Info
                {
                    Title = AppSettings.Information.Name,
                    Version = AppSettings.Information.Version,
                    Description = AppSettings.Information.Description,
                    TermsOfService = AppSettings.Information.TermsOfService,
                    Contact = new Contact
                    {
                        Name = AppSettings.Information.ContactName,
                        Email = AppSettings.Information.ContactEmail
                    },
                    License = new License
                    {
                        Name = AppSettings.Information.LicenseName,
                        Url = AppSettings.Information.LicenseUrl
                    }
                });
                // c.OperationFilter<SwaggerRemoveCancellationTokenParameterFilter>();
                //Set the comments path for the swagger json and ui.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Data.API.xml");
                c.IncludeXmlComments(xmlPath);

                c.SwaggerGeneratorOptions.DescribeAllParametersInCamelCase = true;

            });
        }


        private void ConfigureSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();

         
            // Middleware to expose interactive documentation
            app.UseSwaggerUI(c =>
            {
                //  c.ConfigureOAuth2("workforce", "trinidad", identityConfig["Url"], "bridge");
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", AppSettings.Information.Name);
                c.DocumentTitle = AppSettings.Information.Name;
                //c.OAuth2RedirectUrl("");
                c.SupportedSubmitMethods(SubmitMethod.Delete, SubmitMethod.Get, SubmitMethod.Delete, SubmitMethod.Post,
                    SubmitMethod.Patch, SubmitMethod.Put);

                c.OAuthClientId("swagger");
                c.OAuthAppName("AgencyPro");

                // try this.
                c.DisplayOperationId();
            });
        }
    }
}