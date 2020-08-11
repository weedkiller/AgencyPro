// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Roles.Services
{
    public interface IContractor
    {
        Guid Id { get; set; }
        Guid RecruiterId { get; set; }
        Guid RecruiterOrganizationId { get; set; }
        bool IsAvailable { get; set; }
        DateTime? LastWorkedUtc { get; set; }
        DateTimeOffset Created { get; set; }
        DateTimeOffset Updated { get; set; }
        int HoursAvailable { get; set; }
    }
}