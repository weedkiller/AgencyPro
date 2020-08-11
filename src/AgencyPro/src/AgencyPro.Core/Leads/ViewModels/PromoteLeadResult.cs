// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.ViewModels;
using System;

namespace AgencyPro.Core.Leads.ViewModels
{
    public class PromoteLeadResult : BaseResult
    {
        public Guid LeadId { get; set; }
        public int? AccountNumber { get; set; }
        public Guid? CustomerOrganizationId { get; set; }
        public bool AccountLinked { get; set; }
        public bool AccountCreated { get; set; }
    }
}