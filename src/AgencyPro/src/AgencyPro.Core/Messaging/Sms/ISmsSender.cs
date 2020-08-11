// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;

namespace AgencyPro.Core.Messaging.Sms
{
    public interface ISmsSender
    {
        Task<string> SendSmsAsync(SmsMessage message);
    }
}