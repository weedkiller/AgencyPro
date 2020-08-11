// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.IO;

namespace AgencyPro.Core.Messaging.Email
{
    public class EmailAttachment
    {
        public string Name { get; set; }

        public MemoryStream File { get; set; }
    }
}