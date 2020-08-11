// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel.DataAnnotations;

namespace AgencyPro.Core.UserAccount.ViewModels
{

    //[Required(ErrorMessage = "EMAIL_REQUIRED")]
    //[EmailAddress(ErrorMessage = "EMAIL_INVALID")]
    //public string Email { get; set; }

    //[Required(ErrorMessage = "PASSWORD_REQUIRED")]
    //[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    //[DataType(DataType.Password)]
    //public string Password { get; set; }

    //[DataType(DataType.Password)]
    //[Compare("Password", ErrorMessage = "CONFIRM_PASSWORD_NOT_MATCHING")]
    //public string ConfirmPassword { get; set; }

    public class RegisterInput
    {
        [Required] public string UserName { get; set; }

        [Required] [EmailAddress] public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password confirmation must match password.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required] public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string ProvinceState { get; set; }

        [MaxLength(2)] public string Country { get; set; }
    }
}