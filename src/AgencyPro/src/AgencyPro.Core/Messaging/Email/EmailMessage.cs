// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;

namespace AgencyPro.Core.Messaging.Email
{
    public class EmailMessage
    {
        public string From { get; set; }
        public string To { get; set; }
        public List<string> Recipients { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool AsHtml { get; set; }
    }
}