// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel.DataAnnotations;

namespace AgencyPro.Core.CustomerAccounts.ViewModels
{
    public class LinkCustomerWithCompanyInput : LinkCustomerInput
    {
        [Required] public string CompanyName { get; set; }

        public bool AutoApproveTimeEntries { get; set; }

    }
}