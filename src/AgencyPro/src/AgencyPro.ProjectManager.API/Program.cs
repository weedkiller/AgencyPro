// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Middleware;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace AgencyPro.ProjectManager.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Init();
        }

        public static IWebHostBuilder BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(WebHostBuilderMiddleware.ConfigureAppSharedLogging)
                .ConfigureAppConfiguration(WebHostBuilderMiddleware.ConfigureAppSharedConfiguration)
                .UseSerilog();
        }
    }
}