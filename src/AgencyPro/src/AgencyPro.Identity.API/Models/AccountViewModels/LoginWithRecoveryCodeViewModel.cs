// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel.DataAnnotations;

namespace AgencyPro.Identity.API.Models.AccountViewModels
{
    public class LoginWithRecoveryCodeViewModel
    {
        [Required(ErrorMessage = "ACCOUNT_RECOVERY_CODE_REQUIRED")]
        [DataType(DataType.Text)]
        public string RecoveryCode { get; set; }
    }
}
