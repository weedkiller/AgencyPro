// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.ComponentModel.DataAnnotations;

namespace AgencyPro.Identity.API.Controllers.Organization
{
    public class OrganizationViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
