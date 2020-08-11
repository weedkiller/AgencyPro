// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel.DataAnnotations;

namespace AgencyPro.Core.UserAccount.ViewModels
{
    public class ChangePasswordFromResetKeyInput
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password confirmation must match password.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string Key { get; set; }
    }
}