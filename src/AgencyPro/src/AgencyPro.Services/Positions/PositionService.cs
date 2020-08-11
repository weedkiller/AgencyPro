// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Positions.Models;
using AgencyPro.Core.Positions.Services;
using AgencyPro.Core.Positions.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Services.Positions
{
    public class PositionService : Service<Position>, IPositionService
    {
        private readonly IRepositoryAsync<OrganizationPosition> _organizationPositions;

        public PositionService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _organizationPositions = UnitOfWork.RepositoryAsync<OrganizationPosition>();
        }

        public Task<PositionOutput> GetPosition(int positionId)
        {
            return Repository.Queryable().Where(x => x.Id == positionId)
                .ProjectTo<PositionOutput>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<List<OrganizationPositionOutput>> GetPositions(Guid organizationId)
        {
            return _organizationPositions.Queryable()
                .Where(x => x.OrganizationId == organizationId)
                .ProjectTo<OrganizationPositionOutput>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<PositionResult> Add(IAgencyOwner agencyOwner, int positionId)
        {
            var retVal = new PositionResult()
            {
                PositionId = positionId
            };

            var organizationPosition = new OrganizationPosition()
            {
                OrganizationId = agencyOwner.OrganizationId,
                PositionId = positionId,
                ObjectState = ObjectState.Added
            };

            var records =  _organizationPositions.InsertOrUpdateGraph(organizationPosition, true);

            if (records > 0)
            {
                retVal.Succeeded = true;
            }

            return Task.FromResult(retVal);
        }

        public Task<PositionResult> Remove(IAgencyOwner agencyOwner, int positionId)
        {
            var retVal = new PositionResult()
            {
                PositionId = positionId
            };


            var records = _organizationPositions.Delete(x =>
                x.OrganizationId == agencyOwner.OrganizationId && x.PositionId == positionId);

            if (records > 0)
            {
                retVal.Succeeded = true;
            }


            return Task.FromResult(retVal);
        }
    }
}
