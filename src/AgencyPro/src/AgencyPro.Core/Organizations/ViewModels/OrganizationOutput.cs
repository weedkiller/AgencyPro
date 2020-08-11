// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Organizations.Services;

namespace AgencyPro.Core.Organizations.ViewModels
{
    public class OrganizationOutput : OrganizationCreateInput, IOrganization
    {
        public virtual string CategoryName { get; set; }

        public virtual Guid Id { get; set; }
        
        public int CategoryId { get; set; }
        public virtual OrganizationType OrganizationType { get; set; }
        public virtual DateTimeOffset Created { get; set; }
        public virtual DateTimeOffset Updated { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public virtual string ImageUrl { get; set; }

        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string TertiaryColor { get; set; }
     
        
    }
}