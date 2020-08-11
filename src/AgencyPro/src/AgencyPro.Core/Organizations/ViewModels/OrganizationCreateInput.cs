// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel.DataAnnotations;

namespace AgencyPro.Core.Organizations.ViewModels
{
    public class OrganizationCreateInput
    {
        [Required] public virtual string Name { get; set; }
        
        public virtual string Description { get; set; }
        public string Iso2 { get; set; }
        public string ProvinceState { get; set; }
    }
}