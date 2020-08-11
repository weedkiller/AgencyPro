// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.EmailSending.Services;
using AgencyPro.Core.EventHandling;
using AgencyPro.Core.ForgotPassword.Emails;
using AgencyPro.Core.ForgotPassword.Events;
using AgencyPro.Core.Templates.Models;
using AgencyPro.Data.EFCore;
using AgencyPro.Services.Messaging.Email;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.ForgotPassword.EventHandlers
{
    public class ForgotPasswordEventHandlers : MultiEventHandler<ForgotPasswordEvent>,
        IEventHandler<ForgotPasswordEvent>
    {
        private readonly ILogger<ForgotPasswordEventHandlers> _logger;

        public ForgotPasswordEventHandlers(
            ILogger<ForgotPasswordEventHandlers> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = logger;
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(ForgotPasswordEventHandlers)}.{callerName}] - {message}";
        }

        public void Handle(ForgotPasswordEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Forgot password event raised"));

            Parallel.Invoke(new List<Action>
            {
                () => SendForgotPasswordEmail(evt.PersonId, evt.CallbackUrl),
            }.ToArray());
        }

        private void SendForgotPasswordEmail(Guid personId, string url)
        {
            _logger.LogInformation(GetLogMessage("Person Id: {0}"), personId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var email = context.People
                    .Where(x => x.Id == personId)
                    .ProjectTo<ForgotPasswordEmail>(ProjectionMapping)
                    .First();

                email.Initialize(Settings);

                email.CallbackUrl = url;

                Send(TemplateTypes.ForgotPassword, email, "Password Reset Help", true, true);
            }
        }
    }
}
