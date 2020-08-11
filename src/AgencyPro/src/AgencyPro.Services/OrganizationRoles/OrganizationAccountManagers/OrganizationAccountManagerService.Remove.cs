// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyPro.Services.OrganizationRoles.OrganizationAccountManagers
{
    public partial class OrganizationAccountManagerService
    {
        public async Task<bool> Remove(IAgencyOwner ao, Guid personId, bool commit = true)
        {
            _logger.LogTrace(
                GetLogMessage(
                    $@"OrganizationId: {ao.OrganizationId}, AccountManagerId: {personId}"));

            var entity = _personRepository.Queryable()
                .Include(x => x.AccountManager)
                .ThenInclude(x => x.Accounts)
                .FirstOrDefault(x => x.PersonId == personId && x.OrganizationId == ao.OrganizationId);

            var organization = await _orgService.Repository.Queryable()
                    .Include(x=>x.ProviderOrganization)
                .Where(x=>x.Id == ao.OrganizationId)
                .FirstOrDefaultAsync();

            if (entity.AccountManager.Accounts.Count > 0)
            {

                foreach (var account in entity.AccountManager.Accounts)
                {
                    account.AccountManagerId = organization.ProviderOrganization.DefaultAccountManagerId;
                    account.ObjectState = ObjectState.Modified;
                    account.Updated = DateTimeOffset.UtcNow;
                    account.UpdatedById = _userInfo.UserId;
                }
            }

            entity.AccountManager.IsDeleted = true;
            entity.AccountManager.UpdatedById = _userInfo.UserId;
            entity.AccountManager.Updated = DateTimeOffset.UtcNow;
            entity.AccountManager.ObjectState = ObjectState.Modified;

            entity.Updated = DateTimeOffset.UtcNow;
            entity.UpdatedById = _userInfo.UserId;
            entity.ObjectState = ObjectState.Modified;

            int result = _personRepository.InsertOrUpdateGraph(entity, commit);
            return result > 0;
        }
    }
}