// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel.DataAnnotations;

namespace AgencyPro.Core.Login.ViewModels
{
    public class LoginInputModel
    {
        [Required(AllowEmptyStrings = false)]
        [MinLength(1)]
        public string Username { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MinLength(1)]
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }
    }
}