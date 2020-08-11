// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Data.EFCore;
using AgencyPro.Middleware.Extensions;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using ILogger = Serilog.ILogger;

namespace AgencyPro.Middleware
{
    public static class WebHostBuilderMiddleware
    {

        public static IConfiguration Configuration { get; set; }

        public static void ConfigureAppSharedConfiguration(WebHostBuilderContext hostingContext,
            IConfigurationBuilder config)
        {
            var env = hostingContext.HostingEnvironment;
            var assembly = typeof(WebHostBuilderMiddleware).Assembly;

            config
                .AddEmbeddedJsonFile(assembly, "sharedSettings.json")
                .AddEmbeddedJsonFile(assembly, $"sharedSettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("appsettings.json", true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            config.AddEnvironmentVariables();
        }

        public static void ConfigureAppSharedLogging(WebHostBuilderContext hostingContext, ILoggingBuilder logging)
        {
            logging.AddConsole();
            logging.AddDebug();
            logging.AddFilter(DbLoggerCategory.Database.Connection.Name, LogLevel.Information);
            logging.AddFilter("AgencyPro", LogLevel.Debug);
            logging.AddFilter("Microsoft.AspNetCore", LogLevel.Warning);
            logging.AddFilter("IdentityServer4", LogLevel.Warning);
        }

        public static ILogger CreateLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.ApplicationInsights(TelemetryConfiguration.Active, TelemetryConverter.Traces)
                .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File(@"d:\home\logfiles\application\myapp.txt",
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    fileSizeLimitBytes: 1_000_000,
                    rollOnFileSizeLimit: true,
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1))
                .CreateLogger();
        }

        public static void Init(
            this IWebHostBuilder hostBuilder,
            string initMessage = "Starting Application",
            string exceptionMessage = "Application terminated unexpectedly"
        )
        {
            Log.Logger = CreateLogger();
          
            try
            {
                Log.Information(initMessage);
                var host = hostBuilder.Build();

                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    try
                    {
                        var context = services.GetRequiredService<AppDbContext>();
                        //DataSeeder seeder = services.GetRequiredService<DataSeeder>();

                        // todo: figure out the correct sequence
                        //context.Database.Migrate();
                        context.Database.EnsureCreated();
                        context.Database.Migrate();


                        //Log.Information($"Database created and seeded: {created}");
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "An error occurred seeding the DB.");
                    }
                }


                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, exceptionMessage);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}