// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Contracts.ViewModels;
using AgencyPro.Core.EmailSending.Services;

namespace AgencyPro.Core.Contracts.Emails
{
    public class AgencyOwnerContractEmail : AgencyOwnerProviderContractOutput, IBasicEmail
    {
        public string RecipientName { get; set; }
        public string RecipientEmail { get; set; }
        public string FlowUrl { get; set; }
        public bool SendMail { get; set; }
    }
}