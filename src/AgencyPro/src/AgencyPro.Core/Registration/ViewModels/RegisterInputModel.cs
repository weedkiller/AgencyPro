// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgencyPro.Core.Registration.ViewModels
{
    public class RegisterInputModel
    {
        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Text)]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Text)]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Text)]
        [Display(Name = "Invitation Code")]
        public string InvitationCode { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Country")]
        [MaxLength(2)]
        [MinLength(1)]
        public virtual string Iso2 { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "State/Province")]
        [MinLength(1)]
        public virtual string ProvinceState { get; set; }
        
        public string ReturnUrl { get; set; }

        public List<SelectListItem> Countries { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "US", Text = "United States"  },
        };
    }
}
