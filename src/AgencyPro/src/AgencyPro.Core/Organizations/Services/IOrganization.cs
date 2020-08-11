﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Organizations.Models;

namespace AgencyPro.Core.Organizations.Services
{
    public interface IOrganization : IOrganizationTheme
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string ImageUrl { get; set; }
      
        int CategoryId { get; set; }
        OrganizationType OrganizationType { get; set; }

        DateTimeOffset Created { get; set; }
        DateTimeOffset Updated { get; set; }
        string City { get; set; }
        string Iso2 { get; set; }
        string ProvinceState { get; set; }
        string PostalCode { get; set; }
    }
}