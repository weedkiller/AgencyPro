// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Agreements.ViewModels;
using AgencyPro.Core.EmailSending.Services;

namespace AgencyPro.Core.Agreements.Emails
{
    public class ProviderMarketingAgreementEmail : MarketingAgreementOutput, IBasicEmail
    {
        public string RecipientName { get; set; }
        public string RecipientEmail { get; set; }
        public string FlowUrl { get; set; }
        public bool SendMail { get; set; }
    }
}