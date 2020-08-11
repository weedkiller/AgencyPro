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
using AgencyPro.Core.Registration.Emails;
using AgencyPro.Core.Registration.Events;
using AgencyPro.Core.Templates.Models;
using AgencyPro.Data.EFCore;
using AgencyPro.Services.Messaging.Email;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Registration.EventHandlers
{
    public class RegistrationEventHandlers : MultiEventHandler<RegistrationEvent>,
        IEventHandler<RegistrationEvent>
    {
        private readonly ILogger<RegistrationEventHandlers> _logger;

        public RegistrationEventHandlers(
            ILogger<RegistrationEventHandlers> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = logger;
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(RegistrationEventHandlers)}.{callerName}] - {message}";
        }

        public void Handle(RegistrationEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Person: {0}"), evt.PersonId);

            Parallel.Invoke(new List<Action>
            {
                () => SendWelcomeEmail(evt.PersonId),
            }.ToArray());

            // var user = _userManager.Users.First(x => x.Id == newPerson.PersonId);

            //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            //var callbackUrl = Url.Action(
            //    "ConfirmEmail",
            //    "Account",
            //    values: new { userId = user.Id, code = code },
            //    protocol: HttpContext.Request.Scheme);

            //EmailMessage msg = new EmailMessage
            //{
            //    Body = $"Please confirm your account by <a href='{HttpUtility.HtmlEncode(callbackUrl)}'>clicking here</a>.",
            //    To = user.Email,
            //    Subject = "Confirm your email"
            //};
            //await _emailSender.SendAsync(msg);


        }

        private void SendWelcomeEmail(Guid personId)
        {
            _logger.LogInformation(GetLogMessage("Person Id: {0}"), personId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var email = context.People
                    .Where(x => x.Id == personId)
                    .ProjectTo<WelcomeEmail>(ProjectionMapping)
                    .First();

                email.FlowUrl = Settings.Urls.Origin + "/" + Settings.Urls.LoginPage;

                Send(TemplateTypes.WelcomeEmail, email, "Welcome to AgencyPro", true, true);
            }
        }
    }
}
