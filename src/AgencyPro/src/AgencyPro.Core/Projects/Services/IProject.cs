// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Projects.Enums;

namespace AgencyPro.Core.Projects.Services
{
    public interface IProject
    {
        Guid Id { get; set; }
        ProjectStatus Status { get; set; }
        string Name { get; set; }
        string Abbreviation { get; set; }
        Guid CustomerOrganizationId { get; set; }
        Guid CustomerId { get; set; }
        Guid ProjectManagerId { get; set; }
        Guid ProjectManagerOrganizationId { get; set; }
        Guid AccountManagerId { get; set; }
        Guid AccountManagerOrganizationId { get; set; }
        decimal WeightedContractorAverage { get; }
        DateTimeOffset Created { get; set; }
        DateTimeOffset Updated { get; set; }
        Guid CreatedById { get; set; }
        Guid UpdatedById { get; set; }
    }
}