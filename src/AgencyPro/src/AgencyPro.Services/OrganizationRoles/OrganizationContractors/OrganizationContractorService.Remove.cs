// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyPro.Services.OrganizationRoles.OrganizationContractors
{
    public partial class OrganizationContractorService
    {
        public async Task<bool> Remove(IAgencyOwner ao, Guid personId, bool commit = true)
        {
            _logger.LogTrace(
                GetLogMessage(
                    $@"OrganizationId: {ao.OrganizationId}, ContractorId: {personId}"));

            var entity = _personRepository.Queryable()
                .Include(x => x.Contractor)
                .ThenInclude(x => x.Contracts)
                .FirstOrDefault(x => x.PersonId == personId && x.OrganizationId == ao.OrganizationId);

            if (entity.Contractor.Contracts.Count > 0)
            {
                foreach (var contract in entity.Contractor.Contracts)
                {
                    //contract.ContractorId = organization.DefaultContractorId;
                    contract.ObjectState = ObjectState.Modified;
                    contract.AgencyOwnerPauseDate = DateTimeOffset.UtcNow;
                    contract.Updated = DateTimeOffset.UtcNow;
                    contract.UpdatedById = _userInfo.UserId;
                }
            }

            entity.Contractor.ObjectState = ObjectState.Modified;
            entity.Contractor.IsDeleted = true;
            entity.Contractor.UpdatedById = _userInfo.UserId;
            entity.Contractor.Updated = DateTimeOffset.UtcNow;

            entity.Updated = DateTimeOffset.UtcNow;
            entity.UpdatedById = _userInfo.UserId;
            entity.ObjectState = ObjectState.Modified;

            int result = await _personRepository.UpdateAsync(entity, commit);
            return result > 0;
        }


    }
}