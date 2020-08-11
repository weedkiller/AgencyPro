// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Config;

namespace AgencyPro.Core.EmailSending.Services
{
    public static class BasicEmailExtensions
    {
        public static void Initialize(this IBasicEmail email, AppSettings settings)
        {
            email.FlowUrl = settings.Urls.Flow;
        }
    }

    public interface IBasicEmail
    {
        string RecipientName { get; set; }
        string RecipientEmail { get; set; }
        string FlowUrl { get; set; }
        bool SendMail { get; set; }
    }
}