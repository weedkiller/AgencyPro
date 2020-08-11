// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.ComponentModel.DataAnnotations;
using AgencyPro.Core.People.ViewModels;

namespace AgencyPro.Core.CustomerAccounts.ViewModels
{
    public class NewCustomerAccountInput : PersonInput
    {
        public Guid? MarketerId { get; set; }

        public Guid? AccountManagerId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string OrganizationName { get; set; }

        public int? PaymentTermId { get; set; }
        public Guid? MarketerOrganizationId { get; set; }
        public bool AutoApproveTimeEntries { get; set; }
        
    }
}