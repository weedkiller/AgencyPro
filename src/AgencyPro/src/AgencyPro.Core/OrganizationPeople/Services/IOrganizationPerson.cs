// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.People.Enums;

namespace AgencyPro.Core.OrganizationPeople.Services
{
    public interface IOrganizationPerson
    {
        Guid OrganizationId { get; set; }
        Guid PersonId { get; set; }
        PersonStatus Status { get; set; }
        bool IsHidden { get; set; }
        long PersonFlags { get; set; }
        long AgencyFlags { get; set; }
        bool IsOrganizationOwner { get; set; }
        bool IsDefault { get; set; }
        DateTimeOffset Created { get; set; }
        DateTimeOffset Updated { get; set; }
    }
}