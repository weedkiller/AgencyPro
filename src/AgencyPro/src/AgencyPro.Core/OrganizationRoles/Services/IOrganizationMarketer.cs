// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.OrganizationRoles.Services
{
    public interface IOrganizationMarketer
    {
        Guid MarketerId { get; set; }
        Guid OrganizationId { get; set; }
    }
}