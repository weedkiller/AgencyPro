// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Globalization;
using AgencyPro.Middleware.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AgencyPro.Middleware.Startup
{
    public abstract partial class ApiStartupBase
    {
        private void ConfigureLocalization(IApplicationBuilder app)
        {
            var localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            localizationOptions.Value.RequestCultureProviders.Insert(0, new UrlRequestCultureProvider());
            app.UseRequestLocalization(localizationOptions.Value);
        }

        private void ConfigureLocalizationServices(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en"),
                        new CultureInfo("en-US"),
                        new CultureInfo("pt"),
                        new CultureInfo("pt-BR")
                    };
                    var defaultCulture = supportedCultures[1].Name;
                    options.DefaultRequestCulture = new RequestCulture(defaultCulture, defaultCulture);
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                }
            );
        }
    }
}