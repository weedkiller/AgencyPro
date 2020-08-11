// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AgencyPro.Core.CustomerAccounts.ViewModels
{
    public class LinkCustomerInput
    {
        [BindRequired] public Guid AccountManagerId { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        public Guid AccountManagerOrganizationId { get; set; }

        public int? PaymentTermId { get; set; }
    }
}