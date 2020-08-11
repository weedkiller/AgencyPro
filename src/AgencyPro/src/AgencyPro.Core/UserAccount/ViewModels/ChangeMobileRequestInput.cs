// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel.DataAnnotations;

namespace AgencyPro.Core.UserAccount.ViewModels
{
    public class ChangeMobileRequestInput
    {
        [Required] public string MobilePhoneNumber { get; set; }

        [Required] public string NewMobilePhone { get; set; }
    }
}