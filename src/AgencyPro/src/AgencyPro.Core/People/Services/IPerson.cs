// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.People.Enums;

namespace AgencyPro.Core.People.Services
{
    public interface IPerson
    {
        Guid Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string ImageUrl { get; set; }
        string City { get; set; }
        string Iso2 { get; set; }
        string ProvinceState { get; set; }
        
        PersonStatus Status { get; set; }
        string DisplayName { get; }
        DateTimeOffset Created { get; set; }
        DateTimeOffset Updated { get; set; }
    }
}