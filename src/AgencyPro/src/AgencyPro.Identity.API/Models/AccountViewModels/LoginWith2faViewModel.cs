﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel.DataAnnotations;

namespace AgencyPro.Identity.API.Models.AccountViewModels
{
    public class LoginWith2faViewModel
    {
        [Required(ErrorMessage = "TWOFACTORCODE_REQUIRED")]
        [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        public string TwoFactorCode { get; set; }
        public bool RememberMachine { get; set; }
        public bool RememberMe { get; set; }
    }
}
