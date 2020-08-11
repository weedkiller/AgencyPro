// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.EmailSending.Services;

namespace AgencyPro.Core.DisperseFunds.Emails
{
    public class PayoutEmail : IBasicEmail
    {
        public string RecipientName { get; set; }
        public string RecipientEmail { get; set; }
        public string FlowUrl { get; set; }
        public bool SendMail { get; set; }
        public decimal Amount { get; set; }
        public string Number { get; set; }
        public string ProjectName { get; set; }
        public string ProviderOrganizationName { get; set; }
        public string ProjectManagerOrganizationName { get; set; }
    }
    public class OrganizationTransferEmail : PayoutEmail
    {
        public string OrganizationName { get; set; }
    }

    public class PersonTransferEmail : PayoutEmail
    {
        
    }
}
