// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AgencyPro.Core.EmailSending.Services;
using AgencyPro.Core.Extensions;
using AgencyPro.Core.Infrastructure.Storage;
using AgencyPro.Core.Messaging.Email;
using AgencyPro.Core.Messaging.Sms;
using AgencyPro.Middleware.Messaging;
using AgencyPro.Services.Extensions;
using AgencyPro.Services.Storage;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace AgencyPro.Middleware.Startup
{
    public abstract partial class ApiStartupBase
    {
        private void ConfigureInfrastructureServices(IServiceCollection services)
        {
            Log.Information(GetLogMessage("Configuring Infrastructure Services"));

            services.Configure<AgencyPro.Core.Config.EmailSettings>(Configuration.GetSection("EmailSettings"));

            services.AddSingleton(typeof(ISmsSender), typeof(SmsSender));
            services.AddSingleton(typeof(IEmailSender), typeof(EmailSender));
            services.AddSingleton(typeof(IStorageService), typeof(StorageService));

            var asm = Assembly.GetEntryAssembly();
            var subjectFiles = asm.GetManifestResourceNames().Where(x => x.Contains("subjects.json"));

            var emailSubjects = new Dictionary<string, Dictionary<string, string>>();

            subjectFiles.ForEach(file =>
            {
                var domain = file.Split('.');
                var subjects = asm.ParseFromJson(file);
                emailSubjects.Add(domain[3], subjects);
            });

            services.AddSingleton(typeof(IEmailSubjects), emailSubjects);

            services.AddTransient<IActionContextAccessor, ActionContextAccessor>();

        }
    }
}