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
using AgencyPro.Core.ResetPassword.Events;
using AgencyPro.Core.Templates.Models;
using AgencyPro.Data.EFCore;
using AgencyPro.Services.Messaging.Email;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.ResetPassword.EventHandlers
{
    public class ResetPasswordEventHandlers : MultiEventHandler<ResetPasswordEvent>,
        IEventHandler<ResetPasswordEvent>
    {
        private readonly ILogger<ResetPasswordEventHandlers> _logger;

        public ResetPasswordEventHandlers(
            ILogger<ResetPasswordEventHandlers> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = logger;
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(ResetPasswordEventHandlers)}.{callerName}] - {message}";
        }

        public void Handle(ResetPasswordEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Reset password event raised"));

            Parallel.Invoke(new List<Action>
            {
                () => SendResetPasswordEmail(evt.PersonId),
            }.ToArray());
        }

        private void SendResetPasswordEmail(Guid personId)
        {
            _logger.LogInformation(GetLogMessage("Person Id: {0}"), personId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var email = context.People
                    .Where(x => x.Id == personId)
                    .ProjectTo<ForgotPasswordEmail>(ProjectionMapping)
                    .First();

                email.Initialize(Settings);

                Send(TemplateTypes.PasswordReset, email, "Your password was reset", true, true);
            }
        }
    }
}
