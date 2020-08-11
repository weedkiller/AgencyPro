// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Leads.ViewModels
{
    public class LeadRequirementsModel
    {
        public string Idea { get; set; }
        public string Summary { get; set; }
        public Guid[] Skills { get; set; }
    }
}