// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel.DataAnnotations;

namespace AgencyPro.Core.UserAccount.ViewModels
{
    public class PasswordResetWithSecretInputModel
    {
        public PasswordResetSecretViewModel[] Questions { get; set; }

        [Required] public string ProtectedAccountID { get; set; }
    }
}