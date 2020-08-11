// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AgencyPro.Core.Config;
using AgencyPro.Core.EmailSending.Services;
using AgencyPro.Core.Messaging.Email;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AgencyPro.Middleware.Messaging
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;
        private static EmailSettings _emailSettings;
        private static SendGridClient _client;

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(EmailSender)}.{callerName}] - {message}";
        }

        public EmailSender(IOptions<AppSettings> settings, ILogger<EmailSender> logger)
        {
            _logger = logger;
            _emailSettings = settings.Value.Email;

            _logger.LogInformation(GetLogMessage("Initializing Sendgrid: {0}"), _emailSettings.ApiKey);
            _client = new SendGridClient(_emailSettings.ApiKey);
        }

        public void Send(EmailMessage message, EmailAttachment attachment = null)
        {
            var msg = new SendGridMessage
            {
                From = new EmailAddress(_emailSettings.FromEmail, _emailSettings.FromName),
                Subject = message.Subject,
                HtmlContent = message.Body
            };
            msg.AddTo(new EmailAddress(message.To));
            var response = _client.SendEmailAsync(msg).Result;
        }

        public async Task SendAsync(EmailMessage message, EmailAttachment attachment = null)
        {
            _logger.LogInformation(GetLogMessage("Sending message: {message}"), message);

            var msg = new SendGridMessage
            {
                From = new EmailAddress(_emailSettings.FromEmail, _emailSettings.FromName),
                Subject = message.Subject,
                HtmlContent = message.Body
            };
            msg.AddTo(new EmailAddress(message.To));
            var response = await _client.SendEmailAsync(msg);

            _logger.LogDebug(GetLogMessage("{@response}"), response);
        }
    }
}