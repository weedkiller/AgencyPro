// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.OrganizationRoles.Services
{
    public interface IOrganizationProjectManager
    {
        Guid ProjectManagerId { get; set; }
        Guid OrganizationId { get; set; }
    }
}