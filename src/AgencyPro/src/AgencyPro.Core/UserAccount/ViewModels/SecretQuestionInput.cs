// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel.DataAnnotations;

namespace AgencyPro.Core.UserAccount.ViewModels
{
    public class SecretQuestionInput
    {
        [Required] public string Question { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Answer { get; set; }
    }
}