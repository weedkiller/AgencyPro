// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.Config
{

    public class EmailSettings
    {
        public bool SendMail { get; set; }
        public string SmtpServer { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string ApiKey { get; set; }

        
        public string SenderEmailAddress { get; set; }
        public string SenderName { get; set; }
    }
}