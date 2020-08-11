// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AgencyPro.Middleware.Startup
{
    public abstract partial class ApiStartupBase
    {
        private void ConfigureAutoMapperServices(IServiceCollection services)
        {
            var config = new MapperConfiguration(x => { x.AddMaps("AgencyPro.Core"); });

            var mapper = config.CreateMapper();
            services.AddSingleton(config);
            services.AddScoped(sp => mapper);
        }
    }
}