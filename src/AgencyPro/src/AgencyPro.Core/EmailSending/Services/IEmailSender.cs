// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;
using AgencyPro.Core.Messaging.Email;

namespace AgencyPro.Core.EmailSending.Services
{
    public interface IEmailSender
    {
        void Send(EmailMessage message, EmailAttachment attachment = null);

        Task SendAsync(EmailMessage message, EmailAttachment attachment = null);
    }
}