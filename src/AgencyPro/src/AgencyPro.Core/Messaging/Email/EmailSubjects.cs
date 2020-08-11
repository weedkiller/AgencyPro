// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;

namespace AgencyPro.Core.Messaging.Email
{
    public class EmailSubjects
    {
        public EmailSubjects(Dictionary<string, Dictionary<string, string>> subjects = null)
        {
            Subjects = subjects ?? new Dictionary<string, Dictionary<string, string>>();
        }

        public Dictionary<string, Dictionary<string, string>> Subjects { get; set; }
    }
}