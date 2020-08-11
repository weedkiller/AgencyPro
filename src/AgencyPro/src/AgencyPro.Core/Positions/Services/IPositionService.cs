// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Positions.ViewModels;

namespace AgencyPro.Core.Positions.Services
{
    public interface IPositionService
    {
        Task<PositionOutput> GetPosition(int positionId);
        Task<List<OrganizationPositionOutput>> GetPositions(Guid organizationId);
        Task<PositionResult> Add(IAgencyOwner agencyOwner, int positionId);
        Task<PositionResult> Remove(IAgencyOwner agencyOwner, int positionId);
    }
}
