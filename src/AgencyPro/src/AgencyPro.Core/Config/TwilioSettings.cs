// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.Config
{
    public class TwilioSettings
    {
        public string Sid { get; set; }
        public string Token { get; set; }
        public string BaseUri { get; set; }
        public string RequestUri { get; set; }
        public string From { get; set; }
        public int SmsMaxLength { get; set; }
    }
}