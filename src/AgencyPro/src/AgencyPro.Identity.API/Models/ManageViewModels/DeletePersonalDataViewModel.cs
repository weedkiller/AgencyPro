// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel.DataAnnotations;

namespace AgencyPro.Identity.API.Models.ManageViewModels
{
    public class DeletePersonalDataViewModel
    {
        public bool RequirePassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "PASSWORD_REQUIRED")]
        public string Password { get; set; }
    }
}
