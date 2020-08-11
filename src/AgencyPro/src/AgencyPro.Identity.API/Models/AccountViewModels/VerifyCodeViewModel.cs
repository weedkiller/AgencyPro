// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel.DataAnnotations;

namespace AgencyPro.Identity.API.Models.AccountViewModels
{
    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }
        [Required(ErrorMessage = "CODE_REQUIRED")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberBrowser { get; set; }
        public bool RememberMe { get; set; }
    }
}
